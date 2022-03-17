using System.Collections;
using UnityEngine;

[System.Serializable]
public struct MoveRange
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}

public class PlayerController : MonoBehaviour
{
    private const int ROTATION_SPD = 300;

    public int atkLevel;
    public float spd;
    public float atkDmg;

    [SerializeField] private float bulletSpd;
    [SerializeField] private float bulletInterval;
    [SerializeField] private Transform firePos;
    [SerializeField] private Bullet bulletObj;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private MoveRange moveRange;

    [SerializeField] private GameObject frontViewCam;
    [SerializeField] private GameObject topViewCam;

    private Rigidbody rb;

    private float bulletTime;
    private float rotationZ;
    private bool isAttacked;

    private bool isTopViewCam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        bulletTime = Time.time;
    }

    private void Update()
    {
        #region 총 발사
        if (Input.GetKey(KeyCode.Space) && Time.time > bulletTime)
        {
            bulletTime = Time.time + bulletInterval;
            SoundManager.Instance.PlaySound(Sound_Effect.SHOT);
            Bullet bullet = Instantiate(bulletObj, firePos.position, bulletObj.transform.rotation);
            bullet.SetBullet(atkDmg, bulletObj.dir, bulletSpd);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.R))
        {
            isTopViewCam = !isTopViewCam;
            topViewCam.SetActive(isTopViewCam);
            frontViewCam.SetActive(isTopViewCam == false);
        }
    }

    private void FixedUpdate()
    {
        #region 회전
        float h = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(h, 0))
        {
            rotationZ = Mathf.Lerp(rotationZ, 0, Time.deltaTime * 2f);
        }
        else
        {
            rotationZ += -h * ROTATION_SPD * Time.deltaTime;
            rotationZ = Mathf.Clamp(rotationZ, -50, 50);
        }

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x, 
            transform.localEulerAngles.y, 
            rotationZ);
        #endregion

        #region 이동
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        rb.velocity = moveDir * (Input.GetKey(KeyCode.LeftShift) ? spd / 2 : spd);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, moveRange.xMin, moveRange.xMax),
            0,
            Mathf.Clamp(rb.position.z, moveRange.zMin, moveRange.zMax));
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacked) return;

        if (other.CompareTag("EnemyBullet"))
        {
            SoundManager.Instance.PlaySound(Sound_Effect.HIT);
            Destroy(other.gameObject);
            Instantiate(hitEffect).transform.position = other.transform.position;

            GameManager.Instance.Health = Mathf.Max(0, GameManager.Instance.Health - other.GetComponent<Bullet>().dmg);
            if (Mathf.Approximately(GameManager.Instance.Health, 0))
            {
                OnDie();
            }
            else
            {
                OnHit();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            SoundManager.Instance.PlaySound(Sound_Effect.HIT);
            Instantiate(hitEffect).transform.position = other.transform.position;

            GameManager.Instance.Health = Mathf.Max(0, GameManager.Instance.Health - other.GetComponent<Enemy>().atkDmg / 2f);
            if (Mathf.Approximately(GameManager.Instance.Health, 0))
            {
                OnDie();
            }
            else
            {
                OnHit();
            }
        }
    }

    private void OnHit()
    {
        StartCoroutine(HitCoroutine());
    }

    private IEnumerator HitCoroutine()
    {
        isAttacked = true;
        yield return new WaitForSeconds(1.5f);
        isAttacked = false;
    }

    private void OnDie()
    {
        GameManager.Instance.GameOver();
    }
}

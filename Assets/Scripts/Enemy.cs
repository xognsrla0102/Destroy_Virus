using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform firePos;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;
    [SerializeField] protected Bullet bulletObj;

    [SerializeField] protected float spd;
    [SerializeField] protected float hp;
    public float atkDmg;
    [SerializeField] protected float bulletSpd;
    [SerializeField] protected float bulletInterval;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("Attack", 1f, bulletInterval);
        Move();
    }

    protected abstract void Attack();
    private void Move()
    {
        rb.velocity = Vector3.back * spd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            CameraManager.Instance.EnemyHitShake();

            SoundManager.Instance.PlaySound(Sound_Effect.HIT);
            Destroy(other.gameObject);
            Instantiate(hitEffect).transform.position = other.transform.position;

            hp = Mathf.Max(0, hp - other.GetComponent<Bullet>().dmg);
            if (hp == 0)
            {
                OnDie();
            }
        }
    }

    public void OnDie()
    {
        SoundManager.Instance.PlaySound(Sound_Effect.EXPLOSION);
        Instantiate(dieEffect).transform.position = transform.position;
        Destroy(gameObject);
    }
}

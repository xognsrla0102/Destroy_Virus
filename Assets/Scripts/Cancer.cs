using System.Collections;
using UnityEngine;

public class Cancer : Enemy
{
    [SerializeField] private float continiousShotInterval;

    [HideInInspector] public int wayCnt;
    [HideInInspector] public int shotCnt;

    protected override void Start()
    {
        base.Start();
        shotCnt = 2;
        wayCnt = 2;
    }

    protected override void Attack()
    {
        StartCoroutine(TornadoShotCoroutine());
    }

    private IEnumerator TornadoShotCoroutine()
    {
        for (int shotCnt = 0; shotCnt < this.shotCnt; shotCnt++)
        {
            N_Way_Shot();
            yield return new WaitForSeconds(continiousShotInterval);
        }
    }

    private void N_Way_Shot()
    {
        Vector3 playerDir = GameObject.Find("Player").transform.position - transform.position;
        float rot = Mathf.Rad2Deg * Mathf.Atan2(playerDir.z, playerDir.x);
        rot -= 15 * wayCnt / 2;

        for (int wayCnt = 1; wayCnt <= this.wayCnt; wayCnt++)
        {
            Vector3 dir = new Vector3(Mathf.Cos(rot * Mathf.Deg2Rad), 0, Mathf.Sin(rot * Mathf.Deg2Rad)).normalized;
            Bullet bullet = Instantiate(bulletObj);
            bullet.transform.position = transform.position;
            bullet.SetBullet(atkDmg, dir, bulletSpd);
            rot += 15;
        }
    }
}

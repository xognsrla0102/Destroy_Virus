using UnityEngine;

public class Virus : Enemy
{
    protected override void Attack()
    {
        Bullet bullet = Instantiate(bulletObj);
        bullet.transform.position = transform.position;
        bullet.SetBullet(atkDmg, Vector3.back, bulletSpd);
    }
}

using UnityEngine;

public class Virus : Enemy
{
    protected override void Attack()
    {
        int bulletCnt = 10;

        firePos.LookAt(GameObject.Find("Player").transform.position);

        Quaternion firstRotation = firePos.rotation;

        firePos.Rotate(Vector3.up * -5 * (bulletCnt / 2));

        for (int i = 1; i <= bulletCnt; i++)
        {
            Bullet bullet = Instantiate(bulletObj);
            bullet.transform.position = firePos.position;
            bullet.SetBullet(atkDmg, firePos.forward, bulletSpd);
            firePos.Rotate(Vector3.up * 5);
        }

        firePos.rotation = firstRotation;
    }
}

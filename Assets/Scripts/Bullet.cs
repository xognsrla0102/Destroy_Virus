using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float dmg;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float spd;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = dir * spd;
    }

    public void SetBullet(float dmg, Vector3 dir, float spd)
    {
        this.dmg = dmg;
        this.dir = dir;
        this.spd = spd;
    }
}

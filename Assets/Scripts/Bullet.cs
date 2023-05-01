using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 7f;

    private float damageForce = 5f;
    public float DamageForce
    {
        get { return damageForce; }
        set
        {
            if (value > damageForce)
                damageForce = value;
        }
    }

    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void HitTarget()
    {
        target.GetComponent<Enemy>().TakeDamage(damageForce);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultProjectile : MonoBehaviour
{

    private Transform target;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float explosionRadius;
    [HideInInspector] public float damage;

    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    private void Start()
    {
        Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);
        transform.GetComponent<Rigidbody>().velocity = Vo;
    
    }
    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (transform.position.y < -0.2F)
        {
            HitTarget();
            return;
        }


    }


    private void HitTarget()
    {
        GameObject impactEffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance, 2f);

        Explode();

        Destroy(gameObject);
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);

    }

    void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage*0.7f);
    }

    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vxz /= 2.5f;
        Vy *= 2.5f; 
        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

}

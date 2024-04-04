using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    private Transform target;
    public float speed = 50f;
    public GameObject impactEffect;
    public float explosionRadius;
    public int damage;

    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {

            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
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
        foreach(Collider collider in hitObjects)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

}

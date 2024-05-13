using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    private Transform target;
    [SerializeField] private float speed = 50f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float explosionRadius;
    [HideInInspector] public float damage;

    public void GetTarget(Transform _target)
    {
        target = _target;
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (target == null)
        {

            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position + new Vector3(0f, 0.7f, 0f) - transform.position;
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
        enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage * 0.7f);
    }

}

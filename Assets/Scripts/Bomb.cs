using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    private Vector3 target;
    [SerializeField] private float speed = 70f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private float explosionRadius;
    [SerializeField] private int damage;
    [SerializeField] private int bombType; //standard/ice/venom
    public void GetTargetPoint(Vector3 _target)
    {
        target = _target;
    }

    void Start()
    {

    }

    private void Update()
    {
        if (target == null)
        {

            Destroy(gameObject);
            return;
        }

        Vector3 direction = target - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        
    }


    private void HitTarget()
    {
        GameObject impactEffectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(impactEffectInstance, 2f);
        Explode();
        Destroy(gameObject);
        
    }

    void Explode()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitObjects)
        {
            if (collider.tag == "Enemy")
            {
                if (bombType == 1)
                    Damage(collider.transform);
                if (bombType == 2)
                    Freeze(collider.transform);
                if (bombType == 3)
                    Intoxicate(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }

    void Freeze(Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().Freeze();
    }

    void Intoxicate(Transform enemy)
    {
        enemy.gameObject.GetComponent<Enemy>().Intoxicate();
    }
}

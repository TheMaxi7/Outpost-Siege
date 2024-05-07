using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    [SerializeField] private float speed = 70f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private int damage;
    public void GetTarget(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        
    }

    private void Update()
    {
        if (target == null) {

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
        Destroy(gameObject);
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
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
        Destroy(gameObject);
        Destroy(target.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    public float attackRange = 15f;
    public string enemyTag = "Enemy";
    public Transform turretHead;
    public float turretRotationSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targetDirection = target.position- transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * turretRotationSpeed).eulerAngles;
        turretHead.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0 )
        {
            Shoot();
            fireCountdown = 1f/fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

   
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= attackRange)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Shoot()
    {
        GameObject bulletShot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletShot.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.GetTarget(target);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{

    private Transform target;
    [Header("General")]
    public Animator animator;
    public int turretType;
    public float attackRange = 15f;
    public float turretRotationSpeed = 10f;

    [Header("Use bullets (Default)")]
    public bool isAOE = false;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPrefab;

    [Header("Use laser")]
    public bool useLaser = false;
    public float damageOverTime = 40f;
    public LineRenderer lineRenderer;

    [Header("Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform turretHead;
    public Transform firePoint;
    public int turretCost = 0;
    public bool canShoot = false;
    public Image rangeIndicator;
    public GameObject muzzleEffect;
    //public GameObject domeIndicator;

    private void Start()
    {

        if (canShoot)
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
        }
        rangeIndicator.rectTransform.sizeDelta = new Vector2(2 * attackRange, 2 * attackRange);
        //domeIndicator.transform.localScale = new Vector3(attackRange, attackRange, attackRange);
        rangeIndicator.enabled = true;
    }

    private void Update()
    {

        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                    lineRenderer.enabled = false;

            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                animator.SetTrigger("Fire");
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

    }

    void Laser()
    {
        target.GetComponent<Enemy>().TakeDamage(damageOverTime * Time.deltaTime);

        if (!lineRenderer.enabled)
            lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position + new Vector3(0f, 0.7f, 0f));
    }

    void LockOnTarget()
    {
        Vector3 targetDirection = target.position - firePoint.position;
        targetDirection += new Vector3(0f, 0.7f, 0);
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, Time.deltaTime * turretRotationSpeed).eulerAngles;
        turretHead.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(firePoint.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= attackRange)
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
        GameObject muzzleEffectInstance = Instantiate(muzzleEffect, firePoint.transform.position, firePoint.rotation);
        Destroy(muzzleEffectInstance, 2f);
        GameObject bulletShot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (!isAOE)
        {
            Bullet bullet = bulletShot.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.GetTarget(target);
            }
        }
        else
        {
            if (turretType == 2)
            {
                Missile missile = bulletShot.GetComponent<Missile>();
                if (missile != null)
                {
                    missile.GetTarget(target);
                }
            }
            if (turretType == 3)
            {
                CatapultProjectile catapultProjectile = bulletShot.GetComponent<CatapultProjectile>();
                if (catapultProjectile != null)
                {
                    catapultProjectile.GetTarget(target);
                }
            }

        }


    }

}

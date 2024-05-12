using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;


public class Turret : MonoBehaviour
{

    private Transform target;
    [Header("General")]
    [SerializeField] private Animator animator;
    [SerializeField] private int turretType;
    [SerializeField] public float turretDamage;
    [SerializeField] public float attackRange = 15f;
    [SerializeField] private float turretRotationSpeed = 10f;
    [HideInInspector] public int turretLevel = 1;


    [Header("Use bullets (Default)")]
    [SerializeField] private bool isAOE = false;
    [SerializeField] private float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField] public GameObject bulletPrefab;

    [Header("Use laser")]
    [SerializeField] private ParticleSystem muzzleLaserEffect;
    [SerializeField] private ParticleSystem LaserEffectHit;
    [SerializeField] private bool useLaser = false;
    [SerializeField] private float damageOverTime = 40f;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Setup Fields")]
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform firePoint;
    [SerializeField] public int turretCost = 0;
    [SerializeField] public int sellValue;
    [SerializeField] public int upgradeCost;
    [HideInInspector] public bool canShoot = false;
    [SerializeField] private Image rangeIndicator;
    [SerializeField] public GameObject range;
    [SerializeField] private GameObject muzzleEffect;
    
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

        sellValue = (int)(turretCost * 0.7);
        upgradeCost = (int)(turretCost * 1.3);
    }

    private void Update()
    {
        rangeIndicator.rectTransform.sizeDelta = new Vector2(2 * attackRange, 2 * attackRange);
        if (turretType == 1)
            bulletPrefab.GetComponent<Bullet>().damage = turretDamage;
        if (turretType == 2)
            bulletPrefab.GetComponent<Missile>().damage = turretDamage;
        if (turretType == 3)
            bulletPrefab.GetComponent<CatapultProjectile>().damage = turretDamage;
        if (turretType == 4)
            turretDamage = damageOverTime;

        if (target == null)
        {
            if (useLaser)
            {

                animator.SetBool("Lasering", false);
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    muzzleLaserEffect.Stop();
                    LaserEffectHit.Stop();
                }


            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {

            animator.SetBool("Lasering", true);
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
        {
            muzzleLaserEffect.Play();
            LaserEffectHit.Play();
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position + new Vector3(0f, 0.7f, 0f));
        LaserEffectHit.transform.position = target.position + new Vector3(0f, 0.7f, 0f);
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
            float enemyHealth = enemy.GetComponent<Enemy>().health;
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= attackRange && enemyHealth > 0)
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

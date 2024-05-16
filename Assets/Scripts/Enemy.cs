using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float distanceToTarget;
    private float distanceToEndWaypoint;
    public float health;
    [SerializeField] private float startingHealth;
    [SerializeField] public int value;
    [SerializeField] private float moveSpeed = 5;
    private float movementSpeed;
    [SerializeField] private GameObject deathEffect;
    private Animator animator;
    private CapsuleCollider coll;
    [SerializeField] private Image healthBar;
    private bool isDead;

    public Transform[] roadToExit;
    private bool hasEscaped;
    private Transform endWaypoint;
    private Transform nextWaypoint;
    private int waypointCounter = 0;
    private Vector3 moveDirection;
    private Quaternion lookRotation;
    private SkinnedMeshRenderer[] bodyParts;

    private float poisonTimeEffect = 2f;
    private float freezeTimeEffect = 5f;
    private void Start()
    {
        bodyParts = GetComponentsInChildren<SkinnedMeshRenderer>();
        endWaypoint = roadToExit[roadToExit.Length - 1];
        coll = GetComponent<CapsuleCollider>();
        movementSpeed = 0;
        nextWaypoint = roadToExit[0];
        startingHealth = health;
        moveDirection = (nextWaypoint.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(moveDirection);
        animator = GetComponent<Animator>();
        StartCoroutine(StartMoving());
    }

    private void Update()
    {
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        distanceToTarget = Vector3.Distance(transform.position, nextWaypoint.position);
        distanceToEndWaypoint = Vector3.Distance(transform.position, endWaypoint.position);
        if (distanceToTarget <= 1)
        {
            NextWaypoint();
            moveDirection = (nextWaypoint.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        }

        if ((distanceToEndWaypoint <= 1) && (!hasEscaped))
        {
            hasEscaped = true;
            UiManager.livesCount--;
            Destroy(gameObject, 1f);
        }
    }

    private void NextWaypoint()
    {
        waypointCounter++;
        if (waypointCounter < roadToExit.Length)
        {
            nextWaypoint = roadToExit[waypointCounter];
        }
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(2.7f);
        movementSpeed = moveSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startingHealth;
        if ((health <= 0) && !isDead)
        {
            Die();
        }
        else
            animator.SetTrigger("Hit");
    }

    public void Die()
    {
        UiManager.coinsCount += value;
        isDead = true;
        coll.enabled = false;
        movementSpeed = 0;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.5f);

    }
    public void Freeze()
    {
        StartCoroutine(Frozen());
    }

    public void Intoxicate()
    {
        StartCoroutine(Poison());
    }

    IEnumerator Poison()
    {
        movementSpeed = moveSpeed/2;
        animator.speed = 0.5f;
        int n = 0;
        while (n < 4)
        {
            float elapsedTime = 0;
            while (elapsedTime < poisonTimeEffect)
            {
                elapsedTime += Time.deltaTime;
                foreach (var part in bodyParts)
                {
                    part.material.SetColor("_BaseColor", new Color(Mathf.Lerp(38 * 0.005f, 0, elapsedTime / poisonTimeEffect), Mathf.Lerp(41 * 0.005f, 0, elapsedTime / poisonTimeEffect), 0, 0));
                    part.material.SetColor("_FresnelColor", new Color(Mathf.Lerp(3 * 0.005f, 0, elapsedTime / poisonTimeEffect), Mathf.Lerp(255 * 0.005f, 0, elapsedTime / poisonTimeEffect), 0, Mathf.Lerp(255 * 0.005f, 0, elapsedTime / poisonTimeEffect)));

                }
                yield return null;
            }
            TakeDamage(15f);
            n++;
        }
        movementSpeed = moveSpeed;
        animator.speed = 1;
    }

    public IEnumerator Frozen()
    {
        movementSpeed = 0;
        animator.enabled = false;
        float elapsedTime = 0;
        while (elapsedTime < freezeTimeEffect)
        {
            elapsedTime += Time.deltaTime;
            foreach (var part in bodyParts)
            {
                part.material.SetColor("_FresnelColor", new Color(0, Mathf.Lerp(255 * 0.005f, 0, elapsedTime / poisonTimeEffect), Mathf.Lerp(255 * 0.005f, 0, elapsedTime / poisonTimeEffect), Mathf.Lerp(255 * 0.005f, 0, elapsedTime / freezeTimeEffect)));
                part.material.SetColor("_BaseColor", new Color(0, Mathf.Lerp(72 * 0.005f, 0, elapsedTime / poisonTimeEffect), Mathf.Lerp(87 * 0.005f, 0, elapsedTime / poisonTimeEffect), Mathf.Lerp(255 * 0.005f, 0, elapsedTime / freezeTimeEffect)));

            }
            yield return null;
        }
        movementSpeed = moveSpeed;
        animator.enabled = true;
    }
}

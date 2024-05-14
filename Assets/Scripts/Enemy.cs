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
    [SerializeField] private int moveSpeed = 5;
    private int movementSpeed;
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
    private void Start()
    {
        coll = GetComponent<CapsuleCollider>();
        endWaypoint = roadToExit[roadToExit.Length - 1];
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
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float distanceToTarget;

    public float health;
    [SerializeField] private float startingHealth;
    [SerializeField] private int value;
    [SerializeField] private int moveSpeed = 5;
    private int movementSpeed;
    [SerializeField] private GameObject deathEffect;
    private Animator animator;
    private CapsuleCollider coll;
    [SerializeField] private Image healthBar;

    public Transform[] roadToExit;
    private Transform nextWaypoint;
    private int waypointCounter = 0;
    private Vector3 moveDirection;
    private Quaternion lookRotation;
    private void Start()
    {
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
        if (distanceToTarget <= 1)
        {
            NextWaypoint();
            moveDirection = (nextWaypoint.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

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
        if (health <= 0)
        {
            UiManager.coinsCount+=value;
            Die();
        }
        else
            animator.SetTrigger("Hit");
    }

    public void Die()
    {
        coll.enabled = false;
        movementSpeed = 0;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.5f);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float distanceToTarget;

    public float health;
    private float startingHealth;
    public int value;
    public int moveSpeed = 5;
    public GameObject deathEffect;

    public Image healthBar;

    public Transform[] roadToExit;
    private Transform nextWaypoint;
    private int waypointCounter = 0;
    private Vector3 moveDirection;
    private void Start()
    {
        nextWaypoint = roadToExit[0];
        startingHealth = health;
        moveDirection = (nextWaypoint.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        distanceToTarget = Vector3.Distance(transform.position, nextWaypoint.position);
        if (distanceToTarget <= 1)
        {
            NextWaypoint();
            moveDirection = (nextWaypoint.position - transform.position).normalized;
            
        }
    }

    private void NextWaypoint()
    {
        if (waypointCounter < roadToExit.Length)
        {
            waypointCounter++;
            nextWaypoint = roadToExit[waypointCounter];
        }
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
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
}

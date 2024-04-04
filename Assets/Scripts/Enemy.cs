using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float distanceToTarget;
    private NavMeshAgent agent;

    public float health;
    private float startingHealth;
    public int value;


    public GameObject deathEffect;

    public Image healthBar;
    private void Start()
    {
        startingHealth = health;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        //Debug.Log(distanceToTarget);
        if (distanceToTarget <= 5)
        {
            UiManager.livesCount--;
            Die();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private float distanceToTarget;
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, agent.destination);
        //Debug.Log(distanceToTarget);
        if (distanceToTarget <= 5)
        {
            Destroy(gameObject);
        }
    }
}

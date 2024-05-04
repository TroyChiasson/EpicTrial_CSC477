using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float attackRange;
    public Transform target; // Reference to the player's transform
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            // If the target is not set, try to find the player
            target = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= attackRange)
        {
            // Stop movement if close enough to attack
            navMeshAgent.isStopped = true;
        }
        else
        {
            // Continue movement if close enough to attack
            navMeshAgent.isStopped = false;
        }

        if (target != null)
        {
            // Move the enemy towards the player's position
            navMeshAgent.SetDestination(target.position);
        }
    }
}

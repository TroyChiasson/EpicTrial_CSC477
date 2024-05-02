using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (target == null)
        {
            // If the target is not set, try to find the player
            target = GameObject.FindGameObjectWithTag("Main Player").transform;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Move the enemy towards the player's position
            navMeshAgent.SetDestination(target.position);
        }
    }
}

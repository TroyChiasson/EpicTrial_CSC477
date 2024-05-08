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

            // Look at the player on the Y-axis only
            Vector3 lookDir = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0f);
            lookDir.Normalize();
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else
        {
            // Continue movement if not in attack range
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }
    }
}
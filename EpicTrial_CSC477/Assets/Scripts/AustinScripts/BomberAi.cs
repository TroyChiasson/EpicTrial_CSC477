using UnityEngine;
using UnityEngine.AI;

public class BomberAi : MonoBehaviour
{
    public float fuseTime = 5.0f; // Time until explosion (seconds)
    public float explosionRadius = 5.0f; // Radius of the explosion
    public GameObject explosionPrefab; // Prefab of the explosion effect

    private float timer = 0.0f;
    private bool exploded = false;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("MainPlayer").transform;
    }

    void Update()
    {
        // Update timer
        timer += Time.deltaTime;

        // Check for explosion conditions
        if (timer >= fuseTime || IsPlayerInRange())
        {
            Explode();
        }
    }

    bool IsPlayerInRange()
    {
        // Check if player is within explosion radius
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= explosionRadius;
    }

    void Explode()
    {
        if (!exploded)
        {
            // Prevent multiple explosions
            exploded = true;

            // Stop movement (if applicable)
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }

            // Spawn explosion effect
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Destroy the bomber
            Destroy(gameObject);
        }
    }
}
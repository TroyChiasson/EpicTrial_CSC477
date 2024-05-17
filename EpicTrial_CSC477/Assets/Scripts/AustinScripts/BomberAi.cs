using UnityEngine;
using UnityEngine.AI;

public class BomberAi : MonoBehaviour
{
    private AudioManager am;
    public GameObject Drop;
    public float fuseTime = 5f; // Time until explosion (seconds)
    public float explosionRadius = 5.0f; // Radius of the explosion
    public GameObject explosionPrefab; // Prefab of the explosion effect
    private float timer = 0.0f;
    private bool exploded = false;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    void Start() {
        fuseTime = Random.Range(4, 9);
        am = GameObject.Find("AM").GetComponent<AudioManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("MainPlayer").transform;
    }

    void Update()
    {
        // Update timer
        timer += Time.deltaTime;
        
        // Check for explosion conditions
        if (timer >= fuseTime)
        {
            Explode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("MainPlayer") || collision.gameObject.CompareTag("Shield") && !exploded) // Check collision with player
        {
            print("Yep");
            Explode();
        }
    }

    void Explode()
    {
        if (!exploded)
        {
            am.Play(6);

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
    void OnDestroy() {
        if (!exploded) {
            float dropChance = Random.value;
            if (dropChance < .5f){
                GameObject drop = Instantiate(Drop, transform.position, Quaternion.identity);
            }
        }
    }
}
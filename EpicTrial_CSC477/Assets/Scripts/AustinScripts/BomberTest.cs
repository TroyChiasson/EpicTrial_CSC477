using UnityEngine;
using UnityEngine.AI;

public class BomberTest : MonoBehaviour
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
    private Renderer rend; // Added a variable to store the Renderer component
    private float baseFlashSpeed = 1.0f; // Base flashing speed (slow)

    void Start()
    {
        fuseTime = Random.Range(4, 9);
        am = GameObject.Find("AM").GetComponent<AudioManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        rend = GetComponent<Renderer>(); // Get the Renderer component
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
        else
        {
            // Calculate a flashing speed based on timer progress (increases rapidly near explosion)
            float progress = Mathf.Clamp01(timer / fuseTime); // Progress between 0 and 1
            float flashSpeed = baseFlashSpeed + Mathf.Pow(progress, 3.0f); // Increase speed based on progress cubed

            // Alternate flash color (blue to white) using Mathf.PingPong with adjusted speed
            rend.material.color = Color.Lerp(Color.blue, Color.white, Mathf.PingPong(Time.time * flashSpeed, 1.0f));
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

    void OnDestroy()
    {
        if (!exploded)
        {
            float dropChance = Random.value;
            if (dropChance < .5f)
            {
                GameObject drop = Instantiate(Drop, transform.position, Quaternion.identity);
            }
        }
    }
}

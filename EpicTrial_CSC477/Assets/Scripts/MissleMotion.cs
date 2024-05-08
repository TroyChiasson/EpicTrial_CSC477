using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMotion : MonoBehaviour
{
    public GameObject player; // Assign the player transform in the inspector
    public GameObject explosionPrefab; // Prefab of the explosion effect
    public float speed = 10.0f; // Missile speed
    public float lifetime = 5.0f; // Time in seconds before self-destruction
    private float timer = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer");
        if (player == null)
        {
            Debug.LogError("Missile: Player with tag 'MainPlayer' not found!");
        }
    }

    void Update()
    {
        // Update timer
        timer += Time.deltaTime;

        // Check for self-destruction after lifetime
        if (timer >= lifetime)
        {
            Explode();
            return;
        }

        // Move towards player
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Track player (ignore Y-axis for 2D)
        Vector3 lookDirection = player.transform.position - transform.position;
        lookDirection.y = 0f; // Ignore Y-axis for 2D
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // Spawn explosion and destroy missile
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);

        // Create temporary collision sphere for explosion (optional)
        StartCoroutine(CreateExplosionSphere());
    }

    IEnumerator CreateExplosionSphere()
    {
        // Create a sphere with the Explosion tag
        GameObject explosionSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        explosionSphere.transform.position = transform.position;
        explosionSphere.transform.localScale = Vector3.one * 4f; // Adjust size as needed
        explosionSphere.tag = "Explosion";
        explosionSphere.GetComponent<MeshRenderer>().enabled = false; // Disable mesh renderer for invisibilit

        // Destroy the sphere after one frame (simulates a brief collision event)
        yield return new WaitForSeconds(0.0f);
        Destroy(explosionSphere);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitAroundPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player object in the inspector
    [SerializeField] private float orbitDistance = 1f; // Set the desired distance from the player
    [SerializeField] private float rotationSpeed = 100f; // Adjust the rotation speed

    public GameObject Bullettest;
    public Transform firingPoint;
    public static bool bulletFired;

    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.position;
        direction.z = 0f; // Ensure rotation happens in 2D plane

        // Calculate desired rotation based on player and mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object around the player at a set distance (with Z at 0)
        Vector3 desiredPosition = player.position + Quaternion.Euler(0, 0, angle) * Vector3.right * orbitDistance;
        desiredPosition.z = 0f; // Force Z to 0
        transform.position = desiredPosition;

        // Apply rotation smoothly
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        print("Yo");
        if (other.gameObject.tag == "Bullet")
        {
            Reflect();
        }
    }

    void Reflect()
    {
        Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        GameObject firedBullet = Instantiate(Bullettest, enemyPos, Quaternion.identity);
        Vector3 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.position;
        firedBullet.GetComponent<Rigidbody>().velocity = direction * 2f;
    }
}

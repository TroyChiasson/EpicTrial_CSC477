using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitAroundPlayer : MonoBehaviour
{
    [SerializeField] private Transform player; // Assign the player object in the inspector
    [SerializeField] private float orbitDistance = 1f; // Set the desired distance from the player
    [SerializeField] private float rotationSpeed = 100f; // Adjust the rotation speed

    void Update()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - player.position;
        direction.z = 0f; // Ensure rotation happens in 2D plane

        // Calculate desired rotation based on player and mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the object around the player at a set distance
        transform.position = player.position + (Quaternion.Euler(0, 0, angle) * Vector3.right) * orbitDistance;

        // Apply rotation smoothly
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }
}
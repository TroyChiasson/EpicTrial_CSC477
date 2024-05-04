using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest1 : MonoBehaviour
{
    public GameObject BulletTest;
    public float fireDelay = 3.0f; // Time between shots
    public float minFireDelay = 1.0f; // Minimum time to fire if player is in range
    public float fireTime = 0f;
    public Transform firingPoint;
    public Transform player; // Assign the player transform in the inspector
    public float attackRange = 10.0f; // Distance to fire at player

    void Start()
    {
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        // Check if player is in range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // Player is in range, reset fire timer or fire if ready
            if (fireTime >= fireDelay)
            {
                fireBullet();
                fireTime = 0.0f;
            }
            else
            {
                fireTime += Time.deltaTime;
            }
        }
        else
        {
            // Player is not in range, use minimum fire delay
            if (fireTime >= minFireDelay)
            {
                fireBullet();
                fireTime = 0.0f;
            }
            else
            {
                fireTime += Time.deltaTime;
            }
        }
    }

    void fireBullet()
    {
        Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y + 1.1f, firingPoint.position.z);
        GameObject firedBullet = Instantiate(BulletTest, enemyPos, Quaternion.identity);
        Vector3 direction = player.position - firingPoint.position; // Shoot towards player
        firedBullet.GetComponent<Rigidbody>().velocity = direction.normalized * 20f; // Use normalized for consistent speed
    }
}
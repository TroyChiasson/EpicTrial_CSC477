using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{

    public Transform target; // Reference to the player's transform
    public GameObject BulletTest;
    public GameObject Drop;
    public float maxFireDelay = 5.0f; // Maximum time between shots (at range)
    public float minFireDelay = 1.0f; // Minimum time between shots (close range)
    public AnimationCurve fireRateCurve; // Curve to adjust fire delay based on distance
    public float attackRange = 10.0f; // Distance to fire at player
    public Transform firingPoint;
    public Transform player; // Assign the player transform in the inspector

    private float fireTime = 0f;
    public int bossHealth = 100; // Added boss health

    void Start()
    {
       
        if (target == null)
        {
            // If the target is not set, try to find the player
            target = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        }
        // Ensure fireRateCurve exists (optional)
        if (fireRateCurve == null)
        {
            fireRateCurve = new AnimationCurve();
        }
    }

    void Update()
    {
        Fire();

       
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= attackRange)
        {
            //Phase 1
            if (bossHealth >= 6)
            {
                // Look at the player on the Y-axis only
                Vector3 lookDir = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0f);
                lookDir.Normalize();
                transform.rotation = Quaternion.LookRotation(lookDir);
            }

            //Phase 2
            if (bossHealth < 6)
            {
                transform.Rotate(Vector3.up * 200 * Time.deltaTime);
                maxFireDelay = .5f;
                minFireDelay = .1f;
            }
           
        }

    }

    void Fire()
    {
        // Check if player is in range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calculate fire delay based on distance and curve
        float fireDelay = Mathf.Lerp(maxFireDelay, minFireDelay, fireRateCurve.Evaluate(distanceToPlayer / attackRange));

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

    void fireBullet()
    {
        Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        GameObject firedBullet = Instantiate(BulletTest, enemyPos, Quaternion.identity);
        Vector3 direction = firingPoint.position - transform.localPosition; // Shoot towards player
        firedBullet.GetComponent<Rigidbody>().velocity = direction.normalized * 20f; // Use normalized for consistent speed
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with ShieldBullet tag
        if (collision.gameObject.CompareTag("ShieldBullet"))
        {
            TakeDamage(1); // Take damage from ShieldBullet
        }
    }

    void TakeDamage(int damage)
    {
        bossHealth -= damage;

        // Check if boss is dead (optional)
        if (bossHealth <= 0)
        {
            Destroy(this.gameObject);
            // Add additional logic for boss death here
        }
    }

    void OnDestroy()
    {
        Vector3 enemyPosDrop = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        float dropChance = Random.value;
        if (dropChance < 1f)
        {
            GameObject drop = Instantiate(Drop, enemyPosDrop, Quaternion.identity);
        }
    }
}

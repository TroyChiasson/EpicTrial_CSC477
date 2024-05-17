using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotgunTest : MonoBehaviour
{
    public GameObject BulletTest;
    public GameObject Drop;
    public float maxFireDelay = 5.0f; // Maximum time between shots (at range)
    public float minFireDelay = 1.0f; // Minimum time between shots (close range)
    public AnimationCurve fireRateCurve; // Curve to adjust fire delay based on distance
    public float attackRange = 10.0f; // Distance to fire at player
    public Transform firingPoint;
    public Transform firingPoint2;
    public Transform firingPoint3;
    public Transform player; // Assign the player transform in the inspector

    private float fireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        // Ensure fireRateCurve exists (optional)
        if (fireRateCurve == null)
        {
            fireRateCurve = new AnimationCurve();
        }
    }

    void Update()
    {
        Fire();
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
        Vector3 enemyPos2 = new Vector3(firingPoint2.position.x+1.1f, firingPoint2.position.y, firingPoint2.position.z);
        Vector3 enemyPos3 = new Vector3(firingPoint3.position.x-1.1f, firingPoint3.position.y, firingPoint3.position.z);
        GameObject firedBullet = Instantiate(BulletTest, enemyPos, Quaternion.identity);
        GameObject firedBullet2 = Instantiate(BulletTest, enemyPos2, Quaternion.identity);
        GameObject firedBullet3 = Instantiate(BulletTest, enemyPos3, Quaternion.identity);
        Vector3 direction1 = player.position - firingPoint.position; // Shoot towards player
        Vector3 direction2 = player.position - firingPoint2.position; // Shoot towards player
        Vector3 direction3 = player.position - firingPoint3.position; // Shoot towards player
        firedBullet.GetComponent<Rigidbody>().velocity = direction1.normalized * 10f; // Use normalized for consistent speed
        firedBullet2.GetComponent<Rigidbody>().velocity = direction2.normalized * 10f;
        firedBullet3.GetComponent<Rigidbody>().velocity = direction3.normalized * 10f;
    }

    void OnDestroy() {
        Vector3 enemyPosDrop = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        float dropChance = Random.value;
        if (dropChance < .2f){
            GameObject drop = Instantiate(Drop, enemyPosDrop, Quaternion.identity);
        }
    }
}

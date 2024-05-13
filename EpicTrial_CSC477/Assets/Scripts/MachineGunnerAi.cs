using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunnerAi : MonoBehaviour
{
    public AudioManager am;
    public GameObject BulletTest;
    public GameObject Drop;
    public float maxFireDelay = 5.0f; // Maximum time between shots (at range)
    public float minFireDelay = 1.0f; // Minimum time between shots (close range)
    public AnimationCurve fireRateCurve; // Curve to adjust fire delay based on distance
    public float attackRange = 10.0f; // Distance to fire at player
    public Transform firingPoint;
    public Transform player; // Assign the player transform in the inspector

    private float fireTime = 0f;
    private int bulletsRemaining = 30; // Ammo remaining in clip
    private bool isReloading = false; // Flag to indicate reloading state
    private float reloadStartTime = 0f; // Time when reload started

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
        if (isReloading)
        {
            HandleReload();
        }
        else
        {
            Fire();
        }
    }

    void Fire()
    {
        // Check if player is in range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Calculate fire delay based on distance and curve
        float fireDelay = Mathf.Lerp(maxFireDelay, minFireDelay, fireRateCurve.Evaluate(distanceToPlayer / attackRange));

        if (fireTime >= fireDelay && bulletsRemaining > 0)
        {
            fireBullet();
            bulletsRemaining--;
            fireTime = 0.0f;
        }
        else if (bulletsRemaining == 0)
        {
            StartReload();
        }
        else
        {
            fireTime += Time.deltaTime;
        }
    }

    void StartReload()
    {
        isReloading = true;
        reloadStartTime = Time.time; // Store start time for reload duration
    }

    void HandleReload()
    {
        if (Time.time - reloadStartTime >= 3.0f) // Check if reload time has passed
        {
            isReloading = false;
            bulletsRemaining = 30; // Refill ammo
        }
    }

    void fireBullet()
    {
        am.Play(0);
        Vector3 enemyPos = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        GameObject firedBullet = Instantiate(BulletTest, enemyPos, Quaternion.identity);
        Vector3 direction = firingPoint.position - transform.localPosition; // Shoot towards player
        firedBullet.GetComponent<Rigidbody>().velocity = direction.normalized * 20f; // Use normalized for consistent speed
    }

    void OnDestroy()
    {
        Vector3 enemyPosDrop = new Vector3(firingPoint.position.x, firingPoint.position.y, firingPoint.position.z);
        float dropChance = Random.value;
        if (dropChance < .5f)
        {
            GameObject drop = Instantiate(Drop, enemyPosDrop, Quaternion.identity);
        }
    }
}

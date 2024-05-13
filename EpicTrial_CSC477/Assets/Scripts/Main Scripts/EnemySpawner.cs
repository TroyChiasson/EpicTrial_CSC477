using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs (type1, type2, type3, type4)
    public float spawnRadius = 5.0f; // Radius around spawner for random spawn position
    public float spawnCooldown = 20.0f; // Time between enemy spawns
    public int maxEnemies = 10; // Maximum number of enemies allowed on the level

    public int maxSpawned = 10; // Maximum number of enemies that can be spawned from the spawner
    private int spawned;

    private float timer = 0.0f; // Cooldown timer

    void Start()
    {
        spawned = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if enemy count is below the limit before spawning
        if (timer >= spawnCooldown && CountEnemies() < maxEnemies && spawned < maxSpawned)
        {
            SpawnEnemy();
            timer = 0.0f; // Reset cooldown timer
            spawned++;
        }
    }

    int CountEnemies()
    {
        // Find all GameObjects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Return the number of enemies found
        return enemies.Length;
    }

    void SpawnEnemy()
    {
        // Choose a random enemy prefab
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // Get a random spawn position within the radius
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = transform.position.y; // Maintain same Y position as spawner

        // Instantiate the enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

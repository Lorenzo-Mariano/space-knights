using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDelay = 3f;
    private float nextSpawnTime;
    private int enemiesSpawned = 0;
    public int maxEnemies = 50;
    public GameObject player; // Reference to the player object

    void Update()
    {
        // Check if player object is destroyed
        if (player == null || player.Equals(null)) 
        {
			Debug.Log("Player is destroyed, stopping spawner.");
            return; // Exit if player object is destroyed
        }

        // Continue spawning enemies if the player exists and maxEnemies not reached
        if (Time.time > nextSpawnTime && enemiesSpawned < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    void SpawnEnemy()
    {
        // Ensure that the player is still valid (not destroyed) before spawning
        if (player != null && player.gameObject != null)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemiesSpawned++;
        }
    }
}

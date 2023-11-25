using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomEnemySpawner : MonoBehaviour
{
    public Tilemap Tilemap; // Assign the wall Tilemap in the Inspector
    public GameObject randomEnemyPrefab; // Assign your RandomEnemy prefab in the Inspector
    public float spawnInterval = 5f; // Time in seconds between each spawn
    public Vector2Int spawnAreaSize = new Vector2Int(3, 3); // Size of the area around the player to spawn enemies
    private SubController player;
    private float timeSinceLastSpawn;

    private void Start()
    {
        // If you want to spawn immediately, call SpawnRandomEnemy here or set timeSinceLastSpawn to spawnInterval.
    }

    private void Update()
    {
        // Accumulate time and check if it's time to spawn
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnRandomEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnRandomEnemy()
    {
        if (Tilemap == null)
        {
            Debug.LogError("Tilemap is not assigned in the Inspector.");
            return; // Exit the function if Tilemap is not assigned
        }

        player = FindObjectOfType<SubController>(); // Find the player
        if (player != null)
        {
            // Get a random wall position near the player
            Vector3Int playerCellPosition = Tilemap.WorldToCell(player.transform.position);
            Vector3 spawnPosition = FindRandomWallPositionNearPlayer(playerCellPosition);

            // Instantiate an instance of the RandomEnemy prefab at the found position
            Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

    private Vector3 FindRandomWallPositionNearPlayer(Vector3Int playerCellPosition)
    {
        BoundsInt bounds = new BoundsInt(
            playerCellPosition.x - spawnAreaSize.x / 2,
            playerCellPosition.y - spawnAreaSize.y / 2,
            playerCellPosition.z,
            spawnAreaSize.x,
            spawnAreaSize.y,
            1);

        // Randomly choose a position within the given area
        for (int i = 0; i < 100; i++) // To prevent an infinite loop, we try 100 times
        {
            Vector3Int randomPosition = new Vector3Int(
                Random.Range(bounds.xMin, bounds.xMax),
                Random.Range(bounds.yMin, bounds.yMax),
                0);

            if (Tilemap.HasTile(randomPosition) && Tilemap.GetTile(randomPosition).name == "Wall")
            {
                // Adjust the position to the center of the Tile
                return Tilemap.CellToWorld(randomPosition) + new Vector3(0.5f, 0.5f, 0);
            }
        }

        // If no suitable position is found, return the player's current position
        // You may want to handle this case differently
        return player.transform.position;
    }
}
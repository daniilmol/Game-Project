using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawner : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;
    [Header("Enemies")]
    [SerializeField] GameObject enemySpawner;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float enemyCount;
    [Header("Stage")]
    [SerializeField] GameObject stage;
    float xOffset = -28;
    float yOffset = 8.5f;
    float zOffset = 3.5f;
    void Start()
    {
        SpawnPlayer();
        //SpawnEnemies();
    }

    private void SpawnPlayer() {
        Instantiate(player, new Vector3(1 + xOffset, yOffset, 1 + zOffset), Quaternion.identity);
    }

    private void SpawnEnemies() {
        Vector3[] positions = {new Vector3(5 + xOffset, 0 + yOffset, 5 + zOffset), new Vector3(-5 + xOffset, 0 + yOffset, -5 + zOffset), new Vector3(10 + xOffset, 0 + yOffset, 10 + zOffset), new Vector3(7 + xOffset, 0 + yOffset, 4 + zOffset), new Vector3(4 + xOffset, 0 + yOffset, 7 + zOffset)};
        GameObject[] spawnLocations = new GameObject[positions.Length];
        for (int i = 0; i < positions.Length; i++) {
            GameObject spawnLocation = new GameObject();
            spawnLocation.transform.position = positions[i];
            Instantiate(spawnLocation, positions[i], Quaternion.identity);
            spawnLocations[i] = spawnLocation;
        }
        SpawnEnemies spawnEnemies = enemySpawner.GetComponent<SpawnEnemies>();
        spawnEnemies.setEnemyCount(3);
        spawnEnemies.setEnemySpawns(spawnLocations);
        spawnEnemies.setBulletPrefab(bulletPrefab);
        spawnEnemies.setPlayer(player);
        Instantiate(enemySpawner, new Vector3(0, 0, 0), Quaternion.identity);
        //spawnEnemies.InitializeSpawner();
    }


}

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
    void Start()
    {
        SpawnStage();
        SpawnPlayer();
        SpawnEnemies();
    }

    private void SpawnPlayer() {
        Instantiate(player, new Vector3(1, 0, 1), Quaternion.identity);
    }

    private void SpawnStage() {
        Instantiate(stage, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void SpawnEnemies() {
        Vector3[] positions = {new Vector3(5, 0, 5), new Vector3(-5, 0, -5), new Vector3(10, 0, 10), new Vector3(7, 0, 4), new Vector3(4, 0, 7)};
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
        spawnEnemies.InitializeSpawner();
    }


}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : RandomSpawner
{
    [Tooltip("Add invisible game objects to this array, these objects will represent the different spawnable locations")]
    [SerializeField] GameObject[] enemySpawns;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] enemyTypes;

    [Tooltip("How many objects to spawn. Cannot be greater than the array of spawnable locations")]
    [Range(0, 5)] [SerializeField] int enemyCount;
    public static int numberOfEnimies;

    public void Start(){
        Spawn(enemyCount, enemyTypes, enemySpawns, player, bulletPrefab);
        numberOfEnimies = enemyCount;
    }

    public void setEnemyCount(int enemyCount) {
        this.enemyCount = enemyCount;
    }

    public void setEnemySpawns(GameObject[] enemySpawns) {
        this.enemySpawns = enemySpawns;
    }

    public void setBulletPrefab(GameObject bulletPrefab) {
        this.bulletPrefab = bulletPrefab;
    }

    public void setPlayer(GameObject player) {
        this.player = player;
    }
}

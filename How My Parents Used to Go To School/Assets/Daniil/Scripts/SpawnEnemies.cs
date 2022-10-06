using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : RandomSpawner
{
    [Tooltip("Add invisible game objects to this array, these objects will represent the different spawnable locations")]
    [SerializeField] GameObject[] enemySpawns;
    [SerializeField] GameObject enemy;

    [Tooltip("How many objects to spawn. Cannot be greater than the array of spawnable locations")]
    [Range(0, 5)] [SerializeField] int enemyCount;

    void Start()
    {
        Spawn(enemyCount, enemy, enemySpawns);
    }
}

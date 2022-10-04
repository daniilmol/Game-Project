using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEnemies : RandomSpawner
{
    [SerializeField] GameObject[] enemySpawns;
    [SerializeField] GameObject enemy;

    [Range(0, 5)] [SerializeField] int enemyCount;

    void Start()
    {
        Spawn(enemyCount, enemy, enemySpawns);
    }
}

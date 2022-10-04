using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObstacles : RandomSpawner 
{
    [SerializeField] GameObject[] obstaceSpawns;
    [SerializeField] GameObject obstacle;

    [Range(0, 2)] [SerializeField] int enemyCount;

    void Start()
    {
        Spawn(enemyCount, obstacle, obstaceSpawns);
    }
}

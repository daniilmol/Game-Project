using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnObstacles : RandomSpawner 
{
    [Tooltip("Add invisible game objects to this array, these objects will represent the different spawnable locations")]
    [SerializeField] GameObject[] obstaceSpawns;
    [SerializeField] GameObject obstacle;

    [Tooltip("How many objects to spawn. Cannot be greater than the array of spawnable locations")]
    [Range(0, 2)] [SerializeField] int obstacleCount;

    void Start()
    {
        Spawn(obstacleCount, obstacle, obstaceSpawns);
    }
}

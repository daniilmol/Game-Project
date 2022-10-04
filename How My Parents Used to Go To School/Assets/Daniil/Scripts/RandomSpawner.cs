using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomSpawner : MonoBehaviour
{
    protected void Spawn(int count, GameObject prefab, GameObject[] spawns)
    {
        for (int i = 0; i < count; i++)
        {
            int indexRemove = Random.Range(0, spawns.Length);
            Instantiate(prefab, spawns[indexRemove].transform);
            spawns = spawns.Where((source, index) => index != indexRemove).ToArray();
        }
    }
}

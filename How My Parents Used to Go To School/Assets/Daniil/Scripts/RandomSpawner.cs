using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomSpawner : MonoBehaviour
{
    /*
     * Spawns any prefab object randomly in a list of empty areas
     */
    protected void Spawn(int count, GameObject[] prefab, GameObject[] spawns, GameObject player, GameObject bulletPrefab)
    {
        for (int i = 0; i < count; i++)
        {
            int indexRemove = Random.Range(0, spawns.Length);
            int enemyIndex = Random.Range(0, prefab.Length);
            GameObject instantiated = (GameObject)Instantiate(prefab[2], spawns[indexRemove].transform);
            if (instantiated.tag == "Enemy") {
                instantiated.GetComponent<Enemy>().SetTarget(player, bulletPrefab);
            }
            spawns = spawns.Where((source, index) => index != indexRemove).ToArray();
        }

    }
    protected void Spawn(int count, GameObject prefab, GameObject[] spawns, GameObject player)
    {
        for (int i = 0; i < count; i++)
        {
            int indexRemove = Random.Range(0, spawns.Length);
            GameObject instantiated = (GameObject)Instantiate(prefab, spawns[indexRemove].transform);
            spawns = spawns.Where((source, index) => index != indexRemove).ToArray();
        }

    }
}

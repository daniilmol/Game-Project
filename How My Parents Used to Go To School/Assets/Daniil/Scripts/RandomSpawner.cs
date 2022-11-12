using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RandomSpawner : MonoBehaviour
{
    /*
     * Spawns any prefab object randomly in a list of empty areas
     */
    protected void Spawn(int count, GameObject[] prefab, GameObject[] spawns, GameObject player, GameObject bulletPrefab, float scale)
    {
        int healerNumber = count / 5;
        int swordsmanNumber = count / 2;
        int gunnerNumber = count / 3;
        int tripleGunnerNumber = count / 3;
        int tankNumber = count / 3;
        for (int i = 0; i < count; i++)
        {
            int indexRemove = Random.Range(0, spawns.Length);
            int enemyIndex = Random.Range(0, prefab.Length);
            GameObject instantiated = (GameObject)Instantiate(prefab[enemyIndex], spawns[indexRemove].transform);
            if (instantiated.tag == "Enemy") {
                instantiated.GetComponent<Enemy>().SetTarget(player, bulletPrefab);
                instantiated.GetComponent<EnemyStatContainer>().IncreaseStats(scale);
            }
            spawns = spawns.Where((source, index) => index != indexRemove).ToArray();
            if (instantiated.TryGetComponent(out Swordsman s)) {
                if(--swordsmanNumber <= 0){
                                        print("Removing swordsman from suggestion");
                    prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                }
            }if (instantiated.TryGetComponent(out Healer h)) {
                if(--healerNumber <= 0){
                                        print("Removing healer from suggestion");
                    prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                }
            }if (instantiated.TryGetComponent(out Gunner g)) {
                if(--gunnerNumber <= 0){
                                        print("Removing gunner from suggestion");
                    prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                }
            }if (instantiated.TryGetComponent(out TripleGunner tg)) {
                if(--tripleGunnerNumber <= 0){
                                        print("Removing triple gunner from suggestion");
                    prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                }
            }if (instantiated.TryGetComponent(out Tank t)) {
                if(--tankNumber <= 0){
                    print("Removing tank from suggestion");
                    prefab = prefab.Where((source, index) => index != enemyIndex).ToArray();
                }
            }
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

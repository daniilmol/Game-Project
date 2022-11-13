using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : RandomSpawner
{
    [Tooltip("Add invisible game objects to this array, these objects will represent the different spawnable locations")]
    [SerializeField] GameObject[] enemySpawns;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject[] enemyTypes;
    [SerializeField] float difficultyScaling;
    [SerializeField] NavMeshSurface[] surfaces;
    public static float numberOfRoomsCleared = 0;
    

    [Tooltip("How many objects to spawn. Cannot be greater than the array of spawnable locations")]
    [Range(0, 5)] [SerializeField] int enemyCount;
    public static int numberOfEnimies;

    public void Start(){
        for(int i = 0; i < surfaces.Length; i++){
            surfaces[i].BuildNavMesh();
        }
        print("DIFFICULTY SCALE: " + PlayerPrefs.GetFloat("Scale"));
        difficultyScaling = PlayerPrefs.GetFloat("Scale");
        Spawn(enemyCount, enemyTypes, enemySpawns, player, bulletPrefab, difficultyScaling);
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

    public float GetScale(){
        return difficultyScaling + numberOfRoomsCleared * 0.1f;
    }

    public void SetScale(float scale){
        difficultyScaling = scale;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveTrigger : MonoBehaviour
{
    public string sceneName;
    public SpawnEnemies spawnEnemies;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StatContainer sc = other.gameObject.GetComponent<StatContainer>();
            SpawnEnemies.numberOfRoomsCleared++;
            PlayerPrefs.SetFloat("Damage", sc.GetDamage());
            PlayerPrefs.SetFloat("Speed", sc.GetSpeed());
            PlayerPrefs.SetFloat("AttackSpeed", sc.GetAttackSpeed());
            PlayerPrefs.SetFloat("Health", sc.GetMaxHealth());
            SceneManager.LoadScene(sceneName);
        }
    }
}

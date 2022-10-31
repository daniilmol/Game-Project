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
            PlayerPrefs.SetFloat("Scale", spawnEnemies.GetScale() + 0.1f);
            SceneManager.LoadScene(sceneName);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuSceneUIController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        PlayerPrefs.SetFloat("Scale", 1f);
        PlayerPrefs.SetFloat("Damage", 1f);
        PlayerPrefs.SetFloat("Speed", 1f);
        PlayerPrefs.SetFloat("AttackSpeed", 1f);
        PlayerPrefs.SetFloat("Health", 10f);

        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharectorSceneUIController : MonoBehaviour
{
    public GameObject panel;
    public bool stopFlag;

    private void Awake()
    {
        InitGame();
        stopFlag = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopGame();
        }

        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene("Charector", LoadSceneMode.Single);
        //}
    }

    // initial UI setting
    public void InitGame()
    {
        Time.timeScale = (1);
        // Cursor.lockState = CursorLockMode.Locked;
        SetPanleActive(false);
        stopFlag = true;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // display or hiden panel
    private void SetPanleActive(bool flag)
    {
        panel.SetActive(flag);
    }

    // stop the game, unlock cursor
    private void StopGame()
    {
        Time.timeScale = (0);
        // Cursor.lockState = CursorLockMode.None;
        SetPanleActive(true);
        stopFlag = true;
    }

    // restart the game and relock cursor
    public void RestartGame()
    {
        Time.timeScale = (1);
        // Cursor.lockState = CursorLockMode.Locked;
        SetPanleActive(false);
        stopFlag = false;
    }
}

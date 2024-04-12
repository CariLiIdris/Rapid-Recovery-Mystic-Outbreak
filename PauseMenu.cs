using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool PlayerIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject playerUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        playerUI.SetActive(true);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        playerUI.SetActive(false);       
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadCheckpoint()
    {
        ;
    }

    public void LoadInventory()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void LoadOptions()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SettingsMenu");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StartMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}

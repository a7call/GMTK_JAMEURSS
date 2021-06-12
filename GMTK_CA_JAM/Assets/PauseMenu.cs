using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject settingsWindow;
    public GameObject pauseMenuUI;


    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        settingsWindow.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
    }



}

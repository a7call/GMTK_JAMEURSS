using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;




public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int nbPoints;

    private GameObject canvasPlayer;
    

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        canvasPlayer = GameObject.FindGameObjectWithTag("Canvas");
        DontDestroyOnLoad(canvasPlayer);

        
    }

    

    //Rest of your class code
    public void AddPoints(int Points)
    {
        nbPoints += Points;
    }

    public void GameOver()
    {
        //GameOver
        //Time.timeScale = 0;
        //Display GameOver
    }

    #region MainMenu

    public void PlayGame()
    {
        SceneManager.LoadScene("SceneVal");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion MainMenu


    #region Pause
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
        QuitSettings();
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void QuitSettings()
    {
        settingsWindow.SetActive(false);
        SetKey();
    }

    public void QuitPause()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
    }

    #endregion Pause


    #region Settings

    public AudioMixer audioMixer;
    public Player player1;
    public static KeyCode rightKeyP1=KeyCode.D;
    public static KeyCode leftKeyP1=KeyCode.A;


    public void SetLevelMusic(float sliderValue)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelEffect(float sliderValue)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(sliderValue) * 20);
    }

    private void SwitchToAD()
    {
        rightKeyP1 = KeyCode.D;
        leftKeyP1 = KeyCode.A;
    }

    private void SwitchToQD()
    {
        rightKeyP1 = KeyCode.D;
        leftKeyP1 = KeyCode.Q;
    }

    public void SetKey()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        player1.rightKey = rightKeyP1;
        player1.leftKey = leftKeyP1;
    }




    #endregion Settings

}



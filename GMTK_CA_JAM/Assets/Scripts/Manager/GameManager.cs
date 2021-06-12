using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public int nbPoints = 0;
    public TextMeshProUGUI countText;

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

        DisplayPoints();
        
    }


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

        if (shakeTimeRemaining > 0 && isShaking) GlobalShake();
    }

    #region Points

    public float PointsShakePower;
    public float PointsShakeTime;
    public float rotationMultiplier;

    public void AddPoints(int Points)
    {
        nbPoints += Points;
        DisplayPoints();
    }

    public void DisplayPoints()
    {
        countText.text = nbPoints.ToString();
        StartShakeG(PointsShakeTime, PointsShakePower);

    }

    public void DisplayWithoutShake()
    {
        countText.text = nbPoints.ToString();
    }


    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;
    private float shakeRotation;
    private bool isShaking;



    public void StartShakeG(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;

        countText.fontSize = 60;

        isShaking = true;
    }

    private void GlobalShake()
    {


        shakeTimeRemaining -= Time.deltaTime;


        countText.transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));


        shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);

        if (shakeTimeRemaining <= 0)
        {
            countText.transform.rotation = Quaternion.identity;
            isShaking = false;
            countText.fontSize = 40;
        }
    }
    #endregion Points



    #region MainMenu

    

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion MainMenu


    #region Pause
    public static bool isGamePaused = false;
    public GameObject settingsWindow;
    public GameObject pauseMenuUI;


   


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


    #region GameOver

    public GameObject GameOverUI;

    public void GameOver()
    {

        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }

    public void Retry()
    {
        nbPoints = 0;
        DisplayWithoutShake();
        GameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    #endregion GameOver



    #region Settings

    public AudioMixer audioMixer;
    private Player player1;
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



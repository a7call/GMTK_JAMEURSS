using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private int nbPoints;

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
    }

    private void Start()
    {
        //AudioManagerMusic.instance.Play("");
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
}



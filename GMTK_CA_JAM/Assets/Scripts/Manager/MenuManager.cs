using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    

    public void PlayGame()
    {
        SceneManager.LoadScene("SceneXav");
       
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    public AudioMixer audioMixer;
    private Player player1;



    public void SetLevelMusic(float sliderValue)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelEffect(float sliderValue)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(sliderValue) * 20);
    }

    public void SwitchToAD()
    {
        GameManager.rightKeyP1 = KeyCode.D;
        GameManager.leftKeyP1 = KeyCode.A;
        
    }

    public void SwitchToQD()
    {
        GameManager.rightKeyP1 = KeyCode.D;
        GameManager.leftKeyP1 = KeyCode.Q;
    }

  
}

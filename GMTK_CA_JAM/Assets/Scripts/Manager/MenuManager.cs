using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        AudioManagerEffect.instance.StopPlayingAll();
        AudioManagerMusic.instance.Play("MusicV1");
    }
    public void PlayGame()
    {
        GameManager.Instance.nbPoints = 0;
        GameManager.Instance.DisplayWithoutShake();
        AudioManagerMusic.instance.StopPlaying("MusicV1");
        AudioManagerMusic.instance.Play("MusicV1");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    public AudioMixer audioMixer;
   



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

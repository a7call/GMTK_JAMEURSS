using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        SetVolumeSlider();
        AudioManagerEffect.instance.StopPlayingAll();
        AudioManagerMusic.instance.Play("Menu");
       
    }
    public void PlayGame()
    {
        GameManager.Instance.nbPoints = 0;
        GameManager.Instance.DisplayWithoutShake();
        AudioManagerMusic.instance.StopPlaying("Menu");
        AudioManagerMusic.instance.Play("MusicV1");
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );

        GameManager.Instance.SetVolumeSlider();
        
    }



    public void QuitGame()
    {
        Application.Quit();
    }

    public AudioMixer audioMixer;

    public Slider sliderMusic, sliderEffect;

    private float vMusicInit, vEffectInit;

    private void SetVolumeSlider()
    {
        audioMixer.GetFloat("Music", out vMusicInit);
        audioMixer.GetFloat("Effect", out vEffectInit);

        sliderMusic.value = Mathf.Pow(10, vMusicInit / 65);
        sliderEffect.value = Mathf.Pow(10, vEffectInit / 65);

    }


    public void SetLevelMusic(float sliderValue)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 65);
    }

    public void SetLevelEffect(float sliderValue)
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(sliderValue) * 65);
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


    public void LoadLevel1()
    {
        SceneManager.LoadScene("SceneNiveau0");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("SceneNiveau1");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("SceneNiveau2");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("SceneNiveau3");
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene("SceneNiveau4");
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene("SceneNiveau5");
    }


}

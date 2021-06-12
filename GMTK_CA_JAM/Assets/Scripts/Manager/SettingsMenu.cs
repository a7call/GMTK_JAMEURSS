using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    private Player player1;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
    }

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
        player1.rightKey = KeyCode.D;
        player1.leftKey = KeyCode.A;
    }

    public void SwitchToQD()
    {
        player1.rightKey = KeyCode.D;
        player1.leftKey = KeyCode.Q;
    }

}

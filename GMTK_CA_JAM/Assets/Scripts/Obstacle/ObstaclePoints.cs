using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoints : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private string soundKill;
    [SerializeField] private string soundHit;


    // Start is called before the first frame update
    void Start()
    {
        //Ref
        //Play animation
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameOver();
        }

        if (collision.CompareTag("Chaine"))
        {
            Kill();
            
        }
    }

    private void GameOver()
    {
        AudioManagerEffect.instance.Play(soundHit);
        GameManager.Instance.GameOver();
    }

    private void Kill()
    {
        GameManager.Instance.AddPoints(points);
        AudioManagerEffect.instance.Play(soundKill);
        //Animation Kill
    }


}

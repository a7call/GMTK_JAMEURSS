using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFixe : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private string soundHit;
    void Start()
    {
        //Ref
        //Play animation
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2") || collision.CompareTag("Chaine")) 
        {
            //AudioManagerEffect.instance.Play(soundHit);
            AudioManagerEffect.instance.Play("MortChaine");
            GameManager.Instance.GameOver();
        }
        
    }
}

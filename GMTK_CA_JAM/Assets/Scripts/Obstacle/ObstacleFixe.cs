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
        
        if (collision.CompareTag("Chaine")) 
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            //AudioManagerEffect.instance.Play(soundHit);
            AudioManagerEffect.instance.Play("MortChaine");
            GameManager.Instance.GameOver();
        }

       if( collision.CompareTag("Player1") || collision.CompareTag("Player2"))
        {
            AudioManagerEffect.instance.Play("MortCollision" + Random.Range(1, 3));
            GameManager.Instance.GameOver();
        }


    }
}

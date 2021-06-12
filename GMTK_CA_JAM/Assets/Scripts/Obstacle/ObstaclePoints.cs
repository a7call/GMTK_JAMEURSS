using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoints : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] private string soundKill;
    [SerializeField] private string soundHit;

    public float ShakePower;
    public float ShakeTime;
    public float rotationMultiplier;

    private CameraFollow cameraFollow;


    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        //Ref
        //Play animation
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log(collision.gameObject);
        if (collision.CompareTag("Player1") || collision.CompareTag("Player2"))
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
        Debug.Log("GameOver");
        AudioManagerEffect.instance.Play(soundHit);
        GameManager.Instance.GameOver();
    }

    private void Kill()
    {
        Debug.Log("Points");
        //GameManager.Instance.AddPoints(points);
        points = 0;
        AudioManagerEffect.instance.Play(soundKill);
        cameraFollow.StartShakeG(ShakeTime, ShakePower, rotationMultiplier);
        //Animation Kill
    }


}

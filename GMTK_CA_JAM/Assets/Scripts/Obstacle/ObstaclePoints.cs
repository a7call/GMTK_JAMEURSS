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

    private Animator animator;

    [SerializeField] private GameObject bloodParticle;


    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        animator = gameObject.GetComponent<Animator>();
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
        
        AudioManagerEffect.instance.Play("MortCollision" + Random.Range(1, 3));
        GameManager.Instance.GameOver();
    }

    [SerializeField] private float offsetParticle;
    private void Kill()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        Instantiate(bloodParticle, transform.position + new Vector3(0, offsetParticle, 0), Quaternion.identity);
        GameManager.Instance.AddPoints(points);
        animator.SetBool("IsDead", true);
        AudioManagerEffect.instance.Play("Conde" + Random.Range(1,9));
        cameraFollow.StartShakeG(ShakeTime, ShakePower, rotationMultiplier);
        //Animation Kill
    }


}

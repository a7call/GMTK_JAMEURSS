using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] public KeyCode rightKey;
    [SerializeField] public KeyCode leftKey;
    [SerializeField] private float speed;
    [SerializeField] private float speedY;
    private float movement;
    private Rigidbody2D rb;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        //GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        //gameManager.SetKey();
        if(GameManager.Instance != null)
            GameManager.Instance.SetKey();
    }

    private void Update()
    {
        if (Input.GetKey(rightKey)) movement = speed;
        else if (Input.GetKey(leftKey)) movement = -speed;
        else movement = 0;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement, speedY) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject);
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
        speed = 0;
        speedY = 0;
    }
}

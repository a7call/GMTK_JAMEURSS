using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] public KeyCode rightKey;
    [SerializeField] public KeyCode leftKey;
    [SerializeField] private float speed;
    private float movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.SetKey();
    }

    private void Update()
    {
        if (Input.GetKey(rightKey)) movement = speed;
        else if (Input.GetKey(leftKey)) movement = -speed;
        else movement = 0;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement, 0);
    }
}

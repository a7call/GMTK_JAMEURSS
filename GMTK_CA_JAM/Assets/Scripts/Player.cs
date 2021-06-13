using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    
    [SerializeField] public KeyCode rightKey;
    [SerializeField] public KeyCode leftKey;
    [SerializeField] private float speed;
    [SerializeField] private float speedY;
    private float movement;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float increasingSpeed;
    private GameObject cam;

    private void Awake()
    {
        GameManager.Instance.sceneIndex = SceneManager.GetActiveScene().buildIndex -2;
        animator = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
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
    private bool isLoading;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint") &&  !isLoading)
        {
            cam.GetComponent<CameraFollow>().enabled = false;

            StartCoroutine(LoadingScene());
        }
    }

    private IEnumerator LoadingScene()
    {
        GameManager.Instance.fadeSystem.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        isLoading = true;
        int index = GameManager.Instance.sceneIndex + 1;
        SceneManager.LoadScene("SceneNiveau" + index);
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
        speed = 0;
        speedY = 0;
    }
}

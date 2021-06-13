using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borne : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private string soundExplosion;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainCamera"))
        {
            animator.SetTrigger("isCheck");
            AudioManagerEffect.instance.Play(soundExplosion);
        }
    }

}

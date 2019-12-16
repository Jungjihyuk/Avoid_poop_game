using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poop : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rigidbody;

    private AudioSource audioSource;

    void Start()
    {
      animator = GetComponent<Animator>();
      rigidbody = GetComponent<Rigidbody2D>();
      audioSource = GetComponent<AudioSource>();
      audioSource.loop = false;
      audioSource.volume = 1.5f;
      audioSource.Stop();
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.tag=="Ground")
      {
        GameManager.instance.Score();
        animator.SetTrigger("poop");
        animator.SetTrigger("poop2");
        animator.SetTrigger("poop3");
      }
      if(collision.gameObject.tag=="Player")
      {
        audioSource.Play();
        animator.SetTrigger("poop");
        animator.SetTrigger("poop2");
        animator.SetTrigger("poop3");
        GameManager.instance.Life();
      }
    }
}

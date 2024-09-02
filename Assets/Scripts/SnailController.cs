using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailController : MonoBehaviour
{
    public int speed = 1;
    public int health = 4;
    private bool isHit = false;
    private Rigidbody2D snailRB;
    private ScoreController scoreController; 
    private LivesController livesController;
    private Animator animator;
    public AudioSource enemyHit;
    public AudioSource enemyDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        snailRB = GetComponent<Rigidbody2D>();
        scoreController = FindObjectOfType<ScoreController>();
        livesController = FindObjectOfType<LivesController>();
    }

    void Update()
    {
        if (!isHit)
        {
            snailRB.velocity = -transform.right * speed;
        }
        if (health <= 0)
        {
            enemyDeath.Play();
            scoreController.AddScore();
            Destroy(gameObject);
            
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyHit.Play();
            animator.Play("snailHit");
            StartCoroutine(stunLock());


        }
        if (collision.gameObject.CompareTag("livesTag"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            livesController.lives--;
            Destroy(gameObject);
        }
    }

    IEnumerator stunLock()
    {
        snailRB.velocity = Vector2.zero;
        isHit = true;
        yield return new WaitForSeconds(1.0f);
        isHit = false;
        health--;
    }
}

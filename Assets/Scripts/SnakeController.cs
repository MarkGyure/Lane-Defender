using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public int speed = 4;
    public int health = 1;
    private bool isHit = false;
    public Rigidbody2D snakeRB;
    private ScoreController scoreController;
    private LivesController livesController;
    private Animator animator;
    public AudioSource enemyHit;
    public AudioSource enemyDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        snakeRB = GetComponent<Rigidbody2D>();
        scoreController = FindObjectOfType<ScoreController>();
        livesController = FindObjectOfType<LivesController>();
    }

    void Update()
    {
        if (!isHit)
        {
            snakeRB.velocity = -transform.right * speed;
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
            animator.Play("snakeHit");
            StartCoroutine(stunLock());
            
            
        }
        if (collision.gameObject.CompareTag("livesTag"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            livesController.lifeLost.Play();
            livesController.lives--;
            Destroy(gameObject);
        }
    }

    IEnumerator stunLock()
    {
        snakeRB.velocity = Vector2.zero;
        isHit = true;
        yield return new WaitForSeconds(1.0f);
        isHit = false;
        health--;
    }
}

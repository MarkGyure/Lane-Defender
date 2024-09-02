using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public int speed = 2;
    public int health = 3;
    private bool isHit = false;
    private Rigidbody2D slimeRB;
    private ScoreController scoreController;
    private LivesController livesController;
    private Animator animator;
    public AudioSource enemyHit;
    public AudioSource enemyDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        slimeRB = GetComponent<Rigidbody2D>();
        scoreController = FindObjectOfType<ScoreController>();
        livesController = FindObjectOfType<LivesController>();
    }

    void Update()
    {
        if (!isHit)
        {
            slimeRB.velocity = -transform.right * speed;
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
            animator.Play("slimeHit");
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
        slimeRB.velocity = Vector2.zero;
        isHit = true;
        yield return new WaitForSeconds(1.0f);
        isHit = false;
        health--;
    }
}

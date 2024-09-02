using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    public int lives = 3;
    public TMP_Text livesText;
    public TMP_Text gameOverText;
    public Collider2D scoreCollider;
    public ScoreController scoreController;
    public SpawnsController spawnsController;
    public Button restartButton;
    private TMP_Text buttonText;
    private EndExplosionController endExplosionController;
    public GameObject playerTank;
    public AudioSource lifeLost;

    void Start()
    {
        buttonText = restartButton.GetComponentInChildren<TMP_Text>();
        endExplosionController = FindObjectOfType<EndExplosionController>();
        gameOverText.enabled = false;
        scoreController.highScoreText.enabled = false;
        restartButton.enabled = false;
        restartButton.GetComponent<Image>().enabled = false;

        buttonText.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            lifeLost.Play();
            lives--;
        }
    }

    void Update()
    {
        livesText.text = "Lives: " + lives;
        if (lives == 0)
        {
            endExplosionController.TankExplosion();
            playerTank.GetComponent<SpriteRenderer>().enabled = false;
            spawnsController.spawnInterval = 100000;
            gameOverText.enabled = true;
            scoreController.highScoreText.enabled = true;
            restartButton.enabled = true;
            restartButton.GetComponent<Image>().enabled = true;
            buttonText.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private PlayerControls inputActions; //generated PlayerControls class
    private Rigidbody2D playerRB;
    private float verticalInput;
    public GameObject bulletPrefab;
    public GameObject fireExplosion;
    public float bulletSpawnOffset = 1.0f;
    private Coroutine shootingCoroutine;
    private bool canShoot = true;
    public float shootCooldown = 0.5f;
    public AudioSource shootSound;

    void Awake()
    {
        inputActions = new PlayerControls();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Controls.Shoot.started += StartShooting; //subscribe to shoot event
        inputActions.Controls.Shoot.canceled += StopShooting;
    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Controls.Shoot.started -= StartShooting; //unsubscribe from shoot event
        inputActions.Controls.Shoot.canceled -= StopShooting;
    }

    void Update()
    {
        verticalInput = inputActions.Controls.Move.ReadValue<float>();
    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(0, verticalInput * speed);
        if (playerRB.transform.position.y >= 1)
        {
            playerRB.transform.position = new Vector2(playerRB.transform.position.x, 1);
        }
        if (playerRB.transform.position.y < -4.5)
        {
            playerRB.transform.position = new Vector2(playerRB.transform.position.x, -4.4f);
        }
    }

    void StartShooting(InputAction.CallbackContext context)
    {
        if (canShoot && shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(Shoot());
        }
    }

    void StopShooting(InputAction.CallbackContext context)
    {
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
            shootingCoroutine = null; //reset coroutine
            StartCoroutine(ShootCooldown());
        }
    }

    void DoFireExplosion()
    {
        Vector2 spawnPosition = playerRB.transform.position + new Vector3(0.8f, 0.45f, 0);
        GameObject explosionInstance = Instantiate(fireExplosion, spawnPosition, Quaternion.identity);
        StartCoroutine(WaitExplosion(explosionInstance));
    }


    IEnumerator WaitExplosion(GameObject explosionInstance)
    {
        yield return new WaitForSeconds(0.20f);
        Destroy(explosionInstance);
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Vector2 spawnPosition = playerRB.transform.position + new Vector3(bulletSpawnOffset, 0.42f, 0);
            DoFireExplosion(); // Call the method to create and destroy the explosion
            Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            shootSound.Play();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}

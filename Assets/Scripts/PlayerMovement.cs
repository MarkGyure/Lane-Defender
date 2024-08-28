using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private PlayerControls inputActions; //Reference to generated PlayerControls class
    private Rigidbody2D playerRB;
    private float verticalInput;
    public GameObject bulletPrefab;
    public float bulletSpawnOffset = 1.0f;

    void Awake()
    {
        inputActions = new PlayerControls();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        verticalInput = inputActions.Controls.Move.ReadValue<float>();

        if(inputActions.Controls.Shoot.triggered )
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 spawnPosition = playerRB.transform.position + new Vector3(bulletSpawnOffset, 0.42f, 0);
        Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    }

    void FixedUpdate()
    {
        playerRB.velocity = new Vector2(0, verticalInput * speed);
        if(playerRB.transform.position.y >= 1)
        {
            playerRB.transform.position = new Vector2(playerRB.transform.position.x, 1);
        }
        if(playerRB.transform.position.y < -4.5)
        {
            playerRB.transform.position = new Vector2(playerRB.transform.position.x, -4.4f);
        }
    }
}
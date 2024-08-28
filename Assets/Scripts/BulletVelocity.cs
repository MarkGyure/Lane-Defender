using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    public float velocity = 5f;
    private Rigidbody2D bullet;
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        bullet.velocity = new Vector2(velocity, bullet.velocity.y);
    }
    private void Update()
    {
        if (bullet.transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }
}

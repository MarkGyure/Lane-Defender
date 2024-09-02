using System.Collections;
using UnityEngine;

public class BulletVelocity : MonoBehaviour
{
    public float velocity = 5f;
    private Rigidbody2D bullet;
    public GameObject fireExplosion;

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

    void DoFireExplosion()
    {
        Vector2 spawnPosition = bullet.transform.position;
        GameObject explosionInstance = Instantiate(fireExplosion, spawnPosition, Quaternion.identity);
        StartCoroutine(WaitExplosion(explosionInstance));
        
    }

    IEnumerator WaitExplosion(GameObject explosionInstance)
    {
        yield return new WaitForSeconds(0.20f);
        Destroy(explosionInstance);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            DoFireExplosion();
            bullet.GetComponent<SpriteRenderer>().enabled = false;
            bullet.GetComponent<Collider2D>().enabled = false;
        }
    }
}

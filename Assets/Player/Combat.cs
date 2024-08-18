using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool facingLeft;
    private Rigidbody2D rb;
    public GameObject bullet;
    public float maxhealth = 20, health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            facingLeft = false;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            facingLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            if (facingLeft)
            {
                newBullet.GetComponent<Projectile>().speed *= -1;
            }
        }

        if (health <= 0)
        {
            health = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 1;
            Vector3 collisionVector = transform.position - collision.transform.position;
            GetComponent<Move>().Bump(new Vector2(collisionVector.x, collisionVector.y / 2) * 15);
        }
    }
}

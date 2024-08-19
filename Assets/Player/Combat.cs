using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool facingLeft;
    private Rigidbody2D rb;
    public GameObject bullet;
    public float maxhealth = 20, health, invincibilityDuration, fireGap;
    private float iFrames = 0, lastFired = 0;
    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            health += Time.deltaTime * 5;
        }
        if (health > maxhealth)
        {
            health = maxhealth;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            facingLeft = false;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            facingLeft = true;
        }

        if (Input.GetKey(KeyCode.K) && lastFired <= 0)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            if (facingLeft)
            {
                newBullet.GetComponent<Projectile>().speed *= -1;
            }
            lastFired = fireGap;
            gameObject.GetComponent<Move>().extFreeze(0.5f);
            rb.velocity = new Vector2();
        }

        iFrames -= Time.deltaTime;
        lastFired -= Time.deltaTime;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && iFrames <= 0)
        {
            health = Math.Max(health-1, 0);
            Vector3 collisionVector = transform.position - collision.transform.position;
            GetComponent<Move>().Bump(new Vector2(collisionVector.x, collisionVector.y / 2) * 15);
            iFrames = invincibilityDuration;
        }
    }
}

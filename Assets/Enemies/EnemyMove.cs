using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject Player;
    public float speed;
    private float startpos, freeze = 0;

    SpriteRenderer spriteRenderer;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        startpos = transform.position.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        freeze -= Time.deltaTime;
        Vector2 v = rb.velocity;
        if (freeze <= 0)
        {
            if (Mathf.Abs(Player.transform.position.y - transform.position.y) >= 2.5)
            {
                if (transform.position.x - startpos < 0.1)
                {
                    v.x = 0;
                }
                else if (transform.position.x > startpos)
                {
                    v.x = -speed / 2;
                    spriteRenderer.flipX = false;

                }
                else
                {
                    v.x = speed / 2;
                    spriteRenderer.flipX = true;
                }
            }
            else
            {
                if (Player.transform.position.x > transform.position.x)
                {
                    v.x = speed;
                    spriteRenderer.flipX = true;
                }
                else
                {
                    v.x = -speed;
                    spriteRenderer.flipX = false;
                }
            }
            rb.velocity = v;
        }
        animator.SetFloat("VelocityX", Mathf.Abs(v.x));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), collision.collider);
        }
    }

    public void Bump(Vector3 dir)
    {
        freeze = 0.5f;
        rb.velocity = dir;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject Player;
    public float speed;
    private float startpos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
        startpos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 v = rb.velocity;
        if (Mathf.Abs(Player.transform.position.y - transform.position.y) >= 2.5)
        {
            if (transform.position.x - startpos < 0.1)
            {
                v.x = 0;
            } else if (transform.position.x > startpos)
            {
                v.x = -speed / 2;
            } else
            {
                v.x = speed / 2;
            }
        } else
        {
            if (Player.transform.position.x > transform.position.x)
            {
                v.x = speed;
            }
            else
            {
                v.x = -speed;
            }
        }
        rb.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), collision.collider);
        }
    }
}

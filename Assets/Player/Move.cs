using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jump, speed;
    private bool grounded, frozen;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && grounded && !frozen)
        {
            grounded = false;
            Vector2 v = rb.velocity;
            v.y = jump;
            rb.velocity = v;
        }

        if (rb.velocity.x < 0.5)
        {
            frozen = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 v = rb.velocity;
        if (frozen)
        {

        } else if (Input.GetKey(KeyCode.A))
        {
            v.x = -speed;
            //v.x -= speed * Time.deltaTime * 6;
            //v.x = Mathf.Max(v.x, -speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            v.x = speed;
            //v.x += speed * Time.deltaTime * 6;
            //v.x = Mathf.Min(v.x, speed);
        }
        else
        {
            v.x = 0;
        }
        rb.velocity = v;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Stair"))
        {
            grounded = true;
        }
    }

    public void Bump(Vector2 direction)
    {
        frozen = true;
        rb.velocity = direction;
    }
}

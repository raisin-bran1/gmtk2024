using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jump, speed;
    private bool grounded, frozen, big;
    private float extFrozen = 0;
    private int weaponChildNumber = 2;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        big = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        extFrozen -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) && grounded && !frozen)
        {
            grounded = false;
            Vector2 v = rb.velocity;
            v.y = jump;
            rb.velocity = v;
        }

        if (frozen && Mathf.Abs(rb.velocity.x) < 0.5 && extFrozen <= 0)
        {
            frozen = false;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!big)
            {
                big = true;
                transform.localScale = transform.localScale * 2f;
                jump -= 4;
                speed *= 0.5f;
                if (gameObject.transform.childCount > weaponChildNumber) {
                    gameObject.transform.GetChild(weaponChildNumber).gameObject.GetComponent<GunCombat>().damage *= 2f;
                }
            }
            else
            {
                big = false;
                transform.localScale = transform.localScale * 0.5f;
                jump += 4;
                speed *= 2f;
                if (gameObject.transform.childCount > weaponChildNumber)
                {
                    gameObject.transform.GetChild(weaponChildNumber).gameObject.GetComponent<GunCombat>().damage *= 0.5f;
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 v = rb.velocity;
        if (frozen)
        {

        } else if (Input.GetKey(KeyCode.A))
        {
            //v.x = -speed;
            v.x -= speed * Time.deltaTime * 12;
            v.x = Mathf.Max(v.x, -speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //v.x = speed;
            v.x += speed * Time.deltaTime * 12;
            v.x = Mathf.Min(v.x, speed);
        }
        else
        {
            v.x = 0;
        }
        rb.velocity = v;

        animator.SetFloat("VelocityX", Mathf.Abs(v.x));

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Stair"))
        {
            grounded = true;
            if (frozen)
            {
                rb.velocity = new Vector2();
            }
            if (collision.gameObject.CompareTag("Stair"))
            {
                rb.gravityScale = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            rb.gravityScale = 5;
        }
    }

    public void Bump(Vector2 direction)
    {
        frozen = true;
        rb.velocity = direction;
    }

    public void extFreeze(float duration)
    {
        extFrozen = duration;
        frozen = true;
    }

    public bool isBig()
    {
        return big;
    }

    public bool isGrounded()
    {
        return grounded;
    }
}

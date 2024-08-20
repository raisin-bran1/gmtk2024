using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jump, speed;
    private bool grounded, frozen, big;
    private float extFrozen = 0;
    private int weaponChildNumber = 2;

    public GameObject background;

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

        if (frozen && extFrozen <= 0)
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
                rb.mass *= 2;
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
                rb.mass *= 0.5f;
                if (gameObject.transform.childCount > weaponChildNumber)
                {
                    gameObject.transform.GetChild(weaponChildNumber).gameObject.GetComponent<GunCombat>().damage *= 0.5f;
                }
            }
        }
        /*Vector3 position = transform.position;
        position = position * 0.98f;
        position.y = position.y - 0.129f;
        background.transform.position = position;*/
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
                rb.gravityScale = 1;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stair"))
        {
            rb.gravityScale = 5;
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            StartCoroutine(Win());
        }
    }

    public void Bump(Vector2 direction)
    {
        frozen = true;
        extFrozen = 0.2f;
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

    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Win");
    }
}

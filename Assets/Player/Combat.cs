using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private float facing;
    private Rigidbody2D rb;
    public float maxhealth = 20, health, invincibilityDuration;
    private float iFrames = 0, lastFired = 0;
    private GameObject weapon;
    public GameObject deathscreen;
    public Animator animator;
    private bool fire = false;
    private int weaponChildNumber = 2;
    private float meleeTime = 0.2f;

    SpriteRenderer spriteRenderer;
    Move move;
    [SerializeField] AudioClip loot;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Move>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        iFrames -= Time.deltaTime;
        lastFired -= Time.deltaTime;
        meleeTime += Time.deltaTime;
        animator.SetFloat("MeleeTime", meleeTime);
        if (weapon != null && lastFired <= 0 && lastFired + Time.deltaTime >= 0)
        {
            weapon.gameObject.GetComponent<Animator>().SetBool("Fire", false);
            animator.SetBool("Fire", false);
            fire = false;
            weapon.GetComponent<GunCombat>().Position(facing, false, false);
        }

        if (Input.GetKey(KeyCode.H) && iFrames < -5)
        {
            health += Time.deltaTime * 5;
        }
        if (health > maxhealth)
        {
            health = maxhealth;
        } else if (health <= 0)
        {
            StartCoroutine(Death());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (facing == -1 && weapon != null)
            {
                weapon.GetComponent<GunCombat>().Position(-1, fire, false);
            }
            Vector3 s = transform.localScale;
            s.x = -Mathf.Abs(s.x);
            transform.localScale = s;
            //spriteRenderer.flipX = true;
            facing = 1;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            if (facing == 1 && weapon != null)
            {
                weapon.GetComponent<GunCombat>().Position(1, fire, false);
            }
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x);
            transform.localScale = s;
            //spriteRenderer.flipX = false;
            facing = -1;
        }

        if (Input.GetKey(KeyCode.K) && lastFired <= 0 && weapon != null)
        {
            lastFired = weapon.GetComponent<GunCombat>().fireGap;
            weapon.gameObject.GetComponent<Animator>().SetBool("Fire", true);
            animator.SetBool("Fire", true);
            fire = true;
            GunCombat c = weapon.GetComponent<GunCombat>();
            c.Position(facing, fire, false);
            c.Fire();
            StartCoroutine(c.Wiggle());
            move.extFreeze(0.5f);
            if (move.isGrounded())
            {
                rb.velocity = new Vector2();
            }
        }
        if(Input.GetKey(KeyCode.I) && meleeTime >= 0.8 && lastFired <= 0)
        {
            lastFired = 0.8f;
            meleeTime = 0;
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            if (Input.GetKey(KeyCode.L) && lastFired <= 0)
            {
                lastFired = 0.5f;
                if (gameObject.transform.childCount > weaponChildNumber)
                {
                    gameObject.transform.GetChild(weaponChildNumber).gameObject.transform.parent = null;
                }
                weapon = collider.gameObject;
                weapon.transform.parent = gameObject.transform;
                if (!move.isBig())
                {
                    weapon.transform.localScale *= 0.5f;
                }
                weapon.GetComponent<GunCombat>().Position(1, fire, true);
                animator.SetBool("CarryingPistol", weapon.GetComponent<GunCombat>().isPistol);
                AudioSource.PlayClipAtPoint(loot, transform.position, 1);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && iFrames <= 0 && meleeTime >= 0.6f)
        {
            health = Math.Max(health - 1, 0);
            Vector3 collisionVector = transform.position - collision.transform.position;
            move.Bump(new Vector2(collisionVector.x, collisionVector.y / 2) * 15);
            iFrames = invincibilityDuration;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && meleeTime < 0.6f)
        {
            if (move.isBig())
            {
                collision.gameObject.GetComponent<EnemyCombat>().Damage(2, transform.localScale.x);
            } else
            {
                collision.gameObject.GetComponent<EnemyCombat>().Damage(1, transform.localScale.x);
            }
            Vector3 collisionVector = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<EnemyMove>().Bump(collisionVector);
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(0.6f);
        Instantiate(deathscreen);
        Time.timeScale = 0;
    }
}

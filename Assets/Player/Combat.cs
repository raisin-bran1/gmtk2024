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

    Move move;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Move>();
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
            if (facing == -1 && weapon != null)
            {
                weapon.transform.position = gameObject.transform.position + new Vector3(facing * -1 * gameObject.transform.localScale.x, 0 * gameObject.transform.localScale.y, 0);
                weapon.GetComponent<SpriteRenderer>().flipX = false;
            }
            facing = 1;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            if (facing == 1 && weapon != null)
            {
                weapon.transform.position = gameObject.transform.position + new Vector3(facing * -1 * gameObject.transform.localScale.x, 0 * gameObject.transform.localScale.y, 0);
                weapon.GetComponent<SpriteRenderer>().flipX = true;
            }
            facing = -1;
        }

        if (Input.GetKey(KeyCode.K) && lastFired <= 0 && weapon != null)
        {
            weapon.GetComponent<GunCombat>().Fire();
            lastFired = weapon.GetComponent<GunCombat>().fireGap;
            move.extFreeze(0.5f);
            if (move.isGrounded())
            {
                rb.velocity = new Vector2();
            }
        }

        iFrames -= Time.deltaTime;
        lastFired -= Time.deltaTime;

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            if (Input.GetKey(KeyCode.L) && lastFired <= 0)
            {
                lastFired = 1;
                if (gameObject.transform.childCount > 1)
                {
                    gameObject.transform.GetChild(1).gameObject.transform.parent = null;
                }
                weapon = collider.gameObject;
                weapon.transform.parent = gameObject.transform;
                if (!move.isBig())
                {
                    weapon.transform.localScale *= 0.5f;
                }
                weapon.transform.position = gameObject.transform.position + new Vector3(facing * 1 * gameObject.transform.localScale.x, 0 * gameObject.transform.localScale.y, 0);
                if (facing == -1)
                {
                    weapon.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && iFrames <= 0)
        {
            health = Math.Max(health-1, 0);
            Vector3 collisionVector = transform.position - collision.transform.position;
            move.Bump(new Vector2(collisionVector.x, collisionVector.y / 2) * 15);
            iFrames = invincibilityDuration;
        }
    }
}

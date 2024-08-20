using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float health = 5;
    private float lastHit = 1, iFrames = 0;
    public Worldbuilder wb;
    Animator animator;
    Animator childAnimator;
    [SerializeField] AudioClip blood;
    // Start is called before the first frame update
    void Start()
    {
        childAnimator = transform.GetChild(0).GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        iFrames -= Time.deltaTime;
        lastHit += Time.deltaTime;
        childAnimator.SetFloat("LastHit", lastHit);
    }

    public void Damage(float damage, float speed)
    {
        AudioSource.PlayClipAtPoint(blood, transform.position, 0.5f);
        health -= damage;
        lastHit = 0;
        if (speed > 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
        } else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
        }
        if (health <= 0)
        {
            animator.SetBool("Dead", true);
            Destroy(gameObject, 0.617f);

            if (iFrames <= 0)
            {
                iFrames = 0.15f;
                health -= damage;
                lastHit = 0;
                if (speed > 0)
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }
                if (health <= 0)
                {
                    animator.SetBool("Dead", true);
                    Destroy(gameObject, 0.617f);

                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private float damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.FindWithTag("Player").GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Stair"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider);
        } else
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyCombat>().Damage(damage);
            }
            Destroy(gameObject);
        }
    }
}

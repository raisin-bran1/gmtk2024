using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCombat : MonoBehaviour
{

    public float fireGap, damage, fireSpeed;
    public GameObject bullet;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Quaternion rot = Quaternion.Euler(0, 0, -90);
        GameObject newBullet = Instantiate(bullet, transform.position, rot);
        newBullet.GetComponent<Projectile>().damage = damage;
        newBullet.GetComponent<Projectile>().speed *= fireSpeed;
        if (transform.localScale.x < 0)
        {
            rot = Quaternion.Euler(0, 0, 90);
            newBullet.GetComponent<Projectile>().speed *= -1;
        }
    }

}

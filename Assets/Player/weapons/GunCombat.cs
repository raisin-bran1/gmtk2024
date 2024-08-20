using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunCombat : MonoBehaviour
{

    public float fireGap, damage, fireSpeed, idleX, idleY, idleRot, firingX, firingY, firingRot;
    public bool isPistol;
    public GameObject bullet;
    [SerializeField] AudioClip fire;

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
        GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(0, 0.1f, 0), rot);
        newBullet.GetComponent<Projectile>().damage = damage;
        newBullet.GetComponent<Projectile>().speed *= fireSpeed;
        if (transform.parent.gameObject.transform.localScale.x > 0)
        {
            rot = Quaternion.Euler(0, 0, 90);
            newBullet.GetComponent<Projectile>().speed *= -1;
        }
        AudioSource.PlayClipAtPoint(fire, transform.position, 1);
    }

    public void Position(float flip, bool firing, bool initial)
    {
        if (initial)
        {
            Vector3 s = transform.localScale;
            s.x = -flip * Mathf.Abs(s.x);
            transform.localScale = s;
        }
        if (firing)
        {
            transform.position = transform.parent.gameObject.transform.position + new Vector3(flip * firingX * Mathf.Abs(transform.parent.gameObject.transform.localScale.x), firingY * Mathf.Abs(transform.parent.gameObject.transform.localScale.y), 0);
            transform.rotation = Quaternion.Euler(0, 0, firingRot * flip);
            //transform.rotation = Quaternion.Euler(0, 0, firingRot);
        }
        else
        {
            transform.position = transform.parent.gameObject.transform.position + new Vector3(flip * idleX * Mathf.Abs(transform.parent.gameObject.transform.localScale.x), idleY * Mathf.Abs(transform.parent.gameObject.transform.localScale.y), 0);
            transform.rotation = Quaternion.Euler(0, 0, idleRot * flip);
            //transform.rotation = Quaternion.Euler(0, 0, idleRot);
        }
    }

    public IEnumerator Wiggle()
    {
        Vector3 pos = transform.position;
        int flip = 1;
        if (transform.parent.gameObject.transform.localScale.x > 0)
        {
            flip = -1;
        }
        pos.x -= 0.1f * flip;
        transform.position = pos;
        yield return new WaitForSeconds(fireGap / 2);
        pos = transform.position;
        pos.x += 0.1f * flip;
        transform.position = pos;
    }

}

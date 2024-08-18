using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private bool facingLeft;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            facingLeft = false;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            facingLeft = true;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            if (facingLeft)
            {
                newBullet.GetComponent<Projectile>().speed *= -1;
            }
        }
    }
}

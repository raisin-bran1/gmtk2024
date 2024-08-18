using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour
{
    GameObject Player;
    BoxCollider2D col;
    bool enablecol;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float playerbottom = Player.transform.position.y - Player.transform.localScale.y / 2;
        if (transform.position.y <= playerbottom)
        {
            enablecol = true;
        } else
        {
            enablecol = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            enablecol = false;
        }

        if (!enablecol)
        {
            Physics2D.IgnoreCollision(col, Player.GetComponent<Collider2D>());
        } else
        {
            Physics2D.IgnoreCollision(col, Player.GetComponent<Collider2D>(), false);
        }
    }
}

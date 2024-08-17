using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairCollision : MonoBehaviour
{
    GameObject Player;
    BoxCollider2D col;

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
        if (Mathf.Tan(transform.eulerAngles.z * 3.14f / 180) * (Player.transform.position.x - transform.position.x) + transform.position.y <= playerbottom)
        {
            col.enabled = true;
        }
        else
        {
            col.enabled = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            col.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbarscript : MonoBehaviour
{
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float health = Player.GetComponent<Combat>().health, maxhealth = Player.GetComponent<Combat>().maxhealth;
        transform.localScale = new Vector3(health * 10 / maxhealth, 1, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Enemies : MonoBehaviour
{
    float time = 0, roundedTime;
    TMP_Text text;
    Worldbuilder wb;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        wb = GameObject.Find("GameController").GetComponent<Worldbuilder>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = wb.GetEnemyCount().ToString() + " Enemies Slain";
    }

    public void Reset()
    {
        time = 0;
    }
}

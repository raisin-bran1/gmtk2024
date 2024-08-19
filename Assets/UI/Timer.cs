using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float time = 0, roundedTime;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        roundedTime = (int) time;
        text.text = roundedTime.ToString();
    }
}

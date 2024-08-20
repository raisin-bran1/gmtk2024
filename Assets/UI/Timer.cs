using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name == "Game")
        {
            time += Time.deltaTime;
            roundedTime = (int)time;
            text.text = roundedTime.ToString();
        } else
        {
            roundedTime = (float)(int)(time * 10) / 10;
            if (roundedTime % 1 == 0)
            {
                text.text = roundedTime.ToString() + ".0";
            }
            else
            {
                text.text = roundedTime.ToString();
            }
        }
    }

    public void Reset()
    {
        time = 0;
    }
}

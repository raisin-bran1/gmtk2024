using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressButton : MonoBehaviour
{

    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("TimerCanvas");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadGame()
    {
        Destroy(canvas);
        SceneManager.LoadScene("Game");
    }
}

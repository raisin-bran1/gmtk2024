using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worldbuilder : MonoBehaviour
{
    public GameObject floor, platform, stair;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        int count = 0;
        for (float i = 15.25f; i <= 104.5f; i += 5.5f)
        {
            count++;
            if (count % 2 == 0)
            {
                buildFloor(i, true);
            } else
            {
                buildFloor(i, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildFloor(float ceilingHeight, bool stairtype)
    {
        // Range: -9.5 to 7.5 or -11.5 to 5.5
        float platformx;
        if (stairtype)
        {
            platformx = Random.Range(-9, 9) - 0.5f;
            GameObject s = Instantiate(stair, new Vector3(platformx + 1, ceilingHeight - 2.75f, 0), Quaternion.identity);
        } else
        {
            platformx = Random.Range(-11, 7) - 0.5f;
            GameObject s = Instantiate(stair, new Vector3(platformx + 3, ceilingHeight - 2.75f, 0), Quaternion.identity);
            s.transform.localScale = new Vector3(-1, 1, 1);
        }
        GameObject f1 = Instantiate(floor, new Vector3(-11.5f, ceilingHeight, 0), Quaternion.identity);
        f1.transform.localScale = new Vector3(platformx + 11.5f, 0.5f, 1);
        GameObject p = Instantiate(platform, new Vector3(platformx, ceilingHeight, 0), Quaternion.identity);
        GameObject f2 = Instantiate(floor, new Vector3(platformx + 4, ceilingHeight, 0), Quaternion.identity);
        f2.transform.localScale = new Vector3(7.5f - platformx, 0.5f, 1);
        
    }
}

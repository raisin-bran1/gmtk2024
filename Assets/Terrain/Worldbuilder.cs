using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worldbuilder : MonoBehaviour
{
    public GameObject floor, platform, stair, enemy, cabinet1, cabinet2, table, window, pistol, m4;
    private int enemyCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        int count = 0;
        for (float i = 15.25f; i <= 108.75f; i += 5.5f)
        {
            count++;
            buildFloor(i, count % 2 == 0, count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildFloor(float ceilingHeight, bool stairtype, int difficulty)
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
        for (int x = 0; x < difficulty / 5 + 1; x++)
        {
            Instantiate(enemy, new Vector3(Random.Range(-10,10), ceilingHeight - 4, 0), Quaternion.identity);
        }
        if (Random.Range(0, 2) < 1)
        {
            Instantiate(cabinet1, new Vector3(-7 + Random.Range(0, 3), ceilingHeight - 4, 0), Quaternion.identity);
        }
        if (Random.Range(0, 2) < 1)
        {
            Instantiate(cabinet2, new Vector3(-4 + Random.Range(0, 3), ceilingHeight - 4, 0), Quaternion.identity);
        }
        if (Random.Range(0, 2) < 1)
        {
            Instantiate(table, new Vector3(4 + Random.Range(0, 6), ceilingHeight - 4, 0), Quaternion.identity);
        }
        if (Random.Range(0, 2) < 1)
        {
            Instantiate(window, new Vector3(Random.Range(-7, 7), ceilingHeight - Random.Range(2, 3), 0), Quaternion.identity);
        }
        if (difficulty == 5)
        {
            Instantiate(pistol, new Vector3(Random.Range(-7, 7), ceilingHeight - 4, 0), Quaternion.identity);
        }
        if (difficulty == 15)
        {
            Instantiate(m4, new Vector3(Random.Range(-7, 7), ceilingHeight - 4, 0), Quaternion.identity);
        }
    }

    public void DecrementEnemies()
    {
        enemyCount--;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }
}

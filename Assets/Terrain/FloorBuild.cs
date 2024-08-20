using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorBuild : MonoBehaviour
{
    private Tilemap building;
    private Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        building = GameObject.Find("Building").GetComponent<Tilemap>();
        tiles = Resources.LoadAll<Tile>("Tiles");
        for (float i = 0; i < transform.localScale.x; i += 0.5f)
        {
            building.SetTile(new Vector3Int((int)((transform.position.x + i)*2), (int)((transform.position.y - 0.25)*2)), tiles[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

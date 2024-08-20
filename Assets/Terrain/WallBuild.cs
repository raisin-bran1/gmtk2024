using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallBuild : MonoBehaviour
{
    private Tilemap building;
    private Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        building = GameObject.Find("Building").GetComponent<Tilemap>();
        tiles = Resources.LoadAll<Tile>("Tiles");
        for (float i = 0; i < transform.localScale.y; i += 0.5f)
        {
            building.SetTile(new Vector3Int(((int)((transform.position.x - 0.5) * 2)), (int)((transform.position.y + i) * 2)), tiles[4]);
            building.SetTile(new Vector3Int(((int)((transform.position.x) * 2)), (int)((transform.position.y + i) * 2)), tiles[5]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

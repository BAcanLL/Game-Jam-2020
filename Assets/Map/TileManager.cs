
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum TileType
{
    GRASS, BRICK
}

public class TileManager : MonoBehaviour
{
    List<Block> tiles;
    public Tilemap initialization_map;

    Tilemap NE_map;
    Tilemap SE_map;
    Tilemap SW_map;
    Tilemap NW_map;

    Block createTile(TileType type)
    {
        return null;
        // switch{
            

        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

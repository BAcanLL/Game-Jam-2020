using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

enum Orientation
{
    NE, SE, SW, NW
}


public class Block: MonoBehaviour
{
    Tile NE_viewable_tile;
    Tile SE_viewable_tile;
    Tile SW_viewable_tile;
    Tile NW_viewable_tile;

    // RigidBody collider;

    void setTileView(Tile viewable_tile, Orientation o)
    {
        
    }

    void setPos(Vector3 pos)
    {

    }
}

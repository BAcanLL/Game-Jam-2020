using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// enum Orientation
// {
//     NE, SE, SW, NW
// }

public enum BlockType {
    GRASS, WALL
}

public class Block: MonoBehaviour
{

    List<ViewController> views;
    List<Vector3Int> block_viewables;


    void start(){
        foreach (ViewController v in views){
            // block_viewables.Add(v.createBlockViewable())
        }

    }

    void update(){
        
        // for (int i = 0; i<)

    }




    // RigidBody collider;

    void setTileView(Tile viewable_tile, Orientation o)
    {
        
    }

    void setPos(Vector3 pos)
    {

    }
}

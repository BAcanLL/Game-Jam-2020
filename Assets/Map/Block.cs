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

    BoxCollider collider;

    void initialize(Vector3 pos, BlockType type)
    {
        collider =  gameObject.AddComponent <BoxCollider>();
        collider.center = new Vector3(0,0,0);
        collider.size = new Vector3(1,1,1);

        transform.position = pos;
    }


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

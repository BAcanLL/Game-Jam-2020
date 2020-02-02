using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Block: MonoBehaviour
{
    public List<Tile> tile_views;

    Orientation orientation;
    List<Vector3Int> block_viewables;
    

    public void initialize(Vector3 pos, Orientation orientation)
    {
        
    }


    void start(){
        foreach (ViewController v in views){
        //     // block_viewables.Add(v.createBlockViewable())
        // }

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

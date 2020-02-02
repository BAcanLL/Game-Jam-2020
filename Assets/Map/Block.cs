﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Block: MonoBehaviour
{
    public List<Tile> tile_views;

    Orientation block_orientation;
    List<Vector3Int> block_viewables;
    
    
    BoxCollider collider;

    public void initialize(Vector3Int pos, Orientation orientation)
    {

        collider =  gameObject.AddComponent <BoxCollider>();
        collider.center = new Vector3(0,0,0);
        collider.size = new Vector3(1,1,1);

        transform.position = pos;


        foreach (ViewController view in FindObjectOfType<ViewManager>().GetViewControllers())
        {
            view.createBlockViewable(this, pos); // Vector3Int.RoundToInt(pos)
        }
    }

    void start(){
        
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

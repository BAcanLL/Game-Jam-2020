using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Block: MonoBehaviour
{
    public List<Tile> tile_views;
    public bool wall = false;

    List<Vector3Int> block_viewables;
    
    
    BoxCollider collider;

    public void initialize(Vector3Int pos)
    {

        collider = gameObject.AddComponent<BoxCollider>();
        collider.center = new Vector3(0,0,0);
        collider.size = new Vector3(1,1,1);

        pos.z = -pos.z;

        transform.position = pos + new Vector3(1,1,-1 + (wall ? 1 : 0));

        print(pos.ToString());


        foreach (ViewController view in FindObjectOfType<ViewManager>().GetViewControllers())
        {
            view.createBlockViewable(this, pos); // Vector3Int.RoundToInt(pos)
        }
    }

    void start(){
        
    }

    void update(){
        
    }


    // RigidBody collider;

    void setTileView(Tile viewable_tile)
    {
        
    }

    void setPos(Vector3 pos)
    {

    }
}

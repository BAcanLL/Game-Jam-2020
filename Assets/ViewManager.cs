using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ViewManager : MonoBehaviour
{
    public GameObject initialization_view;

    public List<Block> blocks;

    List<ViewController> view_controllers = new List<ViewController>();

    void Start()
    {
        Orientation[] used_orientations = {Orientation.SW, Orientation.NE};


        foreach( var o in used_orientations)
        {
            // clone initialization view
            GameObject new_view = Instantiate( initialization_view );
            new_view.transform.position += new Vector3(0, (int) o * 50, 0); // Place each view in unique location in world

            view_controllers.Add(new_view.GetComponent<ViewController>());
            view_controllers[view_controllers.Count - 1].orientation = o;
            new_view.GetComponentInChildren<Tilemap>().ClearAllTiles();
        }
        
        Tilemap tilemap = initialization_view.GetComponentInChildren<Tilemap>();
        blocks =  ParseBlocksFromTilemap(tilemap);


        Destroy(initialization_view);

        // print(tilemap.cellBounds);

        // TileBase[] allTiles = tilemap.GetTilesBlock(tilemap.cellBounds);

        // foreach (Tile t in allTiles){
        //     if ( t!= null){
        //         print(t.transform);
        //     }
        // }



        // create empty tilemaps
        // for tile in initialization_view
        // create logical blocks

    }

    public List<ViewController> GetViewControllers(){
        return view_controllers;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    List<Block> ParseBlocksFromTilemap(Tilemap tilemap){

        List<Block> blocks = new List<Block>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
            
            Tile t = (Tile) tilemap.GetTile(pos);
            if (t != null){

                // parse orientation and block from tile sprite
                string [] tilename = ParseTileForName(t).Split('_');
                Orientation o = (Orientation)int.Parse(tilename[1]);
                string block_prefab_name = tilename[0];

                print(o);
                print(block_prefab_name);

                // create logical block prefab
                GameObject block_prefab = Resources.Load<GameObject>(block_prefab_name);
                Block block = block_prefab.GetComponent<Block>();
                block.initialize(pos, o);
                blocks.Add(block);

            }

        }

        return blocks;

    }

    string ParseTileForName(Tile t){
        return t.sprite.name;
    }

}

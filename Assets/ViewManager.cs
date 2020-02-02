using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ViewManager : MonoBehaviour
{
    public GameObject initialization_view;
    public GameObject player1_viewable;
    public GameObject player2_viewable;

    public List<Block> blocks;

    List<ViewController> view_controllers = new List<ViewController>();

    void Start()
    {

        initialization_view = Instantiate(initialization_view);

        Orientation[] used_orientations = {Orientation.NE, Orientation.SW};

        foreach( var o in used_orientations)
        {
            // clone initialization view
            GameObject new_view = Instantiate(initialization_view);
            new_view.transform.position += new Vector3(0, (int) o * 10, 0); // Place each view in unique location in world

            // set up view controller
            view_controllers.Add(new_view.GetComponent<ViewController>());
            view_controllers[view_controllers.Count - 1].orientation = o;

            Tilemap tilemap = new_view.GetComponentInChildren<Tilemap>();
            // clear tiles in new view
            tilemap.ClearAllTiles();

            // attach playerViewables to viewController
            GameObject player1_viewable_object = Instantiate(player1_viewable);
            player1_viewable_object.transform.parent = new_view.transform;
            view_controllers[view_controllers.Count - 1].player1_viewable = player1_viewable_object;

            GameObject player2_viewable_object = Instantiate(player2_viewable);
            player2_viewable_object.transform.parent = new_view.transform;
            view_controllers[view_controllers.Count - 1].player2_viewable = player2_viewable_object;

            // attach cameras
            if (o == Orientation.NE)
                GameObject.Find("Camera1").transform.parent = player1_viewable_object.transform;
            else if (o == Orientation.SW)
                GameObject.Find("Camera2").transform.parent = player2_viewable_object.transform;
            else
            {
                print("Not supported view");
            }


            // attach tileMap to viewController
            view_controllers[view_controllers.Count - 1].tilemap = tilemap;

            // set parent of view to grid
            new_view.transform.parent = FindObjectOfType<Grid>().transform;

        }
        
        blocks = ParseBlocksFromTilemap(initialization_view.GetComponentInChildren<Tilemap>());

        Destroy(initialization_view);

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
                string block_prefab_name = ParseTileForName(t);
                print(block_prefab_name);

                // create logical block prefab
                GameObject block_prefab = Instantiate(Resources.Load<GameObject>(block_prefab_name));
                Block block = block_prefab.GetComponent<Block>();
                block.initialize(pos);
                blocks.Add(block);

            }

        }

        return blocks;

    }

    string ParseTileForName(Tile t){
        return t.sprite.name;
    }

}

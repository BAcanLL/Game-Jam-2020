using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ViewManager : MonoBehaviour
{
    public GameObject initialization_view;

    public List<Block> blocks;

    Tilemap tilemap;

    void Start()
    {
        tilemap = initialization_view.GetComponentInChildren<Tilemap>();

        ParseTilemapForBlocks(tilemap);






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

    public List<GameObject> GetViews(){
        return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void ParseTilemapForBlocks(Tilemap tilemap){

        foreach (var pos in tilemap.cellBounds.allPositionsWithin) {
            
            Tile t = (Tile) tilemap.GetTile(pos);
            if (t != null){

                // parse orientation and block from tile sprite
                string [] tilename = ParseTileForName(t).Split('_');
                Orientation o = (Orientation)int.Parse(tilename[1]);
                string block_prefab_name = tilename[0];

                // create logical block prefab
                GameObject block_prefab = Resources.Load<GameObject>(block_prefab_name);
                block_prefab.GetComponent<Block>().initialize(pos, o);

            }

        }

    }

    string ParseTileForName(Tile t){
        return t.sprite.name;
    }

}

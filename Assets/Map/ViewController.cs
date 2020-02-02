using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


enum Orientation {
      NE, SE, SW, NW
}

public class ViewController : MonoBehaviour {
    // Start is called before the first frame update

    // public Grid

    public Tilemap tilemap;
    public GameObject player1_viewable;
    public GameObject player2_viewable;

    public Vector3Int createBlockViewable(BlockType b, Vector3 pos){ //block type, position

        // depending on orientation
        // choose sprite and modify tilemap

        // tile coordinates
        return new Vector3Int(0, 0, 0);

    }

    public void updateBlockViewable(Vector3Int blockViewable)
    {


    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

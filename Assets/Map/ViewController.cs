using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ViewController : MonoBehaviour {
    // Start is called before the first frame update

    // public Grid

    public Tilemap tilemap;
    public GameObject player1_viewable;
    public GameObject player2_viewable;

    public Orientation orientation;

    public Vector3Int createBlockViewable(Block block, Vector3Int pos){ //block type, position
        Vector3Int new_pos = new Vector3Int();

        switch(orientation)
        {
            case Orientation.NE: 
                new_pos = pos;
                break;
            case Orientation.SW:
                new_pos.x = -pos.x;
                new_pos.y = -pos.y;
                break;
            default: Debug.Log("Invalid orientation"); break;
        }


        tilemap.SetTile(new_pos, block.tile_views[(int) orientation]);

        // // tile coordinates
        return new Vector3Int(0, 0, 0);
    }

    public void updateBlockViewable(Vector3Int blockViewable)
    {


    }

    public void updatePlayer1(Vector3 position, Vector3 velocity)
    {
        player1_viewable.transform.position = tilemap.CellToLocalInterpolated(position);
            

        Animation anim = player1_viewable.GetComponent<Animation>();

        if (velocity.y > 0)
        {
            anim.Play("Walking_back");
        }
        else if (velocity.y < 0 || velocity.x != 0)
        {
            anim.Play("Walking_front");
        }
        else
        {
            anim.Play("Idle");
        }

        if (velocity.x > 0)
        {
            player1_viewable.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (velocity.x < 0)
        {
            player1_viewable.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void updatePlayer2(Vector3 position, Vector3 velocity)
    {
        player2_viewable.transform.position = tilemap.CellToLocalInterpolated(position);
            

        Animation anim = player2_viewable.GetComponent<Animation>();

        if (velocity.y > 0)
        {
            anim.Play("Walking_back");
        }
        else if (velocity.y < 0 || velocity.x != 0)
        {
            anim.Play("Walking_front");
        }
        else
        {
            anim.Play("Idle");
        }

        if (velocity.x > 0)
        {
            player2_viewable.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (velocity.x < 0)
        {
            player2_viewable.GetComponent<SpriteRenderer>().flipX = false;
        }
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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

    public Vector3 orientPos(Orientation o, Vector3 pos, bool tiles = false){
        Vector3 new_pos = pos;
        switch(orientation)
        {
            case Orientation.NE:
                break;
            case Orientation.SW:
                new_pos.x = -pos.x - 1 + (tiles ? 0 : 2);
                new_pos.y = -pos.y - 1 + (tiles ? 0 : 2);
                break;
            default: Debug.Log("Invalid orientation"); break;
        }

        return new_pos;
    }

    public Vector3Int createBlockViewable(Block block, Vector3Int pos){ //block type, position
        Vector3Int new_pos = Vector3Int.RoundToInt(orientPos(orientation, pos, true));

        // Vector3Int offset = new Vector3Int();
        // switch (orientation){
        //     case Orientation.NE:
        //         break;
        //     case Orientation.SW:
        //         offset.x = -1;
        //         offset.y = -1;
        //         break;
        //     default: Debug.Log("Invalid orientation"); break;

        // }

        tilemap.SetTile(new_pos, block.tile_views[(int) orientation]);

        // // tile coordinates
        return pos;
    }

    public void updateBlockViewable(Vector3Int blockViewable)
    {


    }

    public void updatePlayer1(Vector3 world_position, Vector3 velocity)
    {

        player1_viewable.transform.position = tilemap.CellToLocalInterpolated(
            orientPos(orientation, world_position)
        );
        player1_viewable.transform.position += transform.position;

        // print(orientation.ToString() + ' ' + world_position.ToString() + ' ' + player1_viewable.transform.position.ToString());



        Animator anim = player1_viewable.GetComponent<Animator>();

        if (orientation == Orientation.NE)
        {
            if (velocity.y > 0) anim.Play("Walking_back");
            else if (velocity.y < 0 || velocity.x != 0) anim.Play("Walking_front");
            else anim.Play("Idle");
        }
        else if (orientation == Orientation.SW)
        {
            if (velocity.y > 0) anim.Play("Walking_front");
            else if (velocity.y < 0 || velocity.x != 0) anim.Play("Walking_back");
            else anim.Play("Idle");
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

    public void updatePlayer2(Vector3 world_position, Vector3 velocity)
    {
        player2_viewable.transform.position = tilemap.CellToLocalInterpolated(
            orientPos(orientation, world_position)
        );
        player2_viewable.transform.position += transform.position;       

        Animator anim = player2_viewable.GetComponent<Animator>();

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

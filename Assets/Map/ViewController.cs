using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


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
                new_pos.x = pos.x + (tiles ? 0 : -1);
                new_pos.y = pos.y + (tiles ? 0 : -1);
                break;
            case Orientation.SW:
                // new_pos.x = -pos.x - 1 + (tiles ? 0 : 2);
                // new_pos.y = -pos.y - 1 + (tiles ? 0 : 2);
                new_pos.x = -pos.x - 1  + (tiles ? 0 : 1);
                new_pos.y = -pos.y - 1 + (tiles ? 0 : 1);
                break;
            default: Debug.Log("Invalid orientation"); break;
        }

        return new_pos;
    }

    public GameObject createBlockViewable(Block block, Vector3Int pos){ //block type, position
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

        GameObject tileViewable = new GameObject("Tile");
        tileViewable.transform.parent = tilemap.transform;

        tileViewable.AddComponent<SpriteRenderer>();
        tileViewable.GetComponent<SpriteRenderer>().sprite = block.tile_views[(int) orientation].sprite;
        Vector3 screen_coords = tilemap.CellToLocalInterpolated(new_pos);
        screen_coords.z = ((float) (new_pos.x + new_pos.y))/100f;
        //tileViewable.transform.position = screen_coords;
        tileViewable.transform.localPosition = screen_coords;

        //tilemap.SetTile(new_pos, block.tile_views[(int) orientation]);

        // // tile coordinates
        return tileViewable;
    }

    public void updateBlockViewable(Vector3Int blockViewable)
    {


    }

    public void updatePlayer1(Vector3 world_position, Vector3 velocity)
    {
        var new_pos = orientPos(orientation, world_position);
        Vector3 screen_coords = transform.TransformPoint(tilemap.CellToLocalInterpolated(new_pos));
        screen_coords.z = ((float)(new_pos.x + new_pos.y)) / 100f - 0.008f;
        player1_viewable.transform.position = screen_coords;

        // player1_viewable.transform.position += transform.position;

        // print(orientation.ToString() + ' ' + world_position.ToString() + ' ' + player1_viewable.transform.position.ToString());

        const float EPSILON = 0.0001f;

        Animator anim = player1_viewable.GetComponent<Animator>();

        if (orientation == Orientation.NE)
        {
            if (velocity.y > EPSILON) anim.Play("Walking_back");
            else if (velocity.y < -EPSILON || Math.Abs(velocity.x) > EPSILON) anim.Play("Walking_front");
            else anim.Play("Idle");
        }
        else if (orientation == Orientation.SW)
        {
            if (velocity.y > EPSILON) anim.Play("Walking_front");
            else if (velocity.y < -EPSILON || Math.Abs(velocity.x) > EPSILON) anim.Play("Walking_back");
            else anim.Play("Idle");
        }

        if (velocity.x > EPSILON)
        {
            player1_viewable.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (velocity.x < EPSILON)
        {
            player1_viewable.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void updatePlayer2(Vector3 world_position, Vector3 velocity)
    {
        var new_pos = orientPos(orientation, world_position);
        Vector3 screen_coords = transform.TransformPoint(tilemap.CellToLocalInterpolated(new_pos));
        screen_coords.z = ((float)(new_pos.x + new_pos.y)) / 100f - 0.008f;
        player1_viewable.transform.position = screen_coords;     

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

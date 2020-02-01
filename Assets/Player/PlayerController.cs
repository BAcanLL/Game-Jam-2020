using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyMaps
{
    static public KeyMap wasd = new KeyMap(KeyCode.W, 
                                    KeyCode.S, 
                                    KeyCode.A, 
                                    KeyCode.D);
    static public KeyMap arrows = new KeyMap(KeyCode.UpArrow, 
                                      KeyCode.DownArrow, 
                                      KeyCode.LeftArrow, 
                                      KeyCode.RightArrow);
}


public struct KeyMap
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public KeyMap(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }
}


public class PlayerController : MonoBehaviour
{
    // TEMP Until external manager assigns keymaps?
    public enum Player {
    PLAYER_1,
    PLAYER_2
    };

    private Animator anim;
    private bool faceRight;

    public float speed = 5f;
    public Player player = Player.PLAYER_1;
    Rigidbody2D rbody;

    KeyMap current_keymap;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // TODO Have external task assign keymap?

        if(player == Player.PLAYER_1) current_keymap = KeyMaps.wasd;
        else if(player == Player.PLAYER_2) current_keymap = KeyMaps.arrows;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;

        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 input_vector = new Vector2(0,0);

        if (Input.GetKey(current_keymap.left)) input_vector += Vector2.left;
        if (Input.GetKey(current_keymap.right)) input_vector += Vector2.right;
        if (Input.GetKey(current_keymap.up)) input_vector += Vector2.up;
        if (Input.GetKey(current_keymap.down)) input_vector += Vector2.down;

        if (input_vector.y < 0 || input_vector.x != 0) 
        {
            anim.Play("Walking_front");
        } else if (input_vector.y > 0) 
        {
            anim.Play("Walking_back");
        } else
        {
            anim.Play("Idle");
        }

        if (input_vector.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (input_vector.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        // Debug.Log(KeyCode.DownArrow.GetType());

        // Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        input_vector = Vector2.ClampMagnitude(input_vector, 1);

        Vector2 movement = input_vector * speed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        // renderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }
}

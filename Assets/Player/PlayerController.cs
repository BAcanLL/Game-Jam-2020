using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyMaps
{
    static public KeyMap wasd = new KeyMap(KeyCode.W, 
                                    KeyCode.S, 
                                    KeyCode.A, 
                                    KeyCode.D,
                                    KeyCode.E,
                                    KeyCode.R
        );
    static public KeyMap arrows = new KeyMap(KeyCode.UpArrow, 
                                      KeyCode.DownArrow, 
                                      KeyCode.LeftArrow, 
                                      KeyCode.RightArrow,
                                      KeyCode.RightShift,
                                      KeyCode.Return
        );
}


public struct KeyMap
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;
    public KeyCode shoot;

    public KeyMap(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode interact, KeyCode shoot)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.interact = interact;
        this.shoot = shoot;
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

    private List<IInteractable> currentInteractables = new List<IInteractable>();

    public float speed = 5f;
    public Player player = Player.PLAYER_1;
    Rigidbody2D rbody;
    BulletSpawner bulletSpawner;

    KeyMap current_keymap;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletSpawner = GetComponent<BulletSpawner>();

        // TODO Have external task assign keymap?

        if(player == Player.PLAYER_1) current_keymap = KeyMaps.wasd;
        else if(player == Player.PLAYER_2) current_keymap = KeyMaps.arrows;
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity.y > 0)
        {
            anim.Play("Walking_back");
        }
        else if (rbody.velocity.y < 0 || rbody.velocity.x != 0)
        {
            anim.Play("Walking_front");
        }
        else
        {
            anim.Play("Idle");
        }

        if (rbody.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rbody.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKeyDown(current_keymap.interact))
        {
            interactWithObjects();
        }

        if (bulletSpawner)
        {
            bulletSpawner.SetFiring(Input.GetKey(current_keymap.shoot));
        }
        
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

        // Debug.Log(KeyCode.DownArrow.GetType());

        // Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        input_vector = Vector2.ClampMagnitude(input_vector, 1);

        Vector2 movement = input_vector * speed;
        // Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        // renderer.SetDirection(movement);

        rbody.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentInteractables.Add(collision.gameObject.GetComponent<IInteractable>());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentInteractables.Remove(collision.gameObject.GetComponent<IInteractable>());
    }

    private void interactWithObjects()
    {
        if(currentInteractables.Count > 0)
        {
            currentInteractables[0].interact(gameObject);
        }
    }
}

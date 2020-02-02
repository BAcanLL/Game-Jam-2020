using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs
{
    AXIS_X, AXIS_Y, AXIS_CAM_X, AXIS_CAM_Y,
    BUTTON_INTERACT, BUTTON_FIRE
};

public struct InputMap
{


    Dictionary<Inputs, string> inputMap;
    HashSet<string> strings;
    [SerializeField]
    Dictionary<string, bool> currState;
    Dictionary<string, bool> lastState;

    public InputMap(int player)
    {
        string suffix = "_P" + player.ToString();

        strings = new HashSet<string>();
        inputMap = new Dictionary<Inputs, string>();
        currState = new Dictionary<string, bool>();
        lastState = new Dictionary<string, bool>();

        inputMap.Add(Inputs.AXIS_X, "Horizontal" + suffix);
        inputMap.Add(Inputs.AXIS_Y, "Vertical" + suffix);
        inputMap.Add(Inputs.AXIS_CAM_X, "CameraX" + suffix);
        inputMap.Add(Inputs.AXIS_CAM_Y, "CameraY" + suffix);

        inputMap.Add(Inputs.BUTTON_INTERACT, "Interact" + suffix);
        inputMap.Add(Inputs.BUTTON_FIRE, "Fire" + suffix);


        foreach (KeyValuePair<Inputs, string> kv in inputMap)
        {
            strings.Add(kv.Value);
        }

        foreach (string s in strings)
        {
            currState[s] = false;
            lastState[s] = false;
        }
    }

    public void UpdateInputStates()
    {
        foreach (string s in strings)
        {
            lastState[s] = currState[s];
            currState[s] = Input.GetButton(s);
        }
    }

    public bool GetKeyDown(Inputs i)
    {
        string s = inputMap[i];
        return currState[s] && !lastState[s];
    }

    public bool GetKey(Inputs i)
    {
        return currState[inputMap[i]];
    }

    public bool GetKeyUp(Inputs i)
    {
        string s = inputMap[i];
        return !currState[s] && lastState[s];
    }

    public float GetAxis(Inputs i)
    {
        return Input.GetAxis(inputMap[i]);
    }
}

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public ViewManager view_manager;

    // TEMP Until external manager assigns keymaps?
    public enum Player {
        PLAYER_1,
        PLAYER_2
    };

    // private Animator anim;


    private List<IInteractable> currentInteractables = new List<IInteractable>();

    public float speed = 5f;
    public Player player = Player.PLAYER_1;
    Rigidbody rbody;
    BulletSpawner bulletSpawner;

    InputMap inputMap;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        // anim = GetComponent<Animator>();
        bulletSpawner = GetComponent<BulletSpawner>();

        // TODO Have external task assign keymap?

        if(player == Player.PLAYER_1) inputMap = new InputMap(1);
        else if(player == Player.PLAYER_2) inputMap = new InputMap(2);
    }

    // Update is called once per frame
    void Update()
    {
        inputMap.UpdateInputStates();

        if(view_manager)
        {
            if(player == Player.PLAYER_1)
            {
                foreach(ViewController view in view_manager.GetViewControllers()) 
                    view.updatePlayer1(rbody.position, rbody.velocity);
            }

            if(player == Player.PLAYER_2)
            {
                foreach(ViewController view in view_manager.GetViewControllers()) 
                    view.updatePlayer2(rbody.position, rbody.velocity);
            }
        }
        else Debug.Log("Must set view_controller for player");

        if (inputMap.GetKeyDown(Inputs.BUTTON_INTERACT))
        {
            interactWithObjects();
        }

        if (bulletSpawner)
        {
            Vector2 firingDirection = new Vector2(inputMap.GetAxis(Inputs.AXIS_CAM_X), inputMap.GetAxis(Inputs.AXIS_CAM_Y));
            // Set a new angle if non zero aim input
            if (firingDirection != Vector2.zero)
            {
                float angle = Vector2.SignedAngle(Vector2.down, firingDirection);
                bulletSpawner.SetRotation(new Vector3(0, 0, angle));
            }
            bulletSpawner.SetFiring(inputMap.GetKey(Inputs.BUTTON_FIRE));
        }
        
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;

        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 input_vector = new Vector2(inputMap.GetAxis(Inputs.AXIS_X), inputMap.GetAxis(Inputs.AXIS_Y));


        //if (Input.GetKey(current_keymap.left)) input_vector += Vector2.left;
        //if (Input.GetKey(current_keymap.right)) input_vector += Vector2.right;
        //if (Input.GetKey(current_keymap.up)) input_vector += Vector2.up;
        //if (Input.GetKey(current_keymap.down)) input_vector += Vector2.down;

        // Debug.Log(KeyCode.DownArrow.GetType());

        // Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        input_vector = Vector2.ClampMagnitude(input_vector, 1);

        Vector2 movement = input_vector * speed;
        // Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        // renderer.SetDirection(movement);

        rbody.velocity = movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentInteractables.Add(collision.gameObject.GetComponent<IInteractable>());
    }

    private void OnCollisionExit(Collision collision)
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

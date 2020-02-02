using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour, IInteractable
{
    private State state = State.WORKING;
    public enum State
    {
        BROKEN,
        WORKING
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool interact(GameObject user)
    {
        if (state == State.BROKEN)
        {
            state = State.WORKING;
            return true;
        }

        return false;
    }
}

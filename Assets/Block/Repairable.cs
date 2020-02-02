using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repairable : MonoBehaviour, IInteractable
{

    public enum State
    {
        BROKEN,
        WORKING
    }

    public int lifeInSeconds = 5;
    public State state = State.WORKING;

    private Healthbar healthbar = null;
    private float healthTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Healthbar>();
        healthTick = healthbar.maxHealth / lifeInSeconds * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.WORKING)
        {
            if (healthbar.takeDamage(healthTick))
            {
                state = State.BROKEN;
            }
        }
    }

    public bool interact(GameObject user)
    {
        if (state == State.BROKEN)
        {
            // Debug.Log("Repaired");

            state = State.WORKING;
            healthbar.fullHeal();

            return true;
        }

        return false;
    }
}

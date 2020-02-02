﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterController : MonoBehaviour, IInteractable
{
    public Sprite brokenSprite;
    public int periodInSeconds = 1;

    private Animator anim;
    private InventoryManager inventory;
    private Timer harvestTimer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        inventory = GetComponent<InventoryManager>();
        harvestTimer = new Timer(periodInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        harvestTimer.Update();
        //Debug.Log(harvestTimer.time);

        if(GetComponent<Repairable>() && GetComponent<Repairable>().state == Repairable.State.BROKEN)
        {
            anim.enabled = false;
            GetComponent<SpriteRenderer>().sprite = brokenSprite;
        } else if (GetComponent<Repairable>() && GetComponent<Repairable>().state == Repairable.State.WORKING)
        {
            anim.enabled = true;

            if (harvestTimer.Done)
            {
                harvest();
                harvestTimer.Reset();
            }
            
        }
    }

    public bool interact(GameObject user)
    {
        // Debug.Log("Interact");

        if (GetComponent<Repairable>() 
            && GetComponent<Repairable>().state == Repairable.State.WORKING 
            && user.GetComponent<InventoryManager>())
        {
            int itemCount = inventory.GetItemCount();
            Item item = inventory.RemoveAllItems();
            user.GetComponent<InventoryManager>().AddItems(item, itemCount);

            return true;
        }

        return false;
    }

    private void harvest()
    {
        if (GetComponent<InteractiveCollider>())
        {
            //Debug.Log("Harvest");

            GetComponent<InteractiveCollider>().interactAll();

        }
    }
}

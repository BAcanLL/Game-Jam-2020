﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Resourceable : MonoBehaviour, IInteractable
{
    public enum State
    {
        AVAILABLE,
        DEPLETED
    }

    public GameObject resourceItemPrefab;
    public State state = State.AVAILABLE;
    public int quantity = 5;
    public int cooldown = 3;

    private Item resource;
    private Healthbar healthbar = null;
    private float resourcingDamage = 0; // Damage taken from harvesting a resource
    private float healingTick = 0;

    // Start is called before the first frame update
    void Start()
    {
        resource = resourceItemPrefab.GetComponent<Item>();
        healthbar = GetComponent<Healthbar>();

        if (healthbar)
        {
            resourcingDamage = healthbar.maxHealth / quantity;
            healingTick = healthbar.maxHealth / cooldown * Time.deltaTime;

            Debug.Log(healingTick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.DEPLETED)
        {
            if (healthbar && healthbar.heal(healingTick))
            {
                state = State.AVAILABLE;
                healthbar.disabled = false;
            }
        }
    }

    public bool interact(GameObject user)
    {
        //Debug.Log("Interacted.");

        if (user.GetComponent<InventoryManager>() == null)
        {
            Debug.LogError("User is missing an inventory");
            return false;
        }

        if (state == State.DEPLETED)
        {
            Debug.Log("Resource is depleted.");
            return false;
        }

        user.GetComponent<InventoryManager>().AddItem(resource);

        if (healthbar && healthbar.takeDamage(resourcingDamage))
        {
            state = State.DEPLETED;
            healthbar.disabled = true;
            //StartCoroutine(ReplenishAfterDelay(cooldown));
        }

        return true;
    }

    IEnumerator ReplenishAfterDelay(float seconds)
    {
        if (healthbar)
        {
            yield return new WaitForSeconds(seconds);

            healthbar.fullHeal();
        }
    }
}

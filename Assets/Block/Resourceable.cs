using System.Collections;
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
    private float resourcingDamage = 0; // Damage taken from harvesting a resource

    // Start is called before the first frame update
    void Start()
    {
        resource = resourceItemPrefab.GetComponent<Item>();

        if (GetComponent<Healthbar>() != null)
        {
            resourcingDamage = GetComponent<Healthbar>().maxHealth / quantity;
        }
    }

    // Update is called once per frame
    void Update()
    {

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

        if (GetComponent<Healthbar>() != null)
        {
            if (GetComponent<Healthbar>().takeDamage(resourcingDamage))
            {
                state = State.DEPLETED;
                StartCoroutine(ReplenishAfterDelay(cooldown));
            }
        }

        return true;
    }

    IEnumerator ReplenishAfterDelay(float seconds)
    {
        if (GetComponent<Healthbar>() != null)
        {
            yield return new WaitForSeconds(seconds);

            GetComponent<Healthbar>().fullHeal();
        }
    }
}

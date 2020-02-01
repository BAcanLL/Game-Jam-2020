using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] inventoryItems;

    Inventory inventory;
    int activeIndex;

    void Start()
    {
        inventory = new Inventory();
        foreach (GameObject o in inventoryItems)
        {
            Item item = o.GetComponent<Item>();
            if (item)
            {
                inventory.CreateCollection(item);
            }
        }
    }

    public void UseItem()
    {
        if (inventory.GetItemCount(activeIndex) > 0)
        {
            inventory.UseItem(activeIndex);
        }
        else 
        {
            // Indicate some sort of failure
        }
    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item);
    }

    public void ScrollActiveItem(bool right)
    {
        if (right)
        {
            if (++activeIndex >= inventory.GetSize())
            {
                activeIndex = 0;
            }
        }
        else 
        {
            if (--activeIndex < 0)
            {
                activeIndex = (inventory.GetSize() > 0) ? inventory.GetSize() - 1 : 0;
            }
        }
    }
}

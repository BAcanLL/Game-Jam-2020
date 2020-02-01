using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] inventoryItems;

    Inventory inventory;
    int activeIndex;

    public class InventoryUI : UI
    {
        List<Image> inventoryIcons;
        InventoryManager inventoryMgr;
        public InventoryUI(GameObject anchor, InventoryManager inventoryMgr)
        {
            this.inventoryMgr = inventoryMgr;
            inventoryIcons = new List<Image>();
            Inventory inventory = inventoryMgr.inventory;

            float spacing = 500;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                Image image = new GameObject().AddComponent<Image>();
                image.rectTransform.SetParent(anchor.transform);
                image.transform.localPosition = new Vector3(i * spacing, 5.0f, 0.0f);
                image.sprite = inventory.GetItem(i).GetComponent<SpriteRenderer>().sprite;
            }

        }

        public override void UpdateUI(float deltaTime)
        {
            for (int i = 0; i < inventoryIcons.Count; i++)
            {
                if (i == inventoryMgr.activeIndex)
                {
                    inventoryIcons[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else
                {
                    inventoryIcons[i].color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
            }
        }
    }

    InventoryManager()
    {
        inventory = new Inventory();
    }

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

    public Inventory GetInventory()
    {
        return inventory;
    }
}

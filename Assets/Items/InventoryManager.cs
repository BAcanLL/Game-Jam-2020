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
        List<Text> itemCounts;
        InventoryManager inventoryMgr;
        Inventory inventory;
        public InventoryUI(GameObject anchor, InventoryManager inventoryMgr)
        {
            this.inventoryMgr = inventoryMgr;
            inventoryIcons = new List<Image>();
            itemCounts = new List<Text>();
            inventory = inventoryMgr.inventory;

            GameObject o = new GameObject("Inventory UI", typeof(RectTransform));
            o.transform.SetParent(anchor.transform);
            o.transform.Translate(new Vector2(64, 96));

            float spacing = 128;
            for (int i = 0; i < inventory.GetSize(); i++)
            {
                Image image = new GameObject(inventory.GetItem(i).itemID).AddComponent<Image>();
                image.rectTransform.SetParent(o.transform);
                image.transform.localPosition = new Vector3(i * spacing, 5.0f, 0.0f);
                image.sprite = inventory.GetItem(i).GetComponent<SpriteRenderer>().sprite;
                inventoryIcons.Add(image);

                Text count = new GameObject(inventory.GetItem(i).itemID + " count").AddComponent<Text>();
                count.rectTransform.SetParent(image.transform);
                count.rectTransform.localPosition = Vector3.zero;
                count.text = inventory.GetItemCount(i).ToString();
                count.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                itemCounts.Add(count);
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
                itemCounts[i].text = inventory.GetItemCount(i).ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory { get; protected set; }
    public int activeIndex { get; protected set;  }

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
            for (int i = 0; i < Inventory.MAX_SIZE; i++)
            {
                Image image = new GameObject().AddComponent<Image>();
                image.rectTransform.SetParent(o.transform);
                image.transform.localPosition = new Vector3(i * spacing, 5.0f, 0.0f);
                inventoryIcons.Add(image);

                Text count = new GameObject().AddComponent<Text>();
                count.rectTransform.SetParent(image.transform);
                count.rectTransform.localPosition = Vector3.zero;
                count.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
                itemCounts.Add(count);
            }
        }

        public override void UpdateUI(float deltaTime)
        {
            for (int i = 0; i < inventoryIcons.Count; i++)
            {
                Item item = inventory.GetItem(i);
                if (item)
                {
                    inventoryIcons[i].sprite = item.GetComponent<SpriteRenderer>().sprite;
                    inventoryIcons[i].gameObject.SetActive(true);
                    itemCounts[i].text = inventory.GetItemCount(i).ToString();

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
                else {
                    inventoryIcons[i].sprite = null;
                    inventoryIcons[i].gameObject.SetActive(false);
                    itemCounts[i].text = "";
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
    }

    public int GetItemCount()
    {
        return inventory.GetItemCount(activeIndex);
    }

    public void RemoveItems(int n = 1)
    {
        if (inventory.GetItemCount(activeIndex) > 0)
        {
            inventory.RemoveItems(activeIndex, n);
        }
        else 
        {
            // Indicate some sort of failure
        }
    }

    public void RemoveAllItems()
    {
        RemoveItems(inventory.GetItemCount(activeIndex));
    }

    public void AddItems(Item item, int n = 1)
    {
        inventory.AddItems(item, n);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private struct Collection
    {
        public int count;
        public Item item;
    }

    public GameObject[] items;
    private List<Collection> collections;
    private int activeIndex;

    Inventory()
    {
        collections = new List<Collection>();
    }

    void OnStart()
    {
        foreach (GameObject g in items)
        {
            Item item = g.GetComponent<Item>();
            if (item)
            {
                createCollection(item);
            }
        }
    }


    private void createCollection(Item item)
    {
        Collection c = new Collection();
        c.count = 0;
        c.item = item;

        collections.Add(c);
    }
}

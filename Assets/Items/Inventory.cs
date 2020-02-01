using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private class Collection
    {
        public int count;
        public Item item;
    }

    private List<Collection> collections;
    public Inventory()
    {
        collections = new List<Collection>();
    }

    public void CreateCollection(Item item)
    {
        Collection c = new Collection();
        c.count = 0;
        c.item = item;

        collections.Add(c);
    }

    public void UseItem(int index)
    {
        if (index >= 0 && index < collections.Count && collections[index].count > 0)
        {
            Collection c = collections[index];
            c.count--;
            collections[index] = c;
        }
    }

    public Item GetItem(int index)
    {
        if (index >= 0 && index < collections.Count)
        {
            return collections[index].item;
        }
        return null;
    }

    public int GetItemCount(int index)
    {
        if (index >= 0 && index < collections.Count)
        {
            return collections[index].count;
        }
        return 0;
    }

    public bool AddItem(Item item)
    {
        foreach (Collection c in collections)
        {
            if (c.item.itemID == item.itemID)
            {
                c.count += 1;
                return true;
            }
        }
        return false;
    }

    public int GetSize()
    {
        return collections.Count;
    }
}

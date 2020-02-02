using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public const int MAX_SIZE = 10;

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

    public Item RemoveItems(int index, int num)
    {
        if (num > 0 && index >= 0 
            && index < collections.Count 
            && collections[index].count > 0)
        {
            if (num > collections[index].count)
            {
                num = collections[index].count;
            }

            Collection c = collections[index];
            c.count -= num;
            collections[index] = c;

            return collections[index].item;
        }

        return null;
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

    public bool AddItems(Item item, int num)
    {
        {
            foreach (Collection c in collections)
            {
                if (c.item.itemID == item.itemID)
                {
                    c.count += num;
                    return true;
                }
            }

            CreateCollection(item);
        }

        return false;
    }

    public int GetSize()
    {
        return collections.Count;
    }
}

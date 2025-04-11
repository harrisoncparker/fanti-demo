using System;
using System.Collections.Generic;

[Serializable]
public class InventoryModel
{
    [Serializable]
    public struct ItemQuantity
    {
        public string itemId;
        public int quantity;

        public ItemQuantity(string id, int qty)
        {
            itemId = id;
            quantity = qty;
        }
    }

    public List<ItemQuantity> items = new List<ItemQuantity>();

    public void AddItem(string id, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemId != id) continue;
            
            var item = items[i];
            item.quantity += amount;
            items[i] = item;
            return;
        }

        items.Add(new ItemQuantity(id, amount));
    }

    public bool RemoveItem(string id, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemId != id) continue;

            var item = items[i];
            if (item.quantity < amount) return false;

            item.quantity -= amount;
            if (item.quantity == 0)
            {
                items.RemoveAt(i);
            }
            else
            {
                items[i] = item;
            }
            return true;
        }
        return false;
    }

    public int GetQuantity(string id)
    {
        foreach (var item in items)
        {
            if (item.itemId == id) return item.quantity;
        }
        return 0;
    }
} 
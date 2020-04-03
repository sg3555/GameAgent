using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 14;

    private List<IInventortItem> mItems = new List<IInventortItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public event EventHandler<InventoryEventArgs> ItemRemoved;

    public void AddItem(IInventortItem item)
    {
        if(mItems.Count < SLOTS)
        {
            mItems.Add(item);
            item.OnPickup();

            if (ItemAdded != null)
            {
                ItemAdded(this, new InventoryEventArgs(item));
            }
        }
    }

    public void RemoveItem(IInventortItem item)
    {
        if (mItems.Contains(item))
        {
            Debug.Log("Remove");
            mItems.Remove(item);
            item.OnDrop();

            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}

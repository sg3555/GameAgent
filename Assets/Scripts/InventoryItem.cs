using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventortItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup();

}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventortItem item)
    {
        Item = item;
    }

    public IInventortItem Item;
}

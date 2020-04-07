using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 14;   //인벤토리 슬롯 수
    private List<IInventoryItem> mItems = new List<IInventoryItem>();   //인벤토리에 등록된 아이템 리스트
    public event EventHandler<InventoryEventArgs> ItemAdded;    //인벤토리에 아이템을 넣을 때 발생하는 이벤트 핸들러
    public event EventHandler<InventoryEventArgs> ItemRemoved;  //인벤토리에서 아이템을 뺄 때 발생하는 이벤트 핸들러

    public void AddItem(IInventoryItem item)
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

    public void RemoveItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();
            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}

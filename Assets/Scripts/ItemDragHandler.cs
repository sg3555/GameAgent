using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public IInventortItem Item { get; set; }
    public Inventory inventory;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(eventData);
        Debug.Log(Item);
        inventory.RemoveItem(Item);
        transform.localPosition = Vector3.zero;
    }
}

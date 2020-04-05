using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public IInventoryItem Item { get; set; }
    public Inventory inventory;
    GameObject ge;
    Collider2D colid;
    private void Awake()
    {
        //colid = ge.GetComponent<Collider2D>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Item.OnUse();
        
        ge = Item.GetGameObject();
        ge.SetActive(true);
        inventory.RemoveItem(Item);
        //Debug.Log(colid.isTrigger);
        //Item.gameObject.SetActive(true);
        //Debug.Log(Item.transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 objPosition = new Vector2(Mathf.Floor(temp.x) + 0.5f, Mathf.Floor(temp.y) + 0.5f);
        ge.transform.position = objPosition;

        //Debug.Log(ge.transform.position);
        //ge.transform.position = Input.mousePosition;
        //Item.transform.position
        //transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData);
        //Debug.Log(Item);
        //transform.localPosition = Vector2.zero;
        //inventory.RemoveItem(Item);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision);
    }

    public void OnMouseDown()
    {
        //Debug.Log("draggg");
    }
}

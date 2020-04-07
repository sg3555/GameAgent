using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

/*
 * ItemDragHandler에서는 인벤토리에서 아이템을 드래그 및 속성변경 부분만 처리하고
 * 오브젝트 충돌판정은 각 오브젝트에 있는 Drag스크립트에서 처리한다.
 */

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public IInventoryItem Item { get; set; }    //인벤토리에 IInventoryItem타입으로 들어가있는 오브젝트
    public Inventory inventory; //인벤토리
    GameObject invenItem;   //IInventoryItem인 채로는 오브젝트 속성을 변경할 수 없어서 다시 GameObject형식으로 변경

    Rigidbody2D rigid;  //물리엔진
    Drag drag;  //해당 오브젝트의 Drag 스크립트

    //드래그를 시작할 때 발생하는 이벤트 핸들러
    public void OnBeginDrag(PointerEventData eventData)
    {
        invenItem = Item.GetGameObject();
        if (invenItem != null)
        {
            rigid = invenItem.GetComponent<Rigidbody2D>();
            drag = invenItem.GetComponent<Drag>();
        }
        invenItem.SetActive(true);
        rigid.bodyType = RigidbodyType2D.Dynamic;
        inventory.RemoveItem(Item);
    }

    //드래그중에 발생하는 이벤트 핸들러
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 objPosition = new Vector2(Mathf.Floor(temp.x) + 0.5f, Mathf.Floor(temp.y) + 0.5f);
        invenItem.transform.position = objPosition;
    }

    //드래그를 끝낼 때 발생하는 이벤트 핸들러
    public void OnEndDrag(PointerEventData eventData)
    {
        //해당 오브젝트의 Drag 스크립트에서 충돌검사한 bool값을 토대로 충돌이있으면 오브젝트를 다시 인벤토리로 수납
        if (drag.isInventory || drag.deadlock)
        {
            IInventoryItem item = invenItem.GetComponent<IInventoryItem>();
            if (item != null)
            {
                inventory.AddItem(item);
            }
        }
        rigid.bodyType = RigidbodyType2D.Static;
    }
}

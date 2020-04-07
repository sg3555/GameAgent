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
    public IInventoryItem Item { get; set; }
    public Inventory inventory;
    GameObject invenItem;

    bool isInventory;
    bool startgame; //게임시작 bool
    //Vector2 originposition, pastposition;   //각각 최초위치, 마우스를 놓기전 위치
    bool deadlock;  //타일이 다른 객체에 겹치는것을 방지하기 위한 교착상태 bool
    //SpriteRenderer thissprite; //간판전용 스프라이트구동기
    SpriteRenderer[] tiles; //플랫폼전용 스프라이트구동기
    Rigidbody2D rigid; //물리엔진
    Collider2D colid; //충돌자
    Drag drag;


    private void Start()
    {
        //게임시작시 최초위치 설정(나중에 UI인벤토리에 넣으면 수정해아 함)
        //originposition = this.gameObject.transform.position;
        deadlock = false;
        startgame = false;
        isInventory = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Item.OnUse();
        Debug.Log("1");
        invenItem = Item.GetGameObject();
        if (invenItem != null)
        {
            rigid = invenItem.GetComponent<Rigidbody2D>();
            tiles = invenItem.gameObject.GetComponentsInChildren<SpriteRenderer>();
            colid = invenItem.GetComponent<Collider2D>();
            drag = invenItem.GetComponent<Drag>();
        }
        invenItem.SetActive(true);

        //Debug.Log(colid.isTrigger);
        //Debug.Log(drag.isInventory);

        rigid.bodyType = RigidbodyType2D.Dynamic;

        inventory.RemoveItem(Item);
        //Debug.Log(colid.isTrigger);
        //Item.gameObject.SetActive(true);
        //Debug.Log(Item.transform.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("2");
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 objPosition = new Vector2(Mathf.Floor(temp.x) + 0.5f, Mathf.Floor(temp.y) + 0.5f);
        invenItem.transform.position = objPosition;

        //Debug.Log(ge.transform.position);
        //ge.transform.position = Input.mousePosition;
        //Item.transform.position
        //transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("3");
        //Debug.Log(eventData);
        //Debug.Log(Item);
        //transform.localPosition = Vector2.zero;
        //inventory.RemoveItem(Item);
        //if (deadlock)
        //    transform.position = pastposition;
        //Debug.Log(drag.isInventory);
        //Debug.Log(drag.deadlock);

        if (drag.isInventory || drag.deadlock)
        {
            //Debug.Log(this.gameObject);
            IInventoryItem item = invenItem.GetComponent<IInventoryItem>();
            Debug.Log(item);
            if (item != null)
            {
                
                inventory.AddItem(item);
                //Debug.Log("ok");
            }

        }

        rigid.bodyType = RigidbodyType2D.Static;
        //Debug.Log(rigid.bodyType);
    }

    ////타일이 다른 객체에 겹쳤을 경우
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log(collision);
    //    Debug.Log("rty");
    //    if (!startgame)
    //    {
            
    //        if (collision.gameObject.tag == "Inventory")
    //        {
                
    //            isInventory = true;
    //        }
    //        else
    //        {
    //            //자식객체의 각 타일들을 붉은 색으로 바꾸고
    //            foreach (SpriteRenderer objec in tiles)
    //                objec.color = new Color(1, 0, 0);
    //            //교착상태 bool을 true로 변경
    //            deadlock = true;
    //        }
    //    }

    //}

    ////타일이 다른 객체와 겹친 상태에서 빠져 나왔을 경우
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    Debug.Log(collision);
    //    Debug.Log("zxc");
    //    if (!startgame)
    //    {
    //        //원상복귀
    //        foreach (SpriteRenderer objec in tiles)
    //            objec.color = new Color(255, 255, 255);
    //        deadlock = false;
    //        isInventory = false;
    //    }

    //}

    ////위 OnCollisionEnter2D와 동일
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(collision);
    //    Debug.Log("qwe");
    //    if (!startgame)
    //    {
            
    //        if (collision.gameObject.tag == "Inventory")
    //        {
                
    //            isInventory = true;
    //        }
    //        else
    //        {
    //            foreach (SpriteRenderer objec in tiles)
    //                objec.color = new Color(1, 0, 0);
    //            deadlock = true;
    //        }

    //    }
    //}

    ////위 OnCollisionExit2D와 동일
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log(collision);
    //    Debug.Log("asd");
    //    if (!startgame)
    //    {
    //        foreach (SpriteRenderer objec in tiles)
    //        {
    //            objec.color = new Color(255, 255, 255);
    //        }
    //        deadlock = false;
    //        isInventory = false;
    //    }
    //}
}

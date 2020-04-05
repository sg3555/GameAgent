using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마우스로 움직일 수 있는 타일용 스크립트

public class Drag : MonoBehaviour
{
    bool startgame; //게임시작 bool
    Vector2 originposition, pastposition;   //각각 최초위치, 마우스를 놓기전 위치
    bool deadlock;  //타일이 다른 객체에 겹치는것을 방지하기 위한 교착상태 bool
    SpriteRenderer thissprite; //간판전용 스프라이트구동기
    SpriteRenderer[] tiles; //플랫폼전용 스프라이트구동기
    Rigidbody2D rigid; //물리엔진
    Collider2D colid; //충돌자

    bool isInventory;
    public Inventory inventory;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        tiles = gameObject.GetComponentsInChildren<SpriteRenderer>();
        colid = GetComponent<Collider2D>();
        thissprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //게임시작시 최초위치 설정(나중에 UI인벤토리에 넣으면 수정해아 함)
        originposition = this.gameObject.transform.position;
        deadlock = false;
        startgame = false;
        isInventory = false;
    }

    //타일이 다른 객체에 겹쳤을 경우
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!startgame)
        {
            if (collision.gameObject.tag == "Inventory")
            {
                Debug.Log(collision);
                isInventory = true;
            }
            else
            {
                //자식객체의 각 타일들을 붉은 색으로 바꾸고
                foreach (SpriteRenderer objec in tiles)
                    objec.color = new Color(1, 0, 0);
                //교착상태 bool을 true로 변경
                deadlock = true;
            }
        }
        
    }

    //타일이 다른 객체와 겹친 상태에서 빠져 나왔을 경우
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!startgame)
        {
            //원상복귀
            foreach (SpriteRenderer objec in tiles)
                objec.color = new Color(255, 255, 255);
            deadlock = false;
            isInventory = false;
        }
        
    }

    //위 OnCollisionEnter2D와 동일
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!startgame)
        {
            if (collision.gameObject.tag == "Inventory")
            {
                //Debug.Log(collision);
                isInventory = true;
            }
            else
            {
                foreach (SpriteRenderer objec in tiles)
                    objec.color = new Color(1, 0, 0);
                deadlock = true;
            }
            
        }
    }

    //위 OnCollisionExit2D와 동일
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!startgame)
        {
            foreach (SpriteRenderer objec in tiles)
            {
                objec.color = new Color(255, 255, 255);
            }
            deadlock = false;
            isInventory = false;
        }
    }

    //마우스를 클릭한 순간
    private void OnMouseDown()
    {
        if(!startgame)
        {
            //그 순간의 타일 좌표 저장
            //다른 타일과 겹친 상태(교착상태)에 마우스를 놓을 경우 이 좌표로 이동
            pastposition = this.gameObject.transform.position;
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
            
    }

    //마우스를 드래그해서 타일을 옮기는 함수
    void OnMouseDrag()
    {
        if(!startgame)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 objPosition = new Vector2(Mathf.Floor(temp.x) + 0.5f, Mathf.Floor(temp.y) + 0.5f);
            transform.position = objPosition;
        }
    }

    //마우스를 드래그한 상태에서 놓은 순간
    private void OnMouseUp()
    {
        //다른 타일과 겹친 상태(교착상태)에 마우스를 놓을 경우 이 좌표로 이동
        if (deadlock)
            transform.position = pastposition;
        if (isInventory)
        {
            //Debug.Log(this.gameObject);
            IInventoryItem item = this.gameObject.GetComponent<IInventoryItem>();
            if(item != null)
            {
                inventory.AddItem(item);
                //Debug.Log("ok");
            }

        }

        rigid.bodyType = RigidbodyType2D.Static;

    }

    //Start버튼을 눌렀을 경우
    public void StartMove()
    {
        //rigidbody타입 변경(Dynamic상태를 방치하면 캐릭터가 밟을 때 미세하게 움직임
        rigid.bodyType = RigidbodyType2D.Static;
        startgame = true;

        //안내판일 경우 colide속성을 trigger로 변경, 반투명화
        if (this.tag.Contains("Jumper"))
        {
            colid.isTrigger = true;
            thissprite.color  = new Color(255, 255, 255, 0.5f);
        }
            
            
    }

    //Stop버튼을 눌렀을 경우
    public void StopMove()
    {
        startgame = false;
        if (this.tag.Contains("Jumper"))
        {
            colid.isTrigger = false;
            thissprite.color = new Color(255, 255, 255, 1);
        }
    }

    //Reset버튼을 눌렀을 경우
    public void ResetGame()
    {
        startgame = false;

        //타일들의 위치를 게임 시작 직후의 위치로 변경
        //후에 UI로 변경하여 타일들을 UI로 옮기는 작업을 실시해야함
        transform.position = originposition;

        if (this.tag.Contains("Jumper"))
        {
            colid.isTrigger = false;
            thissprite.color = new Color(255, 255, 255, 1);
        }
    }

    
}
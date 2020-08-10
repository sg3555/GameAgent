using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drager : MonoBehaviour
{
    /*
     * Rigidbody Dynamic : 움직일 수 있는 형태
     * Rigidbody Static : 움직일 수 없는 형태
     * 두 Collider의 충돌여부 판정은 반드시 둘중 하나는 Dynamic상태여야 함
     */

    //각각 최초위치, 마우스를 놓기전 위치
    Vector2 originposition, pastposition;

    //간판전용 스프라이트구동기
    //SpriteRenderer sprite;

    //플랫폼전용 스프라이트구동기
    SpriteRenderer[] tiles; 

    Rigidbody2D rigid; //물리엔진
    Collider2D colid; //충돌자
    bool deadlock, //겹침상태 확인
         startGame, //게임시작 확인
        enterinven, //인벤토리상태 확인
        isclick;    //클릭상태 확인

    GameObject mainCam, MovableItem;


    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        tiles = gameObject.GetComponentsInChildren<SpriteRenderer>();
        colid = GetComponent<Collider2D>();
        //sprite = GetComponent<SpriteRenderer>();
        mainCam = GameObject.Find("Inventory");
        MovableItem = GameObject.Find("MovableItem");
    }
    void Start()
    {
        originposition = this.gameObject.transform.position;
        deadlock = false;
        startGame = false;
        isclick = false;

        /*
        //인벤토리 객체의 자식으로 들어가기(화면이 움직일 때 같이 움직이기 위함)
        transform.SetParent(mainCam.transform);
        //객체크기 줄이기
        transform.localScale = new Vector2(0.5f, 0.5f);
        //인벤토리 안에 있는 상태
        enterinven = true;
        */

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //인벤토리 처리
        if (collision.tag == "Inventory" && !isclick)
        {
            //인벤토리 객체의 자식으로 들어가기(화면이 움직일 때 같이 움직이기 위함)
            transform.SetParent(mainCam.transform);
            //객체크기 줄이기
            transform.localScale = new Vector2(0.5f, 0.5f);
            //인벤토리 안에 있는 상태
            enterinven = true;

        }
        else
        {
            //인벤토리 객체에서 MovableItem객체의 자식으로 돌아감
            transform.SetParent(MovableItem.transform);
            //객체크기 원상태
            transform.localScale = new Vector2(1f, 1f);
            //인벤토리 밖에 있는 상태
            enterinven = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //인벤토리 바깥에서 충돌 감지
        if (!startGame)
        {
            if (collision.gameObject != null && !enterinven)
            {
                //자식객체의 각 타일들을 붉은 색으로 바꾸고
                foreach (SpriteRenderer objec in tiles)
                    objec.color = new Color(1, 0, 0);
                //교착상태 bool을 true로 변경
                deadlock = true;
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!startGame)
        {
            //원상복귀
            foreach (SpriteRenderer objec in tiles)
                objec.color = new Color(255, 255, 255);
            deadlock = false;
        }
    }
  
    //마우스를 클릭한 순간
    private void OnMouseDown()
    {
        pastposition = this.gameObject.transform.position;
        rigid.bodyType = RigidbodyType2D.Dynamic;
        isclick = true;
    }

    //마우스를 드래그해서 타일을 옮기는 함수
    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //마우스 절대 좌표 입력
        Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition); //마우스 절대 좌표를 화면상 상대 좌표로 수정
        Vector2 objPosition = new Vector2(temp.x, temp.y); //
        transform.position = objPosition;
    }

    //마우스를 드래그한 상태에서 놓은 순간
    private void OnMouseUp()
    {
        //다른 타일과 겹친 상태(교착상태)에 마우스를 놓을 경우 이 좌표로 이동
        if (deadlock)
            transform.position = pastposition;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        isclick = false;
    }

}

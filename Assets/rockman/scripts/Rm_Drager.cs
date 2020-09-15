using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rm_Drager : MonoBehaviour
{
    /*
    * Rigidbody Dynamic : 움직일 수 있는 형태
    * Rigidbody Static : 움직일 수 없는 형태
    * 두 Collider의 충돌여부 판정은 반드시 둘중 하나는 Dynamic상태여야 함
    */

    //각각 최초위치, 마우스를 놓기전 위치, 최초 사이즈, 인벤사이즈
    Vector2 originPosition, //최초위치
        pastposition,   //마우스를 놓기전 위치
        originSize, //최초 사이즈
        invenSize,  //인벤토리에 들어갔을 시 사이즈
        mousePosition; //마우스 현재위치

    //스프라이트구동기
    SpriteRenderer[] tiles;

    Rigidbody2D rigid; //물리엔진
    Collider2D colid; //충돌자
    public bool deadlock, //겹침상태 확인
         startGame, //게임시작 확인
        enterinven, //인벤토리상태 확인
        paststate; //마우스 드래그 전 인벤에 있는지 상태


    public float minimunsize = 0.5f; //인벤토리에 들어갔을 때 사이즈(디폴트값 : 0.5)

    GameObject Inventory, MovableItem;//아이템이 들어가는 위치

    Ray mouseRay; // 마우스 인벤 충돌 감지용
    RaycastHit2D invenhit;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        tiles = gameObject.GetComponentsInChildren<SpriteRenderer>();
        colid = GetComponent<Collider2D>();
        Inventory = GameObject.Find("Inventory");
        MovableItem = GameObject.Find("MovableItem");
    }
    void Start()
    {
        originPosition = this.gameObject.transform.position;
        originSize = this.gameObject.transform.localScale;
        invenSize = new Vector2(minimunsize, minimunsize);
        deadlock = false;
        startGame = false;
        paststate = true;
        InvenSetting(true);
        LayerSetting("In");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //인벤토리 바깥에서 충돌 감지
        if (!startGame)
            deadlock = true;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!startGame)
            deadlock = false;
    }

    //마우스를 클릭한 순간
    private void OnMouseDown()
    {
        if (!startGame)
        {
            pastposition = this.gameObject.transform.position;
            rigid.bodyType = RigidbodyType2D.Dynamic;
            LayerSetting("In");
            paststate = enterinven;
        }

    }

    //마우스를 드래그해서 타일을 옮기는 함수
    void OnMouseDrag()
    {
        if (!startGame)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //마우스 절대 좌표 입력
            Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition); //마우스 절대 좌표를 화면상 상대 좌표로 수정
            Vector2 objPosition = new Vector2(temp.x, temp.y); //
            transform.position = objPosition;
            DeadlockSetting(deadlock);

            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            invenhit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, LayerMask.GetMask("UI"));
            if (invenhit.collider)
                InvenSetting(true);
            else
                InvenSetting(false);
        }

    }

    //마우스를 드래그한 상태에서 놓은 순간
    private void OnMouseUp()
    {
        if (!startGame)
        {
            //다른 타일과 겹친 상태(교착상태)에 마우스를 놓을 경우 이 좌표로 이동
            if (deadlock)
            {
                transform.position = pastposition;
                deadlock = false;
                DeadlockSetting(deadlock);
                if (paststate)
                {
                    InvenSetting(true);
                    LayerSetting("In");
                }
            }
            else
            {
                if (!enterinven)
                    LayerSetting("Out");
            }

            rigid.bodyType = RigidbodyType2D.Kinematic;

        }

    }

    //인벤 세팅 함수
    private void InvenSetting(bool etinven)
    {
        if (etinven)
        {
            //인벤토리 객체의 자식으로 들어가기(화면이 움직일 때 같이 움직이기 위함)
            transform.SetParent(Inventory.transform);
            //객체크기 줄이기
            transform.localScale = invenSize;
            //인벤여부 bool 확인
            enterinven = true;
        }
        else
        {
            //인벤토리 객체에서 MovableItem객체의 자식으로 돌아감
            transform.SetParent(MovableItem.transform);
            //객체크기 원상태
            transform.localScale = originSize;
            //인벤여부 bool 확인
            enterinven = false;
        }
    }

    /*
     * 레이어 세팅 함수
     * In : 인벤토리 안(인벤토리보다 위에 보이게)
     * Out : 인벤토리 밖(인벤토리에 가려저 안보이게)
    */

    private void LayerSetting(string where)
    {
        if (where == "In")
        {
            foreach (SpriteRenderer objec in tiles)
                objec.sortingLayerName = "Inven 4";
            this.gameObject.layer = 12;
        }
        else if (where == "Out")
        {
            foreach (SpriteRenderer objec in tiles)
                objec.sortingLayerName = "Item out Inventory 3";
            this.gameObject.layer = 0;
        }
    }

    private void DeadlockSetting(bool deadlock)
    {
        if (deadlock)
        {
            //자식객체의 각 타일들을 붉은 색으로 바꾸기
            foreach (SpriteRenderer objec in tiles)
                objec.color = new Color(1, 0, 0);
        }
        else
        {
            //원상복귀
            foreach (SpriteRenderer objec in tiles)
                objec.color = new Color(255, 255, 255);
        }
    }

    public void StartGame()
    {
        startGame = true;
        //rigid.bodyType = RigidbodyType2D.Static;
        if (tag == "Sign")
        {
           // foreach (SpriteRenderer objec in tiles)
                //objec.color = new Color(255, 255, 255, 0.3f);
            colid.isTrigger = true;
        }
    }

    public void StopGame()
    {
        startGame = false;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        colid.isTrigger = false;
        foreach (SpriteRenderer objec in tiles)
            objec.color = new Color(255, 255, 255, 1f);
    }

    public void ResetGame()
    {
        startGame = false;
        transform.position = originPosition;
        InvenSetting(true);
        LayerSetting("In");
        rigid.bodyType = RigidbodyType2D.Kinematic;
        colid.isTrigger = false;
        foreach (SpriteRenderer objec in tiles)
            objec.color = new Color(255, 255, 255, 1f);
    }
}




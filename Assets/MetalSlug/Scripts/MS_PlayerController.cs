using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_PlayerController : MonoBehaviour
{
    Vector2 originPosition; //캐릭터 최초위치
    Rigidbody2D rigid; //물리엔진
    Animator anim;  //스프라이트 애니메이션 담당
    AudioSource audioSource; //소리제어자
    Collider2D col; //충돌제어자
    SpriteRenderer sprite; //스프라이트제어자

    public bool startGame;  //게임 시작상태 bool
    public bool clear; //클리어 여부 
    public float maxSpeed = 5f; // Player Speed
    public int jumpForce = 450; // Player jump force
    public int groundLayerNum = 21; // ground의 레이어 번호
    public int playerLayerNum = 22; // ground의 레이어 번호
    public int goalLayerNum = 24; 
    public int PBulletLayerNum = 25; 
    public GameObject knife;
    public bool isKnife = false;
    private Transform groundCheck;
    private bool onGround = false;
    private bool groundLineCheck = false;
    private bool groundColCheck = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startGame = false;
        clear = false;
        originPosition = this.gameObject.transform.position;
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    void FixedUpdate()
    {
        //게임 시작상태에서만 움직임 활성화
        if (startGame)
        {
            useKnife();
            if (!clear)
            {
                groundLineCheck = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                if (groundLineCheck && groundColCheck)
                    onGround = true;
                else
                    onGround = false;

                rigid.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);
                //최대속력 설정
                if (rigid.velocity.x > maxSpeed && !clear)
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                else if (rigid.velocity.x < -maxSpeed && !clear)
                    rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
                anim.SetBool("IsRunning", true);
                //움직일때 달리는 모션
                //if (Mathf.Abs(rigid.velocity.x) > 0.3)
                //    anim.SetBool("IsRunning", true);
                //else
                //    anim.SetBool("IsRunning", false);

            }
            else
            {
                rigid.velocity = new Vector2(0, 0);
                anim.SetBool("IsRunning", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.layer != playerLayerNum && collision.gameObject.layer != PBulletLayerNum)
        //{ 

        //}

        //점프
        if (collision.tag.Contains("Test") && !anim.GetBool("IsJump") && startGame && onGround)
        {
            Debug.Log("Enter Test");
            anim.SetBool("IsJump", true);
            rigid.AddForce(Vector2.up * jumpForce);
        }

        //나이프
        //if (collision.tag.Contains("Test") && !anim.GetBool("IsKnife") && startGame)
        //{
        //    Debug.Log("Enter Test");
        //    isKnife = true;
        //}

        if (collision.gameObject.layer == goalLayerNum)
        {
            isKnife = true;
            clear = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains("Test") && startGame)
        {
            //Debug.Log("Stay Test");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer + "Enter");
        if (collision.gameObject.layer == groundLayerNum)
        {
            groundColCheck = true;
            if (anim.GetBool("IsJump") && groundLineCheck)
            {
                anim.SetBool("IsJump", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.layer + "Exit");
        if (collision.gameObject.layer == groundLayerNum)
        {
            groundColCheck = false;
        }
    }

    public void startMove()
    {
        Debug.Log("start");
        startGame = true;
    }

    public void stopGame()
    {
        Debug.Log("stop");
    }

    public void resetGame()
    {
        Debug.Log("reset");
    }

    //GameManager나 다른 오브젝트에서 플레이어의 상태를 조회
    public string getState()
    {
        if (clear)
        {
            return "clear";
        }
        else
        {
            return "null";
        }
    }

    private void useKnife()
    {
        //근접공격
        if (isKnife)
        {
            anim.SetBool("IsKnife", true);
            isKnife = false;
        }
        else
            anim.SetBool("IsKnife", false);

        //근접공격 중 칼날에 타격판정 부여
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Eri_Knife"))
        {
            knife.layer = PBulletLayerNum;
            //Debug.Log(knife.layer);
        }
        else
        {
            knife.layer = playerLayerNum;
        }
    }
}

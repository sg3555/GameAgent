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
    //public int jumpForce = 450; // Player jump force
    public int height; // Player jump force
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
    public bool GM_isdead, GM_goal, GM_clear; //게임매니저 수신용

    public AudioClip audioKnife_rope;

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
        height = 10;
    }

    void FixedUpdate()
    {
        eri_move();
        eri_speed();
        eri_inverse();
        multiGroundCheck();
        useKnifeLayer();
        //eri_clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //점프
        //if (collision.tag.Contains("Test") && !anim.GetBool("IsJump") && startGame && onGround)
        //{
        //    Debug.Log("Enter Test");
        //    anim.SetBool("IsJump", true);
        //    rigid.AddForce(Vector2.up * jumpForce);
        //}


        if (collision.tag == "Sign")
        {
            if (collision.name.Contains("Sign_Up"))
            {
                if (!anim.GetBool("IsJump") && startGame)
                {
                    rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
                    anim.SetBool("IsJump", true);
                    //PlaySound(audioJump);
                }
            }
            if (collision.name.Contains("Sign_Down"))
            {

            }
            if (collision.name.Contains("Sign_Left"))
            {

            }
            if (collision.name.Contains("Sign_Right"))
            {

            }
            if (collision.name.Contains("Sign_A"))
            {
                if (!anim.GetBool("IsJump") && startGame)
                {
                    rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
                    anim.SetBool("IsJump", true);
                    //PlaySound(audioJump);
                }
            }
            if (collision.name.Contains("Sign_B"))
            {

            }
            if (collision.name.Contains("Sign_X"))
            {

            }
            if (collision.name.Contains("Sign_Y"))
            {

            }
        }

        if (collision.gameObject.layer == goalLayerNum)
        {
            useKnife();
            clear = true;
            Invoke("eri_clear", 1.2f);
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

    void eri_move()
    {
        //게임 시작상태에서만 움직임 활성화
        if (startGame)
            rigid.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);
    }
    void eri_speed()
    {
        //최대속력 설정
        if (rigid.velocity.x > maxSpeed && !clear)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed && !clear)
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        //클리어 시 정지
        else if (clear)
            rigid.velocity = new Vector2(0, 0);
    }
    void eri_inverse()
    {
        if (Mathf.Abs(rigid.velocity.x) > 0.3)
        {
            anim.SetBool("IsRunning", true);
        }
        else
            anim.SetBool("IsRunning", false);
    }
    void eri_clear()
    {
        anim.SetBool("IsClear", true);
        GM_clear = true;
    }
    void multiGroundCheck()
    {
        groundLineCheck = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (groundLineCheck && groundColCheck)
            onGround = true;
        else
            onGround = false;
    }

    public void startMove()
    {
        Debug.Log("start");
        rigid.bodyType = RigidbodyType2D.Dynamic;
        startGame = true;
    }

    public void resetGame()
    {
        Debug.Log("reset");
        rigid.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsClear", false);
        gameObject.transform.position = originPosition;
        startGame = false;
    }

    //GameManager나 다른 오브젝트에서 플레이어의 상태를 조회
    public string getState()
    {
        if (GM_clear)
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
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Eri_Knife"))
        {
            anim.SetTrigger("useKnife");
            PlaySound(audioKnife_rope);
        }
    }
    private void useKnifeLayer()
    {
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
    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }
}

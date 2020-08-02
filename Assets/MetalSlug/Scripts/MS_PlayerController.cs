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
    public float maxSpeed = 5f;    // Player Speed
    public int jumpForce = 450;   // Player jump force

    private bool isKnife = false;

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
    }

    void FixedUpdate()
    {
        //게임 시작상태에서만 움직임 활성화
        if (startGame)
        {
            rigid.AddForce(Vector2.right * 1f, ForceMode2D.Impulse);
            
            //최대속력 설정
            if (rigid.velocity.x > maxSpeed && !clear)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < -maxSpeed && !clear)
                rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);

            //움직일때 달리는 모션
            if (Mathf.Abs(rigid.velocity.x) > 0.3)
                anim.SetBool("IsRunning", true);
            else
                anim.SetBool("IsRunning", false);

            //근접공격
            if (isKnife)
            {
                anim.SetBool("IsKnife", true);
                isKnife = false;
            }
            else
                anim.SetBool("IsKnife", false);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //점프
        //if (collision.tag.Contains("Test") && !anim.GetBool("IsJump") && startGame)
        //{
        //    Debug.Log("Enter Test");
        //    anim.SetBool("IsJump", true);
        //    rigid.AddForce(Vector2.up * jumpForce);
        //}

        //나이프
        if (collision.tag.Contains("Test") && !anim.GetBool("IsKnife") && startGame)
        {
            Debug.Log("Enter Test");
            isKnife = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Contains("Test") && startGame)
        {
            //Debug.Log("Stay Test");
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
}

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
    public float jumpForce = 600;   // Player jump force

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

            if (Mathf.Abs(rigid.velocity.x) > 0.3)
            {
                anim.SetBool("isRunning", true);
            }
            else
                anim.SetBool("isRunning", false);
        }
    }

    public void startMove()
    {
        Debug.Log("start");
        startGame = true;
    }

    public void resetGame()
    {
        Debug.Log("reset");
    }
}

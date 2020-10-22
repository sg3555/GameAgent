using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario : MonoBehaviour
{
    Vector2 originPosition; //캐릭터 최초위치
    Rigidbody2D rigid; //물리엔진
    Animator anim;  //스프라이트 애니메이션 담당
    AudioSource audioSource; //소리제어자
    Collider2D colid; //충돌제어자
    SpriteRenderer sprit; //스프라이트제어자
    GameObject flag;

    bool startGame; //게임 시작상태 bool
    float height; //점프 높이
    public float maxSpeed; //최대 속도
    float animSpeed; //애니메이션 속도
    bool clear;  //클리어 여부 
    float direc; //캐릭터 이동 방향


    public bool GM_isdead, GM_goal, GM_clear; //게임매니저 수신용
    //public GameManager gm; //게임매니저 함수를 쓰기위한 객체

    public AudioClip audioJump; //점프 효과음
    public AudioClip audioAttack; //공격 효과음
    public float speed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        colid = GetComponent<Collider2D>();
        sprit = GetComponent<SpriteRenderer>();
        flag = GameObject.Find("Goal");
    }

    private void Start()
    {
        direc = 0.2f;
        startGame = false;
        clear = false;
        GM_isdead = false;
        GM_clear = false;
        GM_goal = false;
        height = 18;
        maxSpeed = 5;
        originPosition = this.gameObject.transform.position;
    }

    //움직임
    void mario_move()
    {
        //게임 시작상태에서만 움직임 활성화
        if (startGame)
            rigid.AddForce(Vector2.right * direc, ForceMode2D.Impulse);
    }

    //속력설정
    void mario_speed()
    {
        //최대속력 설정
        if (rigid.velocity.x > maxSpeed && !clear)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed && !clear)
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        //클리어 시 걸음속도 조절
        else if (rigid.velocity.x > maxSpeed && clear)
            rigid.velocity = new Vector2(maxSpeed / 2, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed && clear)
            rigid.velocity = new Vector2(-maxSpeed / 2, rigid.velocity.y);
    }

    //떨어지는 마리오
    void mario_fall()
    {
        //점프후 바닥에 착지시 점프애니메이션 종료
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
                if (rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
        }
    }

    //반대방향구현
    void mario_inverse()
    {
        //이동값(Vector변화량)에 따른 애니메이션 제어
        animSpeed = Mathf.Abs(rigid.velocity.x) / 10 + 0.5f;
        if (Mathf.Abs(rigid.velocity.x) > 0.6)
        {
            anim.speed = animSpeed;
            anim.SetBool("IsMove", true);
        }
        else
            anim.SetBool("IsMove", false);

        //반대방향으로 이동시 스프라이트 반전
        if (rigid.velocity.x < 0)
            sprit.flipX = true;
        else if (rigid.velocity.x > 0)
            sprit.flipX = false;

        
        
    }

    void FixedUpdate()
    {
        mario_move();
        mario_speed();
        mario_fall();
        mario_inverse();
        slipflag();
    }

    //게임 시작
    public void StartGame()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        startGame = true;
        direc = 0.2f;
    }

    //게임 리셋
    public void ResetGame()
    {
        gameObject.layer = 9;
        rigid.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsMove", false);
        anim.SetBool("IsJump", false);
        anim.SetBool("IsDead", false);
        colid.isTrigger = false;
        gameObject.transform.position = originPosition;
        sprit.flipX = false;
        startGame = false;
    }

    

    //효과음 재생용
    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }

    //사망모션1
    void bound()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
    }

    //클리어모션1
    void flip()
    {
        sprit.flipX = true;
        transform.Translate(new Vector3(0.9f, 0, 0));
        Invoke("gotodoor", 0.5f);
    }

    //클리어모션2
    void gotodoor()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("IsGoal", false);
        sprit.flipX = false;
        startGame = true;
    }

    //클리어시 국기봉에서 내려오는 모션
    void slipflag()
    {
        if (clear && transform.position.y >= flag.transform.position.y)
            transform.Translate(new Vector3(0, -0.15f, 0));
    }

    //사망모션
    void deadAction()
    {
        startGame = false;
        rigid.bodyType = RigidbodyType2D.Static;
        anim.SetBool("IsDead", true);
        GM_isdead = true;
        gameObject.layer = 10;
        colid.isTrigger = true;
        Invoke("bound", 0.5f);
    }

    //골인모션
    void goalAction()
    {
        GM_goal = true;
        startGame = false;
        clear = true;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        rigid.velocity = Vector2.zero;
        anim.SetBool("IsJump", false);
        anim.SetBool("IsGoal", true);
        Invoke("flip", 1.2f);
    }

    //공격
    void OnAttack(Transform enemy)
    {
        //Reaction Force
        rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
        //Enemy Die
        if (enemy.name.Contains("Goomba"))
        {
            Mario_Goomba enem = enemy.GetComponent<Mario_Goomba>();
            enem.OnDamaged();
        }
            
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Enemy") && gameObject.layer == 9)
        {
            if (rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
                PlaySound(audioAttack);
            }
            else
                deadAction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //사망
        if (collision.tag.Contains("Damage") && gameObject.layer == 9)
        {
            deadAction();
        }

        //골인
        if (collision.tag.Contains("Finish"))
        {
            goalAction();
        }

        //문안으로 들어가면 클리어 화면 출력
        if (collision.tag.Contains("Disappear"))
        {
            gameObject.SetActive(false);
            GM_clear = true;
        }


        if (collision.tag == "Sign")
        {
            if (collision.name.Contains("Sign_Up"))
            {

            }
            if (collision.name.Contains("Sign_Down"))
            {

            }
            if (collision.name.Contains("Sign_Left"))
            {
                direc = -0.5f;
            }
            if (collision.name.Contains("Sign_Right"))
            {
                direc = 0.5f;
            }
            if (collision.name.Contains("Sign_A"))
            {

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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {
            if (collision.name.Contains("Sign_Up"))
            {
                if (!anim.GetBool("IsJump") && startGame)
                {
                    rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
                    anim.SetBool("IsJump", true);
                    PlaySound(audioJump);
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
                    PlaySound(audioJump);
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
    }
}

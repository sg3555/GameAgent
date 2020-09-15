using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//버튼을 누르면 캐릭터가 움직이는 Actor객체의 스크립트
public class rm_move : MonoBehaviour
{
    Vector2 originPosition;
    public float maxspeed;
    Rigidbody2D rigid;
    public bool start;
    public int JumpPower;
    AudioSource audiosource;
    public AudioClip audiojump;
    public AudioClip audioClear;
    public AudioClip audioWarp;
    Animator anim;
    public static rm_move rm;
    public bool GM_isdead;
    SpriteRenderer sprite;
    

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        anim.SetBool("isstart", true);
        audiosource = GetComponent<AudioSource>();
        PlaySound(audioWarp);
        originPosition = this.gameObject.transform.position;
        rigid.bodyType = RigidbodyType2D.Static;
        GM_isdead = false;
        

    }
    public void rockman_move()
    {
        if (GameManager_RM.gm.start)
        {

            rigid.bodyType = RigidbodyType2D.Dynamic;
            transform.Translate(Vector2.right * 0.05f);
            anim.SetBool("isrun", true);
        }
        else
        {
            anim.SetBool("isrun", false);
        }
    



    }

    public void ResetGame()
    {
        
        gameObject.SetActive(true);
        anim.SetBool("isstart", true);
        gameObject.transform.position = originPosition;
        rigid.bodyType = RigidbodyType2D.Static;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamaged(collision.transform.position);
        }
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Clear")
        {
            GameManager_RM.gm.start = false;
            transform.Translate(Vector2.zero);
            anim.SetBool("isclear",true);
            // PlaySound(audioClear);
            GameManager_RM.gm.clearAction();
            Invoke("Deact", 1f);
            
        }
    }
    void onDamaged(Vector2 targetPos)
    {
        gameObject.layer = 12;

        //sprite.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 2 : -2;
        rigid.AddForce(new Vector2(dirc, 1)*3, ForceMode2D.Impulse);

        //애니메이션
        anim.SetTrigger("damaged");
        GM_isdead = true;
        Invoke("Deact", 0.5f);

    }
    void Deact()
    {
        gameObject.SetActive(false);
    }
    public void rockman_jump()
    {
        if (anim.GetBool("isrun")==true)
        {
            anim.SetBool("isjump", true);
        }
        rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        PlaySound(audiojump);
       

    }
    private void FixedUpdate()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("start")) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.18f))
        {
            anim.SetBool("isstart", false);
        }
        rockman_move();
        //rockman_jump();
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 5,LayerMask.GetMask("ground"));
            if (rayHit.collider != null)
            {
                //Debug.Log(rayHit.collider.tag);
                if (rayHit.distance <= 1f)
                {

                    //anim.SetBool("isrun", true);
                    anim.SetBool("isjump", false);
                    Debug.Log("점프");
                }

            }
        }
    }
    public void Start_move()
    {
        start = true;

    }

    public void PlaySound(AudioClip action)
    {
        audiosource.clip = action;
        audiosource.Play();
    }
}

   // Vector2 originPosition; //캐릭터 최초위치
   // Rigidbody2D rigid; //물리엔진
   // Animator anim;  //스프라이트 애니메이션 담당
   // AudioSource audioSource; //소리제어자
   // Collider2D colid; //충돌제어자
   // SpriteRenderer sprit; //스프라이트제어자

   // public bool startGame; //게임 시작상태 bool
   // public float height; //점프 높이
   // public float maxSpeed; //최대 속도
   // public float animSpeed; //애니메이션 속도
   // public bool clear; //클리어 여부 
   // //public GameManager gm; //게임매니저 함수를 쓰기위한 객체

   // public AudioClip audioJump; //점프 효과음

   // void Awake()
   // {
   //     rigid = GetComponent<Rigidbody2D>();
   //     anim = GetComponent<Animator>();
   //     audioSource = GetComponent<AudioSource>();
   //     colid = GetComponent<Collider2D>();
   //     sprit = GetComponent<SpriteRenderer>();
   // }

   // private void Start()
   // {
   //     startGame = false;
   //     //clear = false;
   //     height = 18;
   //     originPosition = this.gameObject.transform.position;
   // }

   // //움직임
   //public void  rockman_move()
   // {
   //     //게임 시작상태에서만 움직임 활성화
   //     if (startGame)
   //         rigid.AddForce(Vector2.right * 0.2f, ForceMode2D.Impulse);
   // }

   // //속력설정
   // void rockman_speed()
   // {
   //     //최대속력 설정
   //     if (rigid.velocity.x > maxSpeed && !clear)
   //         rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
   //     else if (rigid.velocity.x < -maxSpeed && !clear)
   //         rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
   //     //클리어 시 걸음속도 조절
   //     else if (rigid.velocity.x > maxSpeed && clear)
   //         rigid.velocity = new Vector2(maxSpeed / 2, rigid.velocity.y);
   //     else if (rigid.velocity.x < -maxSpeed && clear)
   //         rigid.velocity = new Vector2(-maxSpeed / 2, rigid.velocity.y);
   // }

   // //떨어지는 마리오
   // void rockman_fall()
   // {
   //     //점프후 바닥에 착지시 점프애니메이션 종료
   //     if (rigid.velocity.y < 0)
   //     {
   //         Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));
   //         RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
   //         if (rayHit.collider != null)
   //             if (rayHit.distance < 0.5f)
   //                 anim.SetBool("IsJump", false);
   //     }
   // }

   // //반대방향구현
   // void rockman_inverse()
   // {
   //     //이동값(Vector변화량)에 따른 애니메이션 제어
   //     animSpeed = Mathf.Abs(rigid.velocity.x) / 10 + 0.5f;
   //     if (Mathf.Abs(rigid.velocity.x) > 0.3)
   //     {
   //         anim.speed = animSpeed;
   //         anim.SetBool("isrun", true);
   //     }
   //     else
   //         anim.SetBool("isrun", false);
   // }

   // void FixedUpdate()
   // {

   //     rockman_move();
   //     rockman_speed();
   //     rockman_fall();
   //     rockman_inverse();




   // }

   // //게임 시작
   // public void StartMove()
   // {
   //     rigid.bodyType = RigidbodyType2D.Dynamic;
   //     startGame = true;
   //     if (startGame)
   //         rigid.AddForce(Vector2.right * 0.1f, ForceMode2D.Impulse);
   // }

   // //게임 리셋
   // public void ResetGame()
   // {
   //     gameObject.layer = 0;
   //     rigid.bodyType = RigidbodyType2D.Static;
   //     anim.SetBool("IsMove", false);
   //     anim.SetBool("IsJump", false);
   //     anim.SetBool("IsDead", false);
   //     colid.isTrigger = false;
   //     gameObject.transform.position = originPosition;
   //     startGame = false;
   // }


   // private void OnTriggerEnter2D(Collider2D collision)
   // {
   //     //사망
   //     if (collision.tag.Contains("Damage") && gameObject.layer == 0)
   //     {
   //         startGame = false;
   //         rigid.bodyType = RigidbodyType2D.Static;
   //         anim.SetBool("IsDead", true);
   //         //gm.deadAction();
   //         gameObject.layer = 10;
   //         colid.isTrigger = true;
   //         Invoke("bound", 0.5f);
   //     }


   //     //골인
   //     if (collision.tag.Contains("Finish"))
   //     {
   //         //gm.goalAction();
   //         startGame = false;
   //         clear = true;
   //         rigid.bodyType = RigidbodyType2D.Kinematic;
   //         rigid.velocity = Vector2.zero;
   //         anim.SetBool("IsJump", false);
   //         anim.SetBool("IsGoal", true);
   //         Invoke("flip", 1.2f);
   //     }

   //     //문안으로 들어가면 클리어 화면 출력
   //     if (collision.tag.Contains("Disappear"))
   //     {
   //         gameObject.SetActive(false);
   //         //gm.clearScreen();
   //     }

   // }

   // private void OnTriggerStay2D(Collider2D collision)
   // {
   //     //점프
   //     if (collision.tag.Contains("Jumper") && !anim.GetBool("IsJump") && startGame)
   //     {
   //         rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
   //         anim.SetBool("IsJump", true);
   //         PlaySound(audioJump);
   //     }
   // }

   // //효과음 재생용
   // void PlaySound(AudioClip action)
   // {
   //     audioSource.clip = action;
   //     audioSource.Play();
   // }

   // //사망모션1
   // void bound()
   // {
   //     rigid.bodyType = RigidbodyType2D.Dynamic;
   //     rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
   // }

   // //클리어모션1
   // void flip()
   // {
   //     sprit.flipX = true;
   //     transform.Translate(new Vector3(0.9f, 0, 0));
   //     Invoke("gotodoor", 0.5f);
   // }

   // //클리어모션2
   // void gotodoor()
   // {
   //     rigid.bodyType = RigidbodyType2D.Dynamic;
   //     anim.SetBool("IsGoal", false);
   //     sprit.flipX = false;
   //     startGame = true;
   // }



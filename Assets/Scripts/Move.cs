using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//버튼을 누르면 캐릭터가 움직이는 Actor객체의 스크립트
public class Move : MonoBehaviour
{
    Vector2 originPosition;
    Rigidbody2D rigid;
    Animator anim;  //스프라이트 애니메이션 담당
    AudioSource audioSource;

    public bool startGame; //게임 시작상태 bool
    public float height;
    public float maxSpeed;
    public float animSpeed;

    public BGM bgm;
    public GameManager gm;

    public AudioClip audioJump;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startGame = false;
        height = 18;
        originPosition = this.gameObject.transform.position;
        
    }

    void FixedUpdate()
    {
        //게임 시작상태에서만 움직임 활성화
        if(startGame)
            rigid.AddForce(Vector2.right * 0.1f, ForceMode2D.Impulse);

        //최대속력 설정
        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < -maxSpeed)
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);

        //점프후 바닥에 착지시 점프애니메이션 종료
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector2.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
                if (rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
                    
        }

        //이동값(Vector변화량)에 따른 애니메이션 제어
        animSpeed = Mathf.Abs(rigid.velocity.x) / 10 + 0.5f;
        if (Mathf.Abs(rigid.velocity.x) > 0.3)
        {
            anim.speed = animSpeed;
            anim.SetBool("IsMove", true);
        }
        else
            anim.SetBool("IsMove", false);

    }

    //게임 시작
    public void StartMove()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        startGame = true;
    }

    //게임 리셋
    public void ResetGame()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        gameObject.transform.position = originPosition;
        startGame = false;
        anim.SetBool("IsMove", false);
        anim.SetBool("IsJump", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains("DeadZone"))
        {
            gm.stopGame();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //점프
        if(collision.tag.Contains("Jumper") && !anim.GetBool("IsJump") && startGame)
        {
            rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
            PlaySound(audioJump);
        }
    }

    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//버튼을 누르면 캐릭터가 움직이는 Actor객체의 스크립트
public class Move : MonoBehaviour
{
    Vector2 originPosition;
    Rigidbody2D rigid;
    Animator anim;  //스프라이트 애니메이션 담당
    public bool startGame; //게임 시작상태 bool
    public float height;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startGame = false;
        height = 18;

        //게임시작전에 Actor객체를 정적상태로 변경(움직임 방지)
        rigid.bodyType = RigidbodyType2D.Static;

        originPosition = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        //게임 시작상태에서만 움직임 활성화
        if (startGame == true)
            rigid.velocity = new Vector2(5, rigid.velocity.y);

        //점프후 바닥에 착지시 점프애니메이션 종료
        if(rigid.velocity.y<0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, LayerMask.GetMask("platform"));
            if (rayHit.collider != null)
                if(rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
        }
        
    }

    //게임 시작
    public void StartMove()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        startGame = true;
        anim.SetBool("IsMove", true);
    }

    //게임 리셋
    public void ResetGame()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        gameObject.transform.position = originPosition;
        startGame = false;
        anim.SetBool("IsMove", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //점프
        if(collision.tag.Contains("Jumper") && !anim.GetBool("IsJump") && startGame)
        {
            rigid.AddForce(Vector2.up * height, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
        }
    }
    
}

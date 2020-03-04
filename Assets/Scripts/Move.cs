using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//버튼을 누르면 캐릭터가 움직이는 Actor객체의 스크립트
public class Move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;  //스프라이트 애니메이션 담당
    public bool startGame; //게임 시작상태 bool

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startGame = false;

        //게임시작전에 Actor객체를 정적상태로 변경(움직임 방지)
        rigid.bodyType = RigidbodyType2D.Static;
    }

    void FixedUpdate()
    {
        //게임 시작상태에서만 움직임 활성화
        if(startGame == true)
            rigid.velocity = new Vector2(3, rigid.velocity.y);
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
        gameObject.transform.position = new Vector2(-13, 0.5f);
        startGame = false;
        anim.SetBool("IsMove", false);
    }
    
}

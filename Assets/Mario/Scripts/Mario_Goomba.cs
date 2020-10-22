using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario_Goomba : MonoBehaviour
{
    Vector2 originPosition; //캐릭터 최초위치
    Rigidbody2D rigid; //물리엔진
    Animator anim;  //스프라이트 애니메이션 담당
    Collider2D colid; //충돌제어자
    SpriteRenderer sprit; //스프라이트제어자

    float maxSpeed; //최대 속도
    bool startGame; //게임 시작상태 bool
    float animSpeed; //애니메이션 속도
    public float origindirec = 1;
    float direc;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colid = GetComponent<Collider2D>();
        sprit = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        colid.enabled = false;
        startGame = false;
        animSpeed = 0;
        maxSpeed = 2;
        direc = origindirec;
        rigid.bodyType = RigidbodyType2D.Static;

        anim.speed = animSpeed;
        originPosition = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if(startGame)
            rigid.velocity = new Vector2(maxSpeed * direc, rigid.velocity.y);

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, new Vector3(direc, 0, 0), 0.6f, LayerMask.GetMask("Platform"));
        if(rayHit.collider != null)
        {
            direc *= -1;
        }
    }

    //사망1
    public void OnDamaged()
    {
        //ColliderDisable
        StopGame();
        colid.enabled = false;
        anim.SetTrigger("IsDamaged");
        //Destroy
        Invoke("DeActive", 0.5f);
    }

    //사망2
    void DeActive()
    {
        anim.SetTrigger("IsReset");
        this.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        colid.enabled = true;
        startGame = true;
        anim.speed = 1;
    }

    //게임 리셋
    public void ResetGame()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        colid.enabled = false;
        startGame = false;
        gameObject.transform.position = originPosition;
        this.gameObject.SetActive(true);
        anim.SetTrigger("IsReset");
        anim.speed = 0;
        direc = origindirec;
    }

    public void StopGame()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        startGame = false;
        anim.speed = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains("Damage"))
        {
            this.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Arabian : MonoBehaviour
{
    Vector2 originPosition; //캐릭터 최초위치
    Rigidbody2D rigid; //물리엔진
    Animator anim;  //스프라이트 애니메이션 담당
    Collider2D colid; //충돌제어자
    private int playerLayerNum = 22;
    private int enemyLayerNum = 23;
    private int P_BulletLayerNum = 25;
    private int E_BulletLayerNum = 26;
    AudioSource audioSource; //소리제어자
    public AudioClip audioDie;

    bool startGame;
    bool isDead;
    public GameObject knife;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colid = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        colid.enabled = false;
        startGame = false;
        rigid.bodyType = RigidbodyType2D.Static;
        isDead = false;
        //anim.speed = animSpeed;
        originPosition = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        attack();
        useKnifeLayer();
    }

    void attack()
    {
        if(startGame && !isDead)
        {
            Debug.DrawRay(this.gameObject.transform.position, Vector2.left * 4f, new Color(0, 255, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.left, 4f, LayerMask.GetMask("Player"));
            if (rayHit.collider != null)
            {
                anim.SetBool("IsAttack", true);
            }
            else
            {
                anim.SetBool("IsAttack", false);
            }
        }
    }

    void useKnifeLayer()
    {
        if (!isDead)
        {
            //근접공격 중 칼날에 타격판정 부여
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Arabian_Attack"))
            {
                knife.layer = E_BulletLayerNum;
                //Debug.Log(knife.layer);
            }
            else
            {
                knife.layer = enemyLayerNum;
            }
        }
    }

    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }

    public void die()
    {
        isDead = true;
        Debug.Log("arabian die");
        anim.SetTrigger("Die");
        PlaySound(audioDie);
        Invoke("destroyObj", 0.4f);
    }
    void destroyObj()
    {
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, P_BulletLayerNum, false);
        rigid.bodyType = RigidbodyType2D.Dynamic;
        colid.enabled = true;
        startGame = true;
        anim.speed = 1;
    }

    //게임 리셋
    public void ResetGame()
    {
        anim.SetBool("IsAttack", false);
        anim.SetTrigger("Reset");
        rigid.bodyType = RigidbodyType2D.Static;
        colid.enabled = false;
        startGame = false;
        isDead = false;
        gameObject.transform.position = originPosition;
        this.gameObject.SetActive(true);
        //anim.SetTrigger("IsReset");
        anim.speed = 1;
    }

    public void StopGame()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        startGame = false;
        anim.speed = 0;
    }

}

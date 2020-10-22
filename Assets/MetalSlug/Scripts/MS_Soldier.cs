using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Soldier : MonoBehaviour
{
    Vector2 originPosition; //캐릭터 최초위치
    Rigidbody2D rigid; //물리엔진
    Animator anim;  //스프라이트 애니메이션 담당
    Collider2D colid; //충돌제어자

    private int enemyLayerNum = 23;
    private int P_BulletLayerNum = 25;
    private int E_BulletLayerNum = 26;
    AudioSource audioSource; //소리제어자
    public AudioClip audioDie;
    private float fireRate = 1.3f;
    private float nextFire = 0f;
    public Transform muzzle;
    public GameObject bulletPrefab;

    bool startGame;
    bool isDead;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colid = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
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
        fire();
    }

    void fire()
    {
        if (startGame && !isDead)
        {
            if (Time.time > nextFire)
            {
                anim.SetBool("IsFire", true);
                nextFire = Time.time + fireRate;
                Invoke("bullet", 0.1f);
            }
            else
            {
                anim.SetBool("IsFire", false);
            }
        }
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

    void bullet()
    {
        GameObject tempBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }
    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }


    public void die()
    {
        //this.gameObject.SetActive(false);
        colid.enabled = false;
        isDead = true;
        Debug.Log("soldier die");
        anim.SetTrigger("Die");
        PlaySound(audioDie);
        Invoke("destroyObj", 0.4f);
    }
    void destroyObj()
    {
        this.gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

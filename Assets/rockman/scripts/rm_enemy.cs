using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

public class rm_enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    BoxCollider2D collide;
    public GameManager_RM gm;
    Animator anim;
    public AudioClip des_sound;
    AudioSource audiosource;
    public bool damage = false;
    public float speed;
    bool isleft = true;
    public bool emove = false;
    Vector2 originPosition;

    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        originPosition= this.gameObject.transform.position;
       
        
    }
    private void Start()
    {
        rigid.bodyType = RigidbodyType2D.Static;
       
    }

    void Update()//적이 데미지를 받았을 경우 그 자리에서 멈추게 하고 그렇지 않으면 이동하게 하는 것
    {
  
        if (emove)
        {
            
            if (gameObject.tag == "trap")
            {
                rigid.bodyType = RigidbodyType2D.Dynamic;

            }
            else if (gameObject.tag == "Enemy")
            {
                rigid.bodyType = RigidbodyType2D.Dynamic;
                anim.gameObject.SetActive(true);
                if (damage == true)
                    transform.Translate(Vector2.zero);

                else
                {

                    transform.Translate(Vector2.left * 2 * Time.deltaTime);
                }
            }
        }
    
     


    }
    
    public void Active()
    {
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)//적의 이동 거리를 정하기 위해 collision의 tag 중 endpoint를 지나게 되면 방향을 바꾸게 함
    {
        if (collision.tag == "endpoint")
        {
            if (isleft)
            {
               
                transform.eulerAngles = new Vector3(0, 180, 0);
                isleft = false;
            }
            else
            {
               
                transform.eulerAngles = new Vector3(0, 0, 0);
                isleft = true;
            }
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
       {


            transform.Translate(Vector2.zero);
            
        }
    }
    //적이 데미지를 받았을 시 하는 행동
    public void OnDamaged()
    {
        Invoke("DeActive", 0.2f);
        damage = true;
        anim.SetTrigger("destroy");
        PlaySound(des_sound);
       


    }
    public void enemyStop()
    {
        rigid.bodyType = RigidbodyType2D.Static;
        gameObject.SetActive(true);
        collide.enabled = true;
        gameObject.transform.position = originPosition;
        transform.Translate(Vector2.zero);
        if (!isleft)
        {
           transform.eulerAngles= new Vector3(0, 0, 0);
            isleft = true;
        }
     
        damage = false;
        emove = false;
    }
    public void enemyMove()
    {
        emove = true;
  
    }
    void DeActive()//적이 사라지게 만드는 함수
    {
        gameObject.SetActive(false);
        collide.enabled = false;
    }
    public void PlaySound(AudioClip action)
    {
        audiosource.clip = action;
        audiosource.Play();
    }

}

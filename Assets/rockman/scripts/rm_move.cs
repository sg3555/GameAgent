﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    public rm_enemy re;
    SpriteRenderer sprite;
    public bool clear = false;
    public bool movestart = false;
    

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
        movestart = true;
        //if (GameManager_RM.gm.start)
        //{

        //    rigid.bodyType = RigidbodyType2D.Dynamic;
        //    transform.Translate(Vector2.right * 0.05f);
        //    anim.SetBool("isrun", true);
        //}
        //else
        //{
        //    anim.SetBool("isrun", false);
        //}
        



    }

    public void ResetGame()
    {
        
        gameObject.SetActive(true);
        anim.SetBool("isstart", true);
        rigid.gravityScale = 5;
        gameObject.transform.position = originPosition;
        rigid.bodyType = RigidbodyType2D.Static;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            onDamagedEnemy(collision.transform.position);
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Clear")
        {
            GameManager_RM.gm.start = false;
            transform.Translate(Vector2.zero);
            anim.SetBool("isclear",true);
            PlaySound(audioClear);
            Invoke("Deact", 4f);
            GameManager_RM.gm.clearAction();
            clearact();
            
            
        }
        if (collision.gameObject.tag == "Dead")
        {
            OnDamagedDeath(collision.transform.position);
        }

    }
    void onDamagedEnemy(Vector2 targetPos)
    {

        re.emove = false;
        anim.SetTrigger("damaged");
        int dirc = transform.position.x - targetPos.x > 0 ? 2 : -2;
        rigid.AddForce(Vector2.up*5, ForceMode2D.Impulse);
        GM_isdead = true;
        Invoke("Deact", 0.8f);

    }
    void OnDamagedDeath(Vector2 targetPos)
    {
        movestart = false;
        anim.SetTrigger("damaged");
        int dirc = transform.position.x - targetPos.x > 0 ? 2 : -2;
        transform.Translate(Vector2.zero);
        rigid.AddForce(Vector2.up*7, ForceMode2D.Impulse);
        rigid.gravityScale = 2;
        re.emove = false;
        //애니메이션
        
        GM_isdead = true;
        Invoke("Deact", 0.67f);
    }
    public void clearact()
    {
        clear = true;
        movestart = false;
        transform.Translate(Vector2.zero);
        anim.SetBool("isclear", true);
        

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
        rigid.gravityScale = 4;
        rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        PlaySound(audiojump);
       

    }
    private void FixedUpdate()
    {
        if (movestart)
        {

            rigid.bodyType = RigidbodyType2D.Dynamic;
            transform.Translate(Vector2.right * 0.05f);
            anim.SetBool("isrun", true);
        }
        else
        {
            anim.SetBool("isrun", false);
        }

        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("start")) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.18f))
        {
            anim.SetBool("isstart", false);
        }
        

        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 5,LayerMask.GetMask("ground"));
            if (rayHit.collider != null)
            {
              
                if (rayHit.distance <= 1f)
                {

                    
                    anim.SetBool("isjump", false);
                    rigid.gravityScale = 5;
                   
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

   



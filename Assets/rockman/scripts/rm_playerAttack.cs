using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class rm_playerAttack : MonoBehaviour
{
    public GameObject Bullet;
    public Transform pos;
    public rm_move rm;
    public rm_enemy re;
    public GameObject sign_A;
    AudioSource audiosource;
    public AudioClip Audioshoot;
    Animator anim;
    Rigidbody2D rigid;
    public bool isshoot=false;
    bool isladder;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();

    }

   
    public void rockman_Attack()
    {
        if (isshoot == false)
        {
            Debug.Log("fire");
            anim.SetBool("isrunAttack", true);
            Debug.Log("anim");
            Instantiate(Bullet, pos.position, transform.rotation);
            rm.PlaySound(Audioshoot);
            isshoot = true;
        }
        Invoke("rockman_isshoot", 0.5f);


    }
    public void rockman_isshoot()
    {
       
        anim.SetBool("isrunAttack", false);
        isshoot = false;
       
    }
    public void rockman_DeAttack()
    {
        anim.SetBool("isrunAttack", false);
    }
    void Update()
    {
        
   
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) { isladder = true; }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) { isladder = false; }
    }
}

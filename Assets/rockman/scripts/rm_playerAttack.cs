using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rm_playerAttack : MonoBehaviour
{
    public GameObject Bullet;
    public Transform pos;
    public rm_move rm;
    public rm_enemy re;
    public GameObject sign_A;
    AudioSource audiosource;
    public AudioClip Audioshoot;
    //public float cooltime;
    //private float curtime;
    Animator anim;
    Rigidbody2D rigid;
    public bool isshoot=false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    bool isladder;
    public void rockman_Attack()
    {
        if (isshoot == false)
        {
            Debug.Log("fire");
            anim.SetBool("isrunAttack", true);
            Instantiate(Bullet, pos.position, transform.rotation);
            rm.PlaySound(Audioshoot);
            isshoot = true;
        }
        Invoke("rockman_isshoot", 1f);


    }
    public void rockman_isshoot()
    {
        isshoot = false;
    }
    public void rockman_DeAttack()
    {
        anim.SetBool("isrunAttack", false);
    }
    void Update()
    {
        if (re.damage)
        {
            anim.SetBool("isrunAttack", false);
        }
        //if (curtime <= 0)
        //{
           
                //if (Input.GetKey(KeyCode.Z))
                //{
                //    if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))&&isladder==false)
                //    {
                //        anim.SetBool("isrunAttack", true);
                //        Instantiate(Bullet, pos.position, transform.rotation);
                //    }



                //    else if(isladder==false)
                //    {
                //        anim.SetBool("isshoot", true);
                //        Instantiate(Bullet, pos.position, transform.rotation);
                //    }
                //    else if (isladder == true)
                //{
                //    Instantiate(Bullet, pos.position, transform.rotation);
                //}


                //}
                //else
                //{
                //    anim.SetBool("isrunAttack", false);
                //    anim.SetBool("isshoot", false);
                //}
                //curtime = cooltime;

            //}
            //curtime -= Time.deltaTime;
 
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


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rm_Signcheck : MonoBehaviour
{
    public Animator anim;
    public float jumpPower;
    Rigidbody2D rigid;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    Transform pos;
    [SerializeField]
    LayerMask islayer;
    public rm_move rm;
    public rm_playerAttack rp;
    public LayerMask isLayer;
    public int distance;
    ///public rm_bullet rb;
    // Start is called before the first frame update
    bool isGround;
    //bool isshoot = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(rigid.position, Vector3.right, new Color(0, 1, 0));
        //if (isshoot)
        //{
        //    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right,distance, islayer);
        //    Debug.Log(rayHit.collider.tag);
        //    if (rayHit== false)
        //    {

        //            anim.SetBool("isrunAttack", false);
        //            isshoot = false;

                
             
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {
         
            if (collision.name.Contains("Sign_Up"))
            {
                rm.rockman_jump();
            }
            
     




            //if (collision.name.Contains("Sign_Down"))
            //{

            //}
            //if (collision.name.Contains("Sign_Left"))
            //{

            //}
            //if (collision.name.Contains("Sign_Right"))
            //{

            //}
            if (collision.name.Contains("Sign_A"))
            {
                rp.rockman_Attack();
                //isshoot = true;
            }
            //if (collision.name.Contains("Sign_B"))
            //{

            //}
            //if (collision.name.Contains("Sign_X"))
            //{

            //}
            //if (collision.name.Contains("Sign_Y"))
            //{

            //}
        }
    }
}
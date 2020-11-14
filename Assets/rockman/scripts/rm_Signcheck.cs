
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class rm_Signcheck : MonoBehaviour
{
    public Animator anim;
    Rigidbody2D rigid;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    Transform pos;
    [SerializeField]
    LayerMask islayer;
    public rm_move rm;
    public rm_playerAttack rp;
    public int distance;
    bool isGround;
    public bool doubleup;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.DrawRay(rigid.position, Vector3.right, new Color(0, 1, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {


            if (collision.name.Contains("Sign_A"))
            {
                rp.rockman_Attack();

            }
        

        }





    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {

            if (collision.name.Contains("Sign_Up"))
            {
                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2f, LayerMask.GetMask("ground"));
                if (rayHit.collider != null && rigid.velocity.y == 0)
                {
                    Debug.Log(rayHit.collider);
                    rm.rockman_jump();
                }
            }
            if (collision.name.Contains("Sign_DoubleUp"))
            {
                RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2f, LayerMask.GetMask("ground"));
                if (rayHit.collider != null && rigid.velocity.y == 0)
                {
                    Debug.Log(rayHit.collider);
                    doubleup = true;
                    rm.rockman_jump();
                }
            }
        }
     

    }
}
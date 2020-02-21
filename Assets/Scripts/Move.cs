using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    public bool move;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        move = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(move==true)
            rigid.velocity = new Vector2(3, rigid.velocity.y);

    }

    public void StartMove()
    {
        move = true;
        anim.SetBool("IsMove", true);
    }

    public void Reset()
    {
        gameObject.transform.position = new Vector3(-7, 1);
        rigid.velocity = new Vector2(0, 0);
        move = false;
        anim.SetBool("IsMove", false);
    }
}

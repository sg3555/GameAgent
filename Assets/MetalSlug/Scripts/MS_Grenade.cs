using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Grenade : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션 담당
    float speed = 12f;
    float destroyTime = 1.5f;
    bool isHit;
    int E_BulletLayerNum = 26;
    int groundLayerNum = 21;
    Rigidbody2D rigid; //물리엔진
    Collider2D col; //충돌제어자

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        //Destroy(gameObject, destroyTime);
        Invoke("explode", 1.5f);
        isHit = false;
        Physics2D.IgnoreLayerCollision(this.gameObject.layer, E_BulletLayerNum, true);

        rigid.AddForce(Vector2.up * 500f);
        rigid.AddForce(Vector2.right * 400f);
    }

    void FixedUpdate()
    {
        if (rigid.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, groundLayerNum, true);
        }
        else if (rigid.velocity.y < 0)
        {
            Physics2D.IgnoreLayerCollision(gameObject.layer, groundLayerNum, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        //rigid.bodyType = RigidbodyType2D.Static;
        //col.isTrigger = true;
        transform.Translate(Vector3.right * 0.7f);
        anim.SetTrigger("Explode");
        explode();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isHit = true;
        rigid.bodyType = RigidbodyType2D.Static;
        col.isTrigger = true;
        explode();
    }
    void explode()
    {
        anim.SetTrigger("Explode");
        Invoke("destroyObj", 0.5f);
    }

    void destroyObj()
    {
        Destroy(gameObject);
    }
}

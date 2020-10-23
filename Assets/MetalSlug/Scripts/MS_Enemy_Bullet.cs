﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Enemy_Bullet : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션 담당
    float speed = 12f;
    float destroyTime = 1.5f;
    bool isHit;
    private int enemyLayerNum = 23;

    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, destroyTime);
        isHit = false;

        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerNum, true);
    }

    void FixedUpdate()
    {
        if (!isHit)
            transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        transform.Translate(Vector3.left * 0.7f);
        destroyBullet();
        //anim.SetTrigger("Hit");
        //Invoke("DestroyBullet", 0.4f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isHit = true;
        transform.Translate(Vector3.left * 0.7f);
        destroyBullet();
    }
    void destroyBullet()
    {
        Destroy(gameObject);
    }
}

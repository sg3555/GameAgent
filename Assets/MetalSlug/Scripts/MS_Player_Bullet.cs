using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Player_Bullet : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션 담당
    float speed = 12f;
    float destroyTime = 1.5f;
    bool isHit;
    void Start()
    {
        anim = GetComponent<Animator>();
        //Destroy(gameObject, destroyTime);
        isHit = false;
    }

    void FixedUpdate()
    {
        if(!isHit)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision);
        isHit = true;
        Destroy(gameObject);
        transform.Translate(Vector3.right * 0.7f);
        anim.SetTrigger("Hit");
        Invoke("destroyBullet", 0.4f);
    }

    void destroyBullet()
    {
        Destroy(gameObject);
    }
}

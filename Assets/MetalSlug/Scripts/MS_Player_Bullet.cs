using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Player_Bullet : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션 담당
    float speed = 12f;
    float destroyTime = 1.5f;
    public float damage;
    bool isHit;
    void Start()
    {
        anim = GetComponent<Animator>();
        damage = 1f;
        Destroy(gameObject, destroyTime);
        isHit = false;
    }

    void FixedUpdate()
    {
        if(!isHit)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        transform.Translate(Vector3.right * 0.7f);
        anim.SetTrigger("Hit");
        Invoke("DestroyBullet", 0.5f);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

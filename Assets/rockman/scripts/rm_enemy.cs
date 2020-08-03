using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rm_enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    BoxCollider2D collide;
    Animator anim;
    bool damage = false;
    public float speed;
    bool isleft = true;
    // Start is called before the first frame update
    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()//적이 데미지를 받았을 경우 그 자리에서 멈추게 하고 그렇지 않으면 이동하게 하는 것
    {
        if (damage==true)
            transform.Translate(Vector2.zero);
        else
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)//적의 이동 거리를 정하기 위해 collision의 tag 중 endpoint를 지나게 되면 방향을 바꾸게 함
    {
        if (collision.tag == "endpoint")
        {
            if (isleft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isleft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isleft = true;
            }
        }
    }
    //적이 데미지를 받았을 시 하는 행동
    public void OnDamaged()
    {
        damage = true;
        //spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        //spriteRenderer.flipY = true;
        collide.enabled = false;
        //rigid.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        anim.SetTrigger("destroy");
        Invoke("DeActive", 0.5f);
    }
    void DeActive()//적이 사라지게 만드는 함수
    {
        gameObject.SetActive(false);
    }
}

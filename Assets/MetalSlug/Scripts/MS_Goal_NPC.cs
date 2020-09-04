using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Goal_NPC : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션
    public int PBulletLayerNum = 25;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        //플레이어의 공격이 닿았을 때
        if (collision.gameObject.layer == PBulletLayerNum && !anim.GetBool("IsClear"))
        {
            anim.SetBool("IsClear", true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Goal_NPC : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션
    public int PBulletLayerNum = 25;
    AudioSource audioSource; //소리제어자
    public AudioClip audioThanks;

    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.layer);
        //플레이어의 공격이 닿았을 때
        if (collision.gameObject.layer == PBulletLayerNum && !anim.GetBool("IsClear"))
        {
            anim.SetBool("IsClear", true);
            PlaySound(audioThanks);
        }
    }

    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }
}

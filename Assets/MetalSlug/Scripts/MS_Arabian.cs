using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Arabian : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션
    private int playerLayerNum = 22;
    private int enemyLayerNum = 23;
    private int P_BulletLayerNum = 25;
    private int E_BulletLayerNum = 26;
    AudioSource audioSource; //소리제어자
    public AudioClip audioDie;
    bool isDead;
    public GameObject knife;
    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isDead = false;
    }

    void FixedUpdate()
    {
        attack();
        useKnifeLayer();
    }

    void attack()
    {
        Debug.DrawRay(this.gameObject.transform.position, Vector2.left * 4f, new Color(0, 255, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.left, 4f, LayerMask.GetMask("Player"));
        if (rayHit.collider != null)
        {
            anim.SetBool("IsAttack", true);
        }
        else
        {
            anim.SetBool("IsAttack", false);
        }
    }

    void useKnifeLayer()
    {
        if (!isDead)
        {
            //근접공격 중 칼날에 타격판정 부여
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Arabian_Attack"))
            {
                knife.layer = E_BulletLayerNum;
                //Debug.Log(knife.layer);
            }
            else
            {
                knife.layer = enemyLayerNum;
            }
        }
    }

    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }

    public void die()
    {
        isDead = true;
        Debug.Log("arabian die");
        anim.SetTrigger("Die");
        Invoke("destroyObj", 0.4f);
    }
    void destroyObj()
    {
        Destroy(gameObject);
    }
}

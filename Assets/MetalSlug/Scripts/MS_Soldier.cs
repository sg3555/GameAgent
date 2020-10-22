using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_Soldier : MonoBehaviour
{
    Animator anim;  //스프라이트 애니메이션
    private int enemyLayerNum = 23;
    private int P_BulletLayerNum = 25;
    private int E_BulletLayerNum = 26;
    AudioSource audioSource; //소리제어자
    public AudioClip audioDie;
    private float fireRate = 1.3f;
    private float nextFire = 0f;
    public Transform muzzle;
    public GameObject bulletPrefab;
    bool isDead;
    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isDead = false;
    }

    void FixedUpdate()
    {
        fire();
    }

    void fire()
    {
        if (!isDead)
        {
            if (Time.time > nextFire)
            {
                anim.SetBool("IsFire", true);
                nextFire = Time.time + fireRate;
                Invoke("bullet", 0.1f);
            }
            else
            {
                anim.SetBool("IsFire", false);
            }
        }
    }
    void bullet()
    {
        GameObject tempBullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }
    void PlaySound(AudioClip action)
    {
        audioSource.clip = action;
        audioSource.Play();
    }

    public void die()
    {
        isDead = true;
        Debug.Log("soldier die");
        anim.SetTrigger("Die");
        Invoke("destroyObj", 0.4f);
    }
    void destroyObj()
    {
        Destroy(gameObject);
    }
}

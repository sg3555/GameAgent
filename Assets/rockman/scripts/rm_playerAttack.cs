using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rm_playerAttack : MonoBehaviour
{
    public GameObject Bullet;
    public Transform pos;
    public float cooltime;
    private float curtime;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    bool isladder;
    void Update()
    {
        if (curtime <= 0)
        {
           
                if (Input.GetKey(KeyCode.Z))
                {
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
                    {
                        anim.SetBool("isrunAttack", true);
                        Instantiate(Bullet, pos.position, transform.rotation);
                    }



                    else if(isladder==false)
                    {
                        anim.SetBool("isshoot", true);
                        Instantiate(Bullet, pos.position, transform.rotation);
                    }
                    else if (isladder == true)
                {
                    Instantiate(Bullet, pos.position, transform.rotation);
                }


                }
                else
                {
                    anim.SetBool("isrunAttack", false);
                    anim.SetBool("isshoot", false);
                }
                curtime = cooltime;

            }
            curtime -= Time.deltaTime;
 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) { isladder = true; }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ladder")) { isladder = false; }
    }
}

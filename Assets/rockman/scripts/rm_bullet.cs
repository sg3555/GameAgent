﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rm_bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    void Start()
    {
        Invoke("DestroyBullet",2);
       
    }

    //총알이 적에게 맞았는지 확인하는 함수
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position,transform.right,distance);
        if (ray.collider != null)
        {
            if(ray.collider.tag=="Enemy")
            {
                
              
                onAttack(ray.collider.transform);
                DestroyBullet();
                

            }
            Invoke("DestroyBullet", 1f);
        }
        //총의 방향을 정하는 것
        if (transform.rotation.y == 0)
        { 
            transform.Translate(transform.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right *(-1)* speed * Time.deltaTime);
        }
        
    }
    //적을 공격했을 경우 enemy 클래스인 enemyMove 변수 생성하고 변수에서 OnDamaged 함수를 실행
    void onAttack(Transform enemy)
    {
       rm_enemy enemyMove = enemy.GetComponent<rm_enemy>();
        enemyMove.OnDamaged();
        
    }
  
   //총알이 사라지게 하는 함수
    void DestroyBullet()
    {
       
        gameObject.SetActive(false);
    }
}

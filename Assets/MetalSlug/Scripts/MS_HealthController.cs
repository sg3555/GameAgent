using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_HealthController : MonoBehaviour
{
    private int P_BulletLayerNum = 25;
    public int originHealth;
    public int Health;
    bool isDead = false;

    private void Start()
    {
        originHealth = Health;
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        // 해당 오브젝트 파괴
        if (Health <= 0 && !isDead)
        {
            isDead = true;
            Physics2D.IgnoreLayerCollision(this.gameObject.layer, P_BulletLayerNum, true);
            //Physics2D.IgnoreLayerCollision(this.gameObject.layer, 22, true);
            if (gameObject.name == "Arabian")
            {
                //Debug.Log(gameObject.name);
                MS_Arabian arabian = gameObject.GetComponent<MS_Arabian>();
                arabian.die();
            }else if (gameObject.name == "Soldier")
            {
                MS_Soldier soldier = gameObject.GetComponent<MS_Soldier>();
                soldier.die();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == P_BulletLayerNum)
        {
            if(collision.gameObject.name == "Player_Bullet(Clone)")
            {
                Health -= 10;
                //Debug.Log(Health);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == P_BulletLayerNum)
        {
            if (collision.gameObject.name == "Grenade(Clone)")
            {
                Health -= 100;
                Debug.Log(Health);
            }
        }
    }

    //게임 리셋
    public void ResetGame()
    {
        isDead = false;
        Health = originHealth;
    }
}

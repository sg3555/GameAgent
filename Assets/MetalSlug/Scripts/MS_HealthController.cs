using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_HealthController : MonoBehaviour
{
    private int P_BulletLayerNum = 25;
    public int Health;
    bool isDead = false;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        // 해당 오브젝트 파괴
        if (Health <= 0 && !isDead)
        {
            isDead = true;
            if(gameObject.name == "Arabian")
            {
                //Debug.Log(gameObject.name);
                MS_Arabian ar = gameObject.GetComponent<MS_Arabian>();
                ar.Die();
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
}

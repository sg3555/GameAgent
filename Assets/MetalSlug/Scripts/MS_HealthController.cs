using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_HealthController : MonoBehaviour
{
    private int P_BulletLayerNum = 25;
    public int Health;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        // 해당 오브젝트 파괴
        if (Health <= 0)
        {
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == P_BulletLayerNum)
        {
            //Debug.Log(collision.gameObject.name);
            // HP를 잃음
        }
    }
}

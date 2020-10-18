using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        velocity = rigid.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid.velocity.y < velocity.y)
        {
            rigid.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigid.velocity = new Vector2(velocity.x, -velocity.y);

        if(collision.contacts[0].normal.x != 0)
        {

        }
    }

    void Explode()
    {
        Destroy(this.gameObject);
    }
}

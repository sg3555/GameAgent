
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rm_Signcheck : MonoBehaviour
{
    public Animator anim;
    public float jumpPower;
    Rigidbody2D rigid;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    Transform pos;
    [SerializeField]
    LayerMask islayer;
    public rm_move rm;
    // Start is called before the first frame update
    bool isGround;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {
         
            if (collision.name.Contains("Sign_Up"))
            {
                rm.rockman_jump();
            }
     




            //if (collision.name.Contains("Sign_Down"))
            //{

            //}
            //if (collision.name.Contains("Sign_Left"))
            //{

            //}
            //if (collision.name.Contains("Sign_Right"))
            //{

            //}
            //if (collision.name.Contains("Sign_A"))
            //{

            //}
            //if (collision.name.Contains("Sign_B"))
            //{

            //}
            //if (collision.name.Contains("Sign_X"))
            //{

            //}
            //if (collision.name.Contains("Sign_Y"))
            //{

            //}
        }
    }
}
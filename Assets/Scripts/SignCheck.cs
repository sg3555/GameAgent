using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sign")
        {
            if(collision.name.Contains("Sign_Up"))
            {

            }
            if (collision.name.Contains("Sign_Down"))
            {

            }
            if (collision.name.Contains("Sign_Left"))
            {

            }
            if (collision.name.Contains("Sign_Right"))
            {

            }
            if (collision.name.Contains("Sign_A"))
            {

            }
            if (collision.name.Contains("Sign_B"))
            {

            }
            if (collision.name.Contains("Sign_X"))
            {

            }
            if (collision.name.Contains("Sign_Y"))
            {

            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rm_obj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Active()
    {
        gameObject.SetActive(true);
    }
    public void DeActive()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappear : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (this.gameObject.transform.position.y <= -12)
            Destroy(gameObject);
    }
}

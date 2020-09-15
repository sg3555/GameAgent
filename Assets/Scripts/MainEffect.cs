using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEffect : MonoBehaviour
{
    //오브젝트들
    public Transform Mario_obj, Megaman_obj, Metalslug_obj;

    //오브젝트가 생성되는 좌표
    Vector2 produceArea;
    
    //오브젝트 회전도
    Quaternion rand_rotate;

    //랜덤X좌표
    float produce_x, quat_z, rand_second;

    /*
     * 1. 마리오
     * 2. 록맨
     * 3. 메탈슬러그
     */
    int sequence;

    private void Start()
    {
        produceObj();
    }

    void produceObj()
    {
        produce_x = Random.Range(-16f, 16f);
        sequence = Random.Range(1, 4);
        quat_z = Random.Range(-360f, 360f);
        produceArea = new Vector2(produce_x, 8);
        rand_rotate.eulerAngles = new Vector3(0, 0, quat_z);

        if (sequence == 1)
            Instantiate(Mario_obj, produceArea, rand_rotate);
        else if (sequence == 2)
            Instantiate(Megaman_obj, produceArea, rand_rotate);
        else if (sequence == 3)
            Instantiate(Metalslug_obj, produceArea, rand_rotate);

        rand_second = Random.Range(0.1f, 0.5f);
        Invoke("produceObj", rand_second);
    }
}

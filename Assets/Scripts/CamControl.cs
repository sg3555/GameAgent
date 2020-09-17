using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{

    public float min_x = 0, max_x = 0, min_y = -2, max_y = -2;   //카메라가 움직일수 있는 범위
    public float camSpeed = 1f; //카메라 조작 속도
    

    float xSmooth = 8f, ySmooth = 8f;   //카메라 움직임을 부드럽게
    float xMargin = 1f, yMargin = 1f;   //카메라중심과 캐릭터 사이 간격
    
    public GameObject characterObject;    //추적대상 캐릭터(주인공)의 게임오브젝트
    Transform actor;    //캐릭터 게임오브젝트의 트랜스폼 컴포넌트
    Vector3 originPosition; //카메라 최초위치

    public bool isStart;

    void Awake()
    {
        actor = characterObject.transform;
        isStart = false;
        originPosition = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (isStart)
            ChasePlayer();
        else
            ControlCamera();
    }

    void ChasePlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
            targetX = Mathf.Lerp(transform.position.x, actor.position.x, xSmooth * Time.deltaTime);
        if (CheckYMargin())
            targetY = Mathf.Lerp(transform.position.y, actor.position.y, ySmooth * Time.deltaTime);

        targetX = Mathf.Clamp(targetX, min_x, max_x);
        targetY = Mathf.Clamp(targetY, min_y, max_y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);


    }

    void ControlCamera()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (Input.GetKey(KeyCode.A))
            targetX = Mathf.Lerp(transform.position.x, -camSpeed + transform.position.x, xSmooth * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))
            targetX = Mathf.Lerp(transform.position.x, camSpeed + transform.position.x, xSmooth * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
            targetY = Mathf.Lerp(transform.position.y, camSpeed + transform.position.y, ySmooth * Time.deltaTime);
        else if (Input.GetKey(KeyCode.S))
            targetY = Mathf.Lerp(transform.position.y, -camSpeed + transform.position.y, ySmooth * Time.deltaTime);

        targetX = Mathf.Clamp(targetX, min_x, max_x);
        targetY = Mathf.Clamp(targetY, min_y, max_y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }


    //카메라의 위치가 캐릭터의 마진 내부에 있는지 체크
    bool CheckXMargin()
    { return Mathf.Abs(transform.position.x - actor.position.x) > xMargin; }

    bool CheckYMargin()
    { return Mathf.Abs(transform.position.y - actor.position.y) > yMargin; }

    public void StartGame()
    {
        isStart = true;
    }
    
    public void StopGame()
    {
        isStart = false;
        transform.position = originPosition;
    }

    public void ResetGame()
    {
        isStart = false;
        transform.position = originPosition;
    }
}

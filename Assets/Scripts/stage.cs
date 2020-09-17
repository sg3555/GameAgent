using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class stage : MonoBehaviour
{
    public GameObject Main, Btns; // 캔버스
    Vector3 defaultScale;//버튼의 원래 크기 설정
    bool turnscreen;

    private void Start()
    {
        turnscreen = false;
        Main.SetActive(true);
        Btns.SetActive(false);
    }

    public void ChangeScene()
    {
        if(turnscreen == false)
        {
            turnscreen = true;
            Main.SetActive(false);
            Btns.SetActive(true);
        }
    }

    public void marioscn()//마리오 씬으로 넘어가는 함수
    {
        SceneManager.LoadScene("Mario_1");
    }

    public void magamanscn()//뒤를 눌렀을 경우 넘어가는 함수
    {
        SceneManager.LoadScene("rockman_1");
    }

    public void metalslugscn()
    {
        SceneManager.LoadScene("MetalSlug_1");
    }


}

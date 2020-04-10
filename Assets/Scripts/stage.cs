using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class stage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Transform buttonscale;//버튼의 크기를 결정하는 변수
    Vector3 defaultScale;//버튼의 원래 크기 설정
    private void Start()
    {
        defaultScale = buttonscale.localScale;//원래 버튼의 크기를 초기화한다.
    }

    public void marioscn()//마리오 씬으로 넘어가는 함수
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void backbtn()//뒤를 눌렀을 경우 넘어가는 함수
    {
        SceneManager.LoadScene("mainscene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonscale.localScale = defaultScale * 1.2f;//초기값의 1.2배하고 버튼의 스케일 변수에 넣는다.
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonscale.localScale = defaultScale;
    }
}

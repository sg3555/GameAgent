using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class main : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Transform buttonscale;//버튼의 크기를 결정하는 변수
    Vector3 defaultScale;//버튼의 원래 크기 설정
    private void Start()
    {
        defaultScale = buttonscale.localScale;//원래 버튼의 크기를 초기화한다.
    }
    public void startbtn()//start버튼을 눌렀을 경우 stage로 넘어가는 함수
    {
        SceneManager.LoadScene("stage");

            }
    public void Quit()//Quit버튼을 눌렀을 경우 넘어가는 함수
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)//버튼 위에 마우스를 갖다댔을 때 실행되는 함수
    {
        buttonscale.localScale = defaultScale * 1.2f;//초기값의 1.2배하고 버튼의 스케일 변수에 넣는다.
    }

    public void OnPointerExit(PointerEventData eventData)//버튼 위에서 마우스가 벗어났을시 실행되는 함수
    {
        buttonscale.localScale = defaultScale;
    }
}

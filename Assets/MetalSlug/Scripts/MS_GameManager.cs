using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MS_GameManager : MonoBehaviour
{
    public MS_PlayerController player; //플레이어블 캐릭터
    public Button[] buttons = new Button[3]; //시작, 정지, 초기화 버튼

    // Start is called before the first frame update
    void Start()
    {

    }

    //캐릭터 행동개시
    public void goButton()
    {
        player.startMove();
    }

    public void stopButton()
    {
        player.stopGame();
    }

    public void resetButton()
    {
        player.resetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //버튼 비활성화 함수
    public void disableButton()
    {
        buttons[0].interactable = false;
        buttons[1].interactable = false;
        buttons[2].interactable = false;
    }

    //버튼 활성화 함수
    public void enableButton()
    {
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        buttons[2].interactable = true;
    }
}

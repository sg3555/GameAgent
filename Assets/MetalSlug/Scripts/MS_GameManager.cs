using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MS_GameManager : MonoBehaviour
{
    public MS_PlayerController player; //플레이어블 캐릭터
    public Button[] buttons = new Button[3]; //시작, 정지, 초기화 버튼
    public BgmController MainBGM, StartBGM, GoalBGM; //배경음 관리자
    public Drager[] MovableTile, Inventory; //아이템창
    public CamControl mainCam; //카메라
    public GameObject ClearUI, ExplainUI; //깃발, 클리어UI, 설명창UI
    bool isopen; //설명창 전용 bool
    bool clearOnce;
    Collider2D buttonArea;
    // Start is called before the first frame update
    void Start()
    {
        MainBGM.PlaySound();
        MainBGM.SetLoop(true);
        MainBGM.SetVolume(0.7f);
        StartBGM.PlaySound();

        isopen = false;
        clearOnce = false;
        ExplainUI.SetActive(isopen);

        MovableTile = GameObject.Find("MovableItem").GetComponentsInChildren<Drager>();
        Inventory = GameObject.Find("Inventory").GetComponentsInChildren<Drager>();
        buttonArea = GameObject.Find("ButtonArea").GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        //클리어 여부 체크
        if (player.getState().Equals("clear") && !clearOnce)
        {
            MainBGM.StopSound();
            GoalBGM.PlaySound();
            clearScreen();
            clearOnce = true;
            //Debug.Log(player.getState());
        }
    }

    //캐릭터 행동개시
    public void goButton()
    {
        mainCam.StartGame();
        player.startMove();
        foreach (Drager dr in MovableTile)
            dr.StartGame();
        foreach (Drager dr in Inventory)
            dr.StartGame();
        MainBGM.SetVolume(1.0f);
        buttonArea.enabled = false;
    }

    public void stopButton()
    {
        mainCam.StopGame();
        player.resetGame();
        foreach (Drager dr in MovableTile)
            dr.StopGame();
        foreach (Drager dr in Inventory)
            dr.StopGame();
        MainBGM.SetVolume(0.7f);
        buttonArea.enabled = true;
    }

    public void resetButton()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        mainCam.ResetGame();
        player.resetGame();
        foreach (Drager dr in MovableTile)
            dr.ResetGame();
        foreach (Drager dr in Inventory)
            dr.ResetGame();
        MainBGM.SetVolume(0.7f);
        buttonArea.enabled = true;
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
    public void clearScreen()
    {
        ClearUI.SetActive(true);
    }
    //게임자체 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //버튼 누르면 설명창 반전
    public void OpenExplain()
    {
        isopen = !isopen;
        ExplainUI.SetActive(isopen);
    }
}

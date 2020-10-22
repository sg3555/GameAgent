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
    public GameObject ammoBox;

    MS_Arabian[] arabian;
    MS_Soldier[] soldier;
    MS_HealthController[] healthController;

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

        arabian = GameObject.Find("Enemy").GetComponentsInChildren<MS_Arabian>();
        soldier = GameObject.Find("Enemy").GetComponentsInChildren<MS_Soldier>();
        healthController = GameObject.Find("Enemy").GetComponentsInChildren<MS_HealthController>();

        MovableTile = GameObject.Find("MovableItem").GetComponentsInChildren<Drager>();
        Inventory = GameObject.Find("Inventory").GetComponentsInChildren<Drager>();
        buttonArea = GameObject.Find("ButtonArea").GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        //클리어 여부 체크
        //if (player.GM_clear && !clearOnce)
        //{
        //    MainBGM.StopSound();
        //    GoalBGM.PlaySound();
        //    clearScreen();
        //    clearOnce = true;
        //    //Debug.Log(player.getState());
        //}
        //else if (player.getState().Equals("dead"))
        //{

        //}
        //Debug.Log(soldier[0].Test());
        if (player.GM_clear)
        {
            MainBGM.StopSound();
            GoalBGM.PlaySound();
            disableButton();
            clearScreen();
            player.GM_clear = false;
        }

        if (player.GM_goal)
        {
            disableButton();
            player.GM_goal = false;
        }

        if (player.GM_isdead)
        {
            deadAction();
            player.GM_isdead = false;
        }

        if (player.isLoaded)
        {
            ammoBox.SetActive(false);
        }
    }

    void deadAction()
    {
        //메인 배경음 볼륨 0으로 줄이고 사망 배경음 재생
        MainBGM.SetVolume(0);
        //DeadBGM.PlaySound();
        //버튼 비활성화
        disableButton();
        Invoke("stopButton", 1.5f);
        Invoke("enableButton", 1.5f);

        //적들 애니메이션과 움직임 중지
        //foreach (Mario_Goomba gm in Goomba)
        //    gm.StopGame();
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
        foreach (MS_Soldier so in soldier)
            so.StartGame();
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
        foreach (MS_Soldier so in soldier)
            so.ResetGame();
        foreach (MS_HealthController hc in healthController)
            hc.ResetGame();
        ammoBox.SetActive(true);
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
        foreach (MS_Soldier so in soldier)
            so.ResetGame();
        foreach (MS_HealthController hc in healthController)
            hc.ResetGame();
        ammoBox.SetActive(true);
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

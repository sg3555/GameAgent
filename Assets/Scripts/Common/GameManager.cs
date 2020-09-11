using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Mario Actor; //마리오
    public Drager[] MovableTile, Inventory; //아이템창
    public BgmController MainBGM, DeadBGM, Goal1, Goal2; //배경음 관리자
    public Button[] buttons = new Button[3]; //시작, 정지, 초기화 버튼
    public GameObject flag, ClearUI, ExplainUI; //깃발, 클리어UI, 설명창UI
    public CamControl mainCam; //카메라
    //이건 클리어 여부 확인을 위한것이 아닌 클리어 후 애니메이션 설정을 위해 둔 bool값
    public bool clear;
    bool isopen; //설명창 전용 bool

    private void Start()
    {
        //메인배경음 재생, 블럭을 옮기는 중일때에는 볼륨 0.3f로 설정
        MainBGM.PlaySound();
        MainBGM.SetLoop(true);
        MainBGM.SetVolume(0.7f);
        clear = false;
        isopen = false;
        ExplainUI.SetActive(isopen);

        MovableTile = GameObject.Find("MovableItem").GetComponentsInChildren<Drager>();
        Inventory = GameObject.Find("Inventory").GetComponentsInChildren<Drager>();
    }

    private void FixedUpdate()
    {
        //클리어 했을 시 깃발이 조금씩 내려오는 모션을 위한 부분
        if (clear && flag.transform.position.y >= -5.5)
            flag.transform.Translate(new Vector3(0, -0.15f, 0));

        if (Actor.GM_clear)
        {
            clearScreen();
            Actor.GM_clear = false;
        }

        if(Actor.GM_isdead)
        {
            deadAction();
            Actor.GM_isdead = false;
        }

        if(Actor.GM_goal)
        {
            goalAction();
            Actor.GM_goal = false;
        }
            
    }

    //캐릭터 사망시 행동
    public void deadAction()
    {
        //메인 배경음 볼륨 0으로 줄이고 사망 배경음 재생
        MainBGM.SetVolume(0);
        DeadBGM.PlaySound();

        //버튼 비활성화
        disableButton();
        //2.712초 후(배경음 재생 완료 후) 게임상태 Stop상태로 전환, 버튼 활성화
        Invoke("stopGame", 2.712f);
        Invoke("enableButton", 2.712f);
    }

    //캐릭터 클리어시 행동
    public void goalAction()
    {
        //버튼 비활성화
        disableButton();
        //배경음 전환, 깃발 내려감
        clear = true;
        MainBGM.StopSound();
        Goal1.PlaySound();
        //1.172초 후 클리어 배경음으로 전환
        Invoke("clearAction", 1.172f);
    }

    //클리어 배경음 2차전환
    public void clearAction()
    {
        Goal2.PlaySound();
    }

    //클리어 UI호출
    public void clearScreen()
    {
        ClearUI.SetActive(true);
    }

    //캐릭터 행동개시
    public void startUpGame()
    {
        mainCam.StartGame();
        Actor.StartMove();
        foreach (Drager dr in MovableTile)
            dr.StartGame();
        foreach (Drager dr in Inventory)
            dr.StartGame();
        MainBGM.SetVolume(1.0f);
    }

    //캐릭터 행동 중단
    public void stopGame()
    {
        mainCam.StopGame();
        Actor.ResetGame();
        foreach (Drager dr in MovableTile)
            dr.StopGame();
        foreach (Drager dr in Inventory)
            dr.StopGame();
        MainBGM.SetVolume(0.7f);
    }

    //맵 초기화
    public void resetGame()
    {
        mainCam.ResetGame();
        Actor.ResetGame();
        foreach (Drager dr in MovableTile)
            dr.ResetGame();
        foreach (Drager dr in Inventory)
            dr.ResetGame();
        MainBGM.SetVolume(0.7f);
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

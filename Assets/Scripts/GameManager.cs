using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Move Actor; //마리오
    public Drag[] MovableTile; //움직일 수 있는 아이템들
    public BgmController MainBGM, DeadBGM, Goal1, Goal2; //배경음 관리자
    public Button[] buttons = new Button[3]; //시작, 정지, 초기화 버튼
    public GameObject flag, ClearUI; //깃발, 클리어UI
    //이건 클리어 여부 확인을 위한것이 아닌 클리어 후 애니메이션 설정을 위해 둔 bool값
    public bool clear; 

    private void Start()
    {
        //메인배경음 재생, 블럭을 옮기는 중일때에는 볼륨 0.3f로 설정
        MainBGM.PlaySound();
        MainBGM.SetLoop(true);
        MainBGM.SetVolume(0.7f);
        clear = false;
    }

    private void FixedUpdate()
    {
        //클리어 했을 시 깃발이 조금씩 내려오는 모션을 위한 부분
        if (clear && flag.transform.position.y >= -5.5)
            flag.transform.Translate(new Vector3(0, -0.15f, 0));
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
        Actor.StartMove();
        foreach (Drag dr in MovableTile)
            dr.StartMove();
        MainBGM.SetVolume(1.0f);
    }

    //캐릭터 행동 중단
    public void stopGame()
    {
        Actor.ResetGame();
        foreach (Drag dr in MovableTile)
            dr.StopMove();
        MainBGM.SetVolume(0.7f);
    }

    //맵 초기화
    public void resetGame()
    {
        Actor.ResetGame();
        foreach (Drag dr in MovableTile)
        {
            ItemProperty item = dr.GetComponent<ItemProperty>();
            item.ResetBtn();
        }
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
        SceneManager.LoadScene(0);
    }

    //최종수정일 2020.03.10 오후 19:47
}

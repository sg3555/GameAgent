using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_RM : MonoBehaviour
{
    public static GameManager_RM gm;
    public Bgm_controller_RM MainBgm, clear;
    public rm_move rm;
    public bool start = false;
    public Button[] Btn = new Button[3];
    private void Awake()
    {
        gm = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MainBgm.PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        //startBtn();
    }
   public void startBtn()
    {
        start = true;
        disableButton();

        //rm.Start_move();

    }
    public void clearAction()
    {
        disableButton();
        MainBgm.StopSound();
        clear.PlaySound();
    }
    public void disableButton()
    {
        Btn[0].interactable = false;
        Btn[1].interactable = false;
        Btn[2].interactable = false;
    }

    public void enableButton()
    {
        Btn[0].interactable = true;
        Btn[1].interactable = true;
        Btn[2].interactable = true;
    }
}

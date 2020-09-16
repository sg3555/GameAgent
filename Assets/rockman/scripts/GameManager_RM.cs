﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_RM : MonoBehaviour
{
    public static GameManager_RM gm;
    public Bgm_controller_RM MainBgm, clear,DeadBgm;
    public Drager[] MovableTile, Inventory;
    //public GameObject Enemy;
    public rm_enemy[] enemy;
    public rm_move rm;
    public bool start = false;
    public CamControl mainCam; //카메라
    public Button[] Btn = new Button[3];
    public GameObject ExplainUI;
    bool isopen;
    public bool reset = false;
    public bool stop = false;
    private void Awake()
    {
        gm = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MainBgm.PlaySound();
        MainBgm.SetLoop(true);
        MovableTile = GameObject.Find("MovableItem").GetComponentsInChildren<Drager>();
        Inventory = GameObject.Find("Inventory").GetComponentsInChildren<Drager>();
        enemy= GameObject.Find("Enemy").GetComponentsInChildren<rm_enemy>();
    }


    void FixedUpdate()
    {

        if (rm.GM_isdead)
        {
            deadAction();
            rm.GM_isdead = false;
        }
    }
   public void startBtn()
    {
        mainCam.StartGame();
        start = true;
        disableButton();
        foreach (Drager dr in MovableTile)
            dr.StartGame();
        foreach (Drager dr in Inventory)
            dr.StartGame();
        foreach (rm_enemy en in enemy)
            en.enemyMove();
       

    }
    public void resetGame()
    {
        start = false;
        reset = true;   
        mainCam.ResetGame();
        rm.ResetGame();
        foreach (Drager dr in MovableTile)
            dr.ResetGame();
        foreach (Drager dr in Inventory)
            dr.ResetGame();
        foreach (rm_enemy en in enemy)
            en.enemyStop();
        MainBgm.PlaySound();
        MainBgm.SetVolume(0f);
    }

    public void stopGame()
    {
        start = false;
        stop = true;
        mainCam.StopGame();
        rm.ResetGame();
        foreach (Drager dr in MovableTile)
            dr.StopGame();
        foreach (Drager dr in Inventory)
            dr.StopGame();
        foreach (rm_enemy en in enemy)
            en.enemyStop();
        MainBgm.PlaySound();
        MainBgm.SetVolume(0f);
    }
    public void deadAction()
    {
        MainBgm.StopSound();
        Invoke("deadBgm", 0.1f);
        DeadBgm.PlaySound();
        start = false;
        disableButton();
        Invoke("StopGame", 3f);
        Invoke("enableButton", 3f);
    }
    void deadBgm()
    {
        DeadBgm.PlaySound();
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
    public void OpenExplain()
    {
        isopen = !isopen;
        ExplainUI.SetActive(isopen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_RM : MonoBehaviour
{
    public static GameManager_RM gm;
    public Bgm_controller_RM MainBgm, clear;
    public Rm_Drager[] MovableTile, Inventory;
    public rm_move rm;
    public bool start = false;
    public CamControl mainCam; //카메라
    public Button[] Btn = new Button[3];
    private void Awake()
    {
        gm = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        MainBgm.PlaySound();
        
        MovableTile = GameObject.Find("MovableItem").GetComponentsInChildren<Rm_Drager>();
        Inventory = GameObject.Find("Inventory").GetComponentsInChildren<Rm_Drager>();
    }


    // Update is called once per frame
    void Update()
    {
        //startBtn();
    }
   public void startBtn()
    {
        mainCam.StartGame();
        start = true;
        disableButton();
        foreach (Rm_Drager dr in MovableTile)
            dr.StartGame();
        foreach (Rm_Drager dr in Inventory)
            dr.StartGame();
        //rm.Start_move();

    }
    public void resetGame()
    {
        mainCam.ResetGame();
        rm.ResetGame();
        foreach (Rm_Drager dr in MovableTile)
            dr.ResetGame();
        foreach (Rm_Drager dr in Inventory)
            dr.ResetGame();
        MainBgm.SetVolume(0.7f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public bool actionStart;
    //public bool Reset;
    public Move Actor;
    public Drag[] MovableTile;
    public BGM bgm;


    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startUpGame()
    {
        Actor.StartMove();
        foreach (Drag dr in MovableTile)
            dr.StartMove();
        bgm.SetVolume(1.0f);
    }

    public void stopGame()
    {
        Actor.ResetGame();
        foreach (Drag dr in MovableTile)
            dr.StopMove();
        bgm.SetVolume(0.3f);

    }

    public void resetGame()
    {
        Actor.ResetGame();
        foreach (Drag dr in MovableTile)
            dr.ResetGame();
        bgm.SetVolume(0.3f);
    }

    

    //최종수정일 2020.03.10 오후 19:47
}

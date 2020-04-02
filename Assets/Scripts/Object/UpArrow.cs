using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArrow : MonoBehaviour, IInventortItem
{
    public Inventory inventory;
    public string Name
    {
        get
        {
            return "UpArrow";
        }
    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }





    // 실행 후 2초 뒤 화살표를 인벤토리에 넣음
    float timer;
    int waitingTime;
    private void Start()
    {
        timer = 0.0f;
        waitingTime = 2;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            inventory.AddItem(this);
            timer = 0;
        }
    }
}

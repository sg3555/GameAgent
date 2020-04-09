using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }    //아이템 이름
    Sprite Image { get; }   //아이템 스프라이트
    void OnPickup();    //아이템을 인벤토리에 넣을 때
    void OnDrop();  //인벤토리에서 아이템을 꺼낼 때
    GameObject GetGameObject(); //게임오브젝트 속성을 컨트롤할 수 있도록 gameObject를 반환
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}

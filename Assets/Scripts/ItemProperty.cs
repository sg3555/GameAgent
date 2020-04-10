﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * InventoryItem 인터페이스 참조
 */

public class ItemProperty : MonoBehaviour, IInventoryItem
{
    public Inventory inventory;

    public string _Name = null;
    public string Name
    {
        get
        {
            return _Name;
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
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop()
    {

    }
}
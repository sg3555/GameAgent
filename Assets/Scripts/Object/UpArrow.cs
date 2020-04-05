using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArrow : MonoBehaviour, IInventoryItem
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

    public void OnDrop()
    {
        //Debug.Log("drop");
        gameObject.SetActive(true);
    }

}

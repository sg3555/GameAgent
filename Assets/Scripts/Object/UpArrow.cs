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
        //gameObject.SetActive(true);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnUse()
    {
        //Debug.Log(this.transform.parent);
        //drag = gameObject.GetComponent<Drag>();
        ////Debug.Log("use");
        //gameObject.SetActive(true);
        //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition);
        //Vector2 objPosition = new Vector2(Mathf.Floor(temp.x) + 0.5f, Mathf.Floor(temp.y) + 0.5f);
        //gameObject.transform.position = objPosition;

        //drag.OnMouseDown();
    }
    //void Update()
    //{
    //    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    Vector2 temp = Camera.main.ScreenToWorldPoint(mousePosition);
    //    Vector2 objPosition = new Vector2(Mathf.Floor(temp.x) + 0.5f, Mathf.Floor(temp.y) + 0.5f);
    //    transform.position = objPosition;
    //}
    
}

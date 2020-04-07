using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory Inventory;
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }

    //인벤토리에 오브젝트를 넣을 때 발생하는 이벤트 핸들러
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventory = transform.Find("Inventory");

        foreach (Transform slot in inventory)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);    //slot의 child의 child인 ItemImage
            Image image = imageTransform.GetComponent<Image>(); //ItemImage 오브젝트의 스프라이트
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();   //ItemImage 오브젝트의 드래그 핸들러 스크립트

            //해당 슬롯이 사용중인지 검사, 사용중이 아닌경우 해당 슬롯을 사용
            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }

    //인벤토리에서 오브젝트를 뺄 때 발생하는 이벤트 핸들러
    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventory = transform.Find("Inventory");

        foreach (Transform slot in inventory)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if(itemDragHandler.Item != null)
            {
                if (itemDragHandler.Item.Equals(e.Item))
                {
                    image.enabled = false;
                    image.sprite = null;
                    itemDragHandler.Item = null;
                    break;
                }
            }
            
        }
    }
}

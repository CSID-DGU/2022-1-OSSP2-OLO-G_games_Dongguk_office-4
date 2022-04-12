using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public GameObject inventoryLayout;
    public GameObject inventoryItemContent;
    private void OnEnable()
    {
        SetItems();
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetItems()
    {
        for(int i = inventoryLayout.transform.childCount-1;i>=0;i--)
        {
            Destroy(inventoryLayout.transform.GetChild(i).gameObject);
        }
        //init itemStatus        
        foreach(var i in DataMangaer.userData.inventory)
        {
            
            GameObject item = Instantiate(inventoryItemContent, inventoryLayout.transform) as GameObject;          
            var itemSC = item.GetComponent<InventoryItemContent>();
            //allocate hotkey
            itemSC.onClickAction = HotkeyAllocate;
            itemSC.itemImage.sprite = ItemDB.instance.items[i.Key].GetComponent<Item>().itemImage;
            itemSC.amoutOfItemText.text = "X" + i.Value.ToString();
        }
    }

    public void HotkeyAllocate()
    {
        InGameUIManager.instance.StartBlinkHotKeyNumber();
    }
}

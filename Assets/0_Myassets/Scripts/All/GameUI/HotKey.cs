using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class HotKey : MonoBehaviour
{
    public int keyCode;
    public int itemCode;
    public int itemAmount = 0;
    public Action<int> myDele;
    public Image itemImage;
    public Text amountOfItemText;
    

    public void HotKeyButton()
    {

        myDele?.Invoke(InGameUIManager.instance.nowSelectedItemCode);

        if (itemCode == -1)
        {
            //if item not allocated, return
            return;
        }
        
        if (DataMangaer.userData.inventory.ContainsKey(itemCode))
        {
            itemAmount = DataMangaer.userData.inventory[itemCode];
        }

        if (InGameUIManager.instance.isHotKeyAllocating)
        {
            InGameUIManager.instance.isHotKeyAllocating = false;
            myDele = null;
        }
        else if (itemAmount > 0&&InGameUIManager.instance.isHotKeyAllocating==false)
        {
            Debug.Log("?????? ????:" + itemCode);
            DataMangaer.instance.ConsumItem(itemCode);
            if (Inventory.instance != null)
            {
                Inventory.instance.SetItems();
            }
            

            //???? ???? ???? ?????? ?????? ???? ?????? ???? ????????????
            foreach(var i in DataMangaer.instance.hotKeys)
            {
                i.GetComponent<HotKey>().UpdateAmountOfItem();
            }
           
        }
        DataMangaer.instance.saveData();
       
        


    }
    public void UpdateAmountOfItem()
    {
        int itemAmount;
        if (DataMangaer.userData.inventory.ContainsKey(itemCode))
        {
            itemAmount = DataMangaer.userData.inventory[itemCode];
        }
        else
        {
            itemAmount = 0;
        }
        amountOfItemText.text = "X"+ itemAmount.ToString();
        if (itemAmount <= 0)
        {
            //?????? ???? ?? ?????? ???? ????
            itemImage.color = new Color(1, 1, 1, 0.5f);
        
        }
        else
        {

        }
        
    }

    public void SetHotKey(int itemCode)
    {
        myDele = null;
        Debug.Log("SetHotkey: " + itemCode.ToString());
        if (DataMangaer.userData.inventory.ContainsKey(itemCode))
        {
            if (DataMangaer.userData.inventory[itemCode] > 0)
            {
                this.itemCode = itemCode;
                this.itemAmount = DataMangaer.userData.inventory[itemCode];
                itemImage.color = new Color(1, 1, 1, 1);
                itemImage.sprite = ItemDB.instance.items[itemCode].GetComponent<Item>().itemImage;
                amountOfItemText.text = "X" + DataMangaer.userData.inventory[itemCode].ToString();
                DataMangaer.userData.hotKeyItems[keyCode] = itemCode;
            }
        }      
        InGameUIManager.instance.StopBlinkHotKeyNumber();
        
        
    }

    public void UpdateHotKeyInfo()
    {
        SetHotKey(itemCode);
    }

}

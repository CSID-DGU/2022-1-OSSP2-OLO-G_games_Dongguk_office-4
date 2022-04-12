using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class HotKey : MonoBehaviour
{
    public int itemCode;
    public Action<int> myDele;
    public Image itemImage;
    public Text amountOfItemText;    
   
    public void HotKeyButton()
    {
        myDele?.Invoke(itemCode);
        if (itemCode == -1)
        {
            //if item not allocated, return
            return;
        }
        int itemAmount = DataMangaer.userData.inventory[itemCode];
        
        if (itemAmount > 0)
        {
            Debug.Log("?????? ????:" + itemCode);
            DataMangaer.instance.ConsumItem(itemCode);

            //???? ???? ???? ?????? ?????? ???? ?????? ???? ????????????
            foreach(var i in DataMangaer.instance.hotKeys)
            {
                i.GetComponent<HotKey>().UpdateAmountOfItem();
            }
           
        }
       
        
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
        Debug.Log("SetHotkey: " + itemCode.ToString());
        if (itemCode != -1)
        {
            this.itemCode = itemCode;
            itemImage.sprite = ItemDB.instance.items[itemCode].GetComponent<Item>().itemImage;
            amountOfItemText.text = "X" + DataMangaer.userData.inventory[itemCode].ToString();

        }
        
    }
  
   

   
}

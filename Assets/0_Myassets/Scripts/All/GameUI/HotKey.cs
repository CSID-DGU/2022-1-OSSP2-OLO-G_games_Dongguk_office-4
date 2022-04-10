using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class HotKey : MonoBehaviour
{
    public int itemCode;
    public Action myDele;
    public Image itemImage;
    public Text amountOfItemText;    
   
    public void HotKeyButton()
    {
        int itemAmount = DataMangaer.userData.inventory[itemCode];
        myDele?.Invoke();
        if (itemAmount > 0)
        {
            Debug.Log("아이템 사용:" + itemCode);
            DataMangaer.instance.ConsumItem(itemCode);

            //여러 키에 같은 아이템 등록한 경우 때문에 모두 실행해줘야댐
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
            //아이템 소진 시 아이템 컬러 조절
            itemImage.color = new Color(1, 1, 1, 0.5f);
        
        }
        else
        {

        }
        
    }

    public void SetHotKey(int itemCode)
    {
        this.itemCode = itemCode;
        itemImage.sprite = ItemDB.instance.items[itemCode].GetComponent<Item>().itemImage;
        amountOfItemText.text = "X"+DataMangaer.userData.inventory[itemCode].ToString();
    }
  
   

   
}

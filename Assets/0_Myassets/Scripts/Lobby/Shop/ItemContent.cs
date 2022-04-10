using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemContent : MonoBehaviour
{
    public int itemCode;
    public Image itemImage;
    public TMP_Text itemDescriptionBox;
    public TMP_Text itemPrictBox;
    private void Awake()
    {
        
    }
 
    public void SetItemInfo()
    {
        Item item = ItemDB.instance.items[itemCode].GetComponent<Item>();
        itemImage.sprite = item.itemImage;
        itemDescriptionBox.text = item.description;
        itemPrictBox.text = "°¡°Ý:" + item.itemPrice.ToString() + "°ñµå";
    }
    public void PlusItemBtn()
    {

    }
    public void MinusItemBtn()
    {

    }
}

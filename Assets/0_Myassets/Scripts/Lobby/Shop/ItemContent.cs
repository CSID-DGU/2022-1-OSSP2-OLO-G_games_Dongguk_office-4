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
    public TMP_Text amountOfItemBox;
    public int amountOfItem = 0;
    private void Awake()
    {
        
    }
 
    public void SetItemInfo()
    {
        Item item = ItemDB.instance.items[itemCode].GetComponent<Item>();
        itemImage.sprite = item.itemImage;
        itemDescriptionBox.text = item.description;
        itemPrictBox.text = "Price: " + item.itemPrice.ToString() + "Gold";
        amountOfItemBox.text = "X" + amountOfItem.ToString();
    }
    public void PlusItemBtn()
    {
        ConsumptionShopManager.instance.PlusItem(itemCode);
        amountOfItem++;
        amountOfItemBox.text = "X" + amountOfItem.ToString();
    }
    public void MinusItemBtn()
    {
        if (amountOfItem > 0)
        {
            ConsumptionShopManager.instance.MinusItem(itemCode);
            amountOfItem--;
            amountOfItemBox.text = "X" + amountOfItem.ToString();
        }        
    }
}

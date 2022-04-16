using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConsumptionShopManager : MonoBehaviour
{
    public static ConsumptionShopManager instance;

    public TMP_Text sumOfPriceTextBox;

    //???????? ?????? ????
    int[] itemArray = new int[] {200, 201};

    List<GameObject> listOfItems;
    public GameObject ItemLayout;
    public GameObject ItemContent;
    Dictionary<int, int> wantBuyItems;
    int allItemPrice = 0;
    private void Awake()
    {
        if (instance == null)
        {            
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        listOfItems = new List<GameObject>();
        wantBuyItems = new Dictionary<int, int>();
    }
    private void Start()
    {
        for(int i = 0; i < itemArray.Length; i++)
        {
            GameObject shopItem = Instantiate(ItemContent,ItemLayout.transform) as GameObject;
            listOfItems.Add(shopItem);
           
            shopItem.GetComponent<ItemContent>().itemCode = itemArray[i];
            shopItem.GetComponent<ItemContent>().SetItemInfo();
        }
    }
    public void SetWantBuyItems()
    {
        int sumOfPrice = 0;
        foreach(var i in wantBuyItems)
        {
            sumOfPrice += (ItemDB.instance.items[i.Key].GetComponent<Item>().itemPrice) * i.Value;
        }
        sumOfPriceTextBox.text = "All: " + sumOfPrice + "Gold";
        allItemPrice = sumOfPrice;
    }
    public void PlusItem(int itemCode)
    {
        if (wantBuyItems.ContainsKey(itemCode))
        {
            wantBuyItems[itemCode]++;
        }
        else
        {
            wantBuyItems.Add(itemCode, 1);
        }
        SetWantBuyItems();
    }
    public void MinusItem(int itemCode)
    {
        wantBuyItems[itemCode]--;
        SetWantBuyItems();
    }
    

    public void BuyItems()
    {
        if(DataMangaer.userData.haveMoney>= allItemPrice)
        {
            DataMangaer.userData.haveMoney -= allItemPrice;
            foreach (var i in wantBuyItems)
            {                
                DataMangaer.instance.AddItem(i.Key, i.Value);
            }            
            InGameUIManager.instance.UpdateGold();
            if (Inventory.instance != null)
            {
                Inventory.instance.SetItems();
            }            
            wantBuyItems.Clear();
            SetWantBuyItems();
            foreach(var i in listOfItems)
            {
                i.GetComponent<ItemContent>().amountOfItem = 0;
                i.GetComponent<ItemContent>().SetItemInfo();
            }
            InGameUIManager.instance.UpdateAllHotKeyInfo();
            
        }
        else
        {
            //can not buy items cuz not enough gold
        }
    }
}

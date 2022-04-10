using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumptionShopManager : MonoBehaviour
{
    //판매하는 아이템 목록
    int[] itemArray = new int[] {200,201 };
    public GameObject ItemLayout;
    public GameObject ItemContent;
    Dictionary<int, int> wantBuyItems;
    private void Awake()
    {
        wantBuyItems = new Dictionary<int, int>();
    }
    private void Start()
    {
        for(int i = 0; i < itemArray.Length; i++)
        {
            GameObject shopItem = Instantiate(ItemContent,ItemLayout.transform) as GameObject;
            
           
            shopItem.GetComponent<ItemContent>().itemCode = itemArray[i];
            shopItem.GetComponent<ItemContent>().SetItemInfo();
        }
    }
    public void SetWantBuyItems()
    {

    }

    public void BuyItems(int itemCode)
    {
        switch (itemCode)
        {

        }
    }
}

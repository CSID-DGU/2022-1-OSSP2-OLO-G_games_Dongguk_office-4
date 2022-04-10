using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionShopManager : MonoBehaviour
{
    Dictionary<int, int> wantBuyItems;
    private void Awake()
    {
        wantBuyItems = new Dictionary<int, int>();
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

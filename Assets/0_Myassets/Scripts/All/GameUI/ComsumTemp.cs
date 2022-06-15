using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ComsumTemp : MonoBehaviour
{
    int wantBuyHpPotion;
    int wantBuyMpPotion;
    int hpPotionPrice = 5;
    int mpPotionPrice = 10;
    int allPrice;
    public TMP_Text wantBuyHpAmountText;
    public TMP_Text wantBuyMpAmountText;
    public TMP_Text allPriceText;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddHpPotion()
    {
        if (allPrice + hpPotionPrice <= DataMangaer.instance.userData.haveMoney)
        {
            wantBuyHpPotion++;
            allPrice = allPrice + hpPotionPrice;
            SetUi();
        }
    }
    public void DecreaseHpPotion()
    {
        if (wantBuyHpPotion > 0)
        {
            wantBuyHpPotion--;
            allPrice = allPrice - hpPotionPrice;
            SetUi();
        }
    }

    public void AddMpPotion()
    {
        if (allPrice + mpPotionPrice <= DataMangaer.instance.userData.haveMoney)
        {
            wantBuyMpPotion++;
            allPrice = allPrice + mpPotionPrice;
            SetUi();
        }
    }

    public void DecreaseMpPotion()
    {
        if (wantBuyMpPotion > 0)
        {
            wantBuyMpPotion--;
            allPrice = allPrice - mpPotionPrice;
            SetUi();
        }
    }

    public void Purchase()
    {
        DataMangaer.instance.userData.haveHpAmount += wantBuyHpPotion;
        DataMangaer.instance.userData.haveMpAmount += wantBuyMpPotion;
        DataMangaer.instance.userData.haveMoney -= allPrice;
        wantBuyHpPotion = 0;
        wantBuyMpPotion = 0;
        allPrice = 0;
        InGameUIManager.instance.UpdateGold();
        InGameUIManager.instance.UpdatePotionUi();
        DataMangaer.instance.saveData();
        SetUi();
    }
    void SetUi()
    {
        wantBuyHpAmountText.text = "X" + wantBuyHpPotion;
        wantBuyMpAmountText.text = "X" + wantBuyMpPotion;
        allPriceText.text = "All: " + allPrice + "Gold";
    }

    
}

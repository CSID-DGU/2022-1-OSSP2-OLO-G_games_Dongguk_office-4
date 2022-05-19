using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text money_text;
    public Image hpBarImage;
    public Image mpBarImage;
    public TMP_Text hp_text;
    public TMP_Text mp_text;
    public GameObject inventoryPanel;

    public static InGameUIManager instance;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateGold()
    {
        money_text.text= DataMangaer.instance.userData.haveMoney.ToString();
    }

    public void PopUpPanel(GameObject panel)
    {
        panel.SetActive(true);
        
    }
    public void PopUpInventory()
    {
        inventoryPanel.SetActive(true);
        InventoryManager.instance.UpdateEquipInventory();
    }
    
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void UpdateStatUI()
    {
        int maxHp = DataMangaer.instance.gameStat.maxHp;
        int maxMp = DataMangaer.instance.gameStat.maxMp;
        int nowHp = DataMangaer.instance.gameStat.nowHp;
        int nowMp = DataMangaer.instance.gameStat.nowMp;
        hp_text.text = $"{nowHp}/{maxHp}";
        mp_text.text = $"{nowMp}/{maxMp}";
        hpBarImage.fillAmount = (float)nowHp / maxHp;
        mpBarImage.fillAmount = (float)nowMp / maxMp;
    }
}

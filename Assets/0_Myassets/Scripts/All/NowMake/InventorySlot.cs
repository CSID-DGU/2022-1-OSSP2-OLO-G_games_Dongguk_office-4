using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public EquipData equipData;

    public Image itemImage;
    public Text itemCount;
    public GameObject equipText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickInventorySlot()
    {
        if (equipData != null)
        {
            EquipData temp = DataMangaer.instance.userData.equipInventory.Find(x => x == equipData);
            
            foreach(var i in DataMangaer.instance.userData.equipInventory.FindAll(x => x.itemType == equipData.itemType))
            {
                i.isNowEquip = false;
            }
            temp.isNowEquip = true;
            DataMangaer.instance.saveData();
            DataMangaer.instance.nowEquipData.LoadData();
            DataMangaer.instance.UpdateStat();
            InventoryManager.instance.UpdateEquipInventory();
        }
    }
    
}

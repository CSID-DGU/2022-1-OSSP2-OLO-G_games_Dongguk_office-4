using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject slotPrefab;
    

    public GameObject[] inventoryPanels;



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


    }
    public void OpenPanel(int panelIndex)
    {
        for(int i = 0; i < inventoryPanels.Length; i++)
        {
            inventoryPanels[i].SetActive(false);
        }
        inventoryPanels[panelIndex].SetActive(true);
    }
    public void UpdateInventory()
    {
        UpdateEquipInventory();
    }
    public void AddEquipToInventory(EquipData data)
    {
        if (inventoryPanels[0].activeInHierarchy)
        {
            GameObject go = Instantiate(slotPrefab, inventoryPanels[0].transform) as GameObject;
            var sc = go.GetComponent<InventorySlot>();
            sc.itemCount.gameObject.SetActive(false);
            sc.equipText.SetActive(false);
            sc.equipData = data;
            if (data.isNowEquip)
            {
                sc.equipText.SetActive(true);
            }
            sc.itemImage.sprite = data.GetItemImage();
        }
        DataMangaer.instance.saveData();
    }
    public void UpdateEquipInventory()
    {
        for(int i = inventoryPanels[0].transform.childCount - 1; i >= 0; i--)
        {
            Destroy(inventoryPanels[0].transform.GetChild(i).gameObject);
        }
        foreach(var i in DataMangaer.instance.userData.equipInventory)
        {
            GameObject go = Instantiate(slotPrefab, inventoryPanels[0].transform) as GameObject;
            var sc = go.GetComponent<InventorySlot>();
            sc.itemCount.gameObject.SetActive(false);
            sc.equipText.SetActive(false);
            sc.equipData = i;
            if (i.isNowEquip)
            {
                sc.equipText.SetActive(true);
            }
            sc.itemImage.sprite = i.GetItemImage();            
        }
        
        
    }
    private void Start()
    {
        UpdateEquipInventory();
    }


}

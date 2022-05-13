using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    

    public GameObject[] inventoryPanels;

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

    }
   

}

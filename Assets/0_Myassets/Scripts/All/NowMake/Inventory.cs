using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InventoryType { Equip, Cosume }
public class Inventory : MonoBehaviour
{
    public InventoryType type;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateItemList()
    {
        switch (type)
        {
            case InventoryType.Equip:
                //DataMangaer.instance.userData
                break;
            case InventoryType.Cosume:
                break;
        }
    }
    public void OnClickItem()
    {
        switch (type)
        {
            case InventoryType.Equip:
                break;
            case InventoryType.Cosume:
                break;
        }
    }
}

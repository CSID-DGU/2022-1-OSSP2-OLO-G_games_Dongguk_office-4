using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class DroppedEquipment : MonoBehaviour
{
    public EquipData data;

  
    public void GetItem()
    {
        if (!DataMangaer.instance.isEquipInventoryFull())
        {
            Debug.Log(data.itemName + "획득");
            InventoryManager.instance.AddEquipToInventory(data);
            DataMangaer.instance.saveData();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("인벤토리 포화");
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            GetItem();
        }
    }
}

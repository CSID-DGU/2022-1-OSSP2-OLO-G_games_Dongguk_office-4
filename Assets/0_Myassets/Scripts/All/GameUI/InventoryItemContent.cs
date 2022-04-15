using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InventoryItemContent : MonoBehaviour
{
    public Action onClickAction;
    public int itemCode;
    // Start is called before the first frame update
    public Image itemImage;
    public Text amoutOfItemText;

    public void OnItemClick()
    {
        Debug.Log("inventory item content click");
        InGameUIManager.instance.nowSelectedItemCode = itemCode;
        onClickAction?.Invoke();
    }

    
}

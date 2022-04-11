using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InventoryItemContent : MonoBehaviour
{
    public Action onClickAction;
    // Start is called before the first frame update
    public Image itemImage;
    public Text amoutOfItemText;

    public void OnItemClick()
    {
        onClickAction?.Invoke();
    }

    
}

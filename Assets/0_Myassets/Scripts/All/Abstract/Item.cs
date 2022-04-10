using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Item:MonoBehaviour
{
    //0:무기,  1:방어구,   2:소비

    public int itemCode = -1;
    public int ItemType = -1;
    public string ItemName = null;
    public Sprite itemImage;
    public int itemPrice;
   
    public abstract void OnClick();
   
}

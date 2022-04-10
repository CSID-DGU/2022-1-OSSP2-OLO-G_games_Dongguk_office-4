using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Item:MonoBehaviour
{
    //0:����,  1:��,   2:�Һ�

    public int itemCode = -1;
    public int ItemType = -1;
    public string ItemName = null;
    public Sprite itemImage;
   
    public abstract void OnClick();
   
}

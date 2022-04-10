using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Item:MonoBehaviour
{
    //0:公扁,  1:规绢备,   2:家厚

    public int itemCode = -1;
    public int ItemType = -1;
    public string ItemName = null;
    public Sprite itemImage;
   
    public abstract void OnClick();
   
}

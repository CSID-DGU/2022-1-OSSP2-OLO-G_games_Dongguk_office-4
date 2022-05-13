using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


enum ItemType
{
    //소비:0,무기:1,머리:2,몸통:3,다리:4
    Consume,Weapon,Head,Body,Leg
}

[System.Serializable]
public abstract class Item
{
    
    
    public int itemCode;
   
    public string ItemName;
    public Sprite itemImage;
    public int itemPrice;
    public string description;
   
    


   
  
}

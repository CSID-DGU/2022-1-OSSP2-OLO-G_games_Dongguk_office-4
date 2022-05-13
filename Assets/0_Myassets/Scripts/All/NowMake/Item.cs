using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    public string itemName; //아이템 이름
    public string spriteName; //아이템 이미지 이름

    

    public Sprite GetItemSprite()
    {
        var sp = Resources.Load("ItemSprite/"+spriteName) as Sprite;
        return sp;
    }
   
    public void Use()
    {
        
    }
}

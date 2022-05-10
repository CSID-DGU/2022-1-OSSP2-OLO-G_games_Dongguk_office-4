using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _200_HpPotion : Item
{
    public int hpRecoverAmount;
   
  
  
    private void Start()
    {
       
    }
   
    public override void OnClick()
    {
        Debug.Log("hp포션");
       
    }

    protected override void OnPickUpItem()
    {
        throw new System.NotImplementedException();
    }
}

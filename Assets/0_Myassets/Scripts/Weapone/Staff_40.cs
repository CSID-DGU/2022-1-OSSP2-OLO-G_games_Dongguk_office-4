using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff_40 : Gun
{
    protected override void Fire()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (DataMangaer.instance.gameStat.nowMp >= 10)
        {
            DataMangaer.instance.gameStat.nowMp -= 10;
            InGameUIManager.instance.UpdateStatUI();
            base.Fire();
        }
      

        Debug.Log("fire");
    }

    protected override void Awake()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        base.Awake();
        bulletName = "StaffEffect";
        isNeedRotation = true;        
        fireRate = 1.2f;
    }

    protected override void UpdateFunc()
    {
     
    }

   
   
}

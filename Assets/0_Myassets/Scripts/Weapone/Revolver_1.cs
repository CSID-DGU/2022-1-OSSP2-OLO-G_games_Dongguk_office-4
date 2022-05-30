using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_1 : Gun
{
    protected override void Fire()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        base.Fire();
        Debug.Log("fire");
    }

    protected override void Awake()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        base.Awake();
        bulletName = "Revolver_1_Bullet";
        fireRate = 0.5f;
    }

    protected override void UpdateFunc()
    {
     
    }

   
   
}

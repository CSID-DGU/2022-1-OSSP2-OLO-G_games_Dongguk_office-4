using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_1 : Gun
{
    protected override void Fire()
    {
        base.Fire();
        Debug.Log("fire");
    }

    protected override void SetValues()
    {
        damage = 1;
        fireRate = 1.0f;
    }

    protected override void UpdateFunc()
    {
     
    }

   
   
}

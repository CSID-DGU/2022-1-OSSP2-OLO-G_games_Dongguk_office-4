using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver_1_bullet : Bullet
{
    
    protected override void OnHit()
    {
        
    }

    protected override void SetValues()
    {
        bulletSpeed = 5.0f;
    }
}

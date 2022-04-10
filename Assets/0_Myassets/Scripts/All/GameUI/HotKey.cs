using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HotKey : MonoBehaviour
{

    public Action myDele;
   
    public void HotKeyButton()
    {
       
        myDele?.Invoke();
        
    }
  
   

   
}

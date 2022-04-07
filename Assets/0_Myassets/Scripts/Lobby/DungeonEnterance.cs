using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnterance : Enterance
{
    protected override void EnterAction()
    {      
        Debug.Log("???? ????");
        LobbyManager.instance.AskEnterDungeon();
    }

   
}

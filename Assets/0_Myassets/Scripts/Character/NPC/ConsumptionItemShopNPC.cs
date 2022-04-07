using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionItemShopNPC : NPC
{
    
    public override void OnRaycastTargeted()
    {
        if (isInLobby)
        {
            Debug.Log("Shop");
            InGameUIManager.instance.PopUpPanel(LobbyManager.instance.consumptionItemShopPanel);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

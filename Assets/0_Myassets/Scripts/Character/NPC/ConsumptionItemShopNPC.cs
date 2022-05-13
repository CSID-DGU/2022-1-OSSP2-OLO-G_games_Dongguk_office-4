using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionItemShopNPC : NPC
{
    int npcID = 0;
    public override void OnRaycastTargeted()
    {
        if (DataMangaer.instance.isInLobby)
        {
            //if npc in lobby it active as shop
            Debug.Log("Shop");
            
        }
        else
        {
            //save npc
            Debug.Log("Save Npc");
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

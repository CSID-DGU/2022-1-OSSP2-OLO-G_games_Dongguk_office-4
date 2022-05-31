using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionItemShopNPC : NPC
{
    int npcID = 0;
    public bool isInLobby;
    bool isSavedNPC;
    public override void OnRaycastTargeted()
    {
        if (isInLobby)
        {
          
            //if npc in lobby it active as shop
            Debug.Log("Shop");
            LobbyManager.instance.PopUpPanel(LobbyManager.instance.consumptionItemShopPanel);
        }
    
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DataMangaer.instance.userData.savedNpcList.Contains(npcID))
        {
            isSavedNPC = true;
        }
        else
        {
            isSavedNPC=false;
        }
        if (isInLobby)
        {
            if (!isSavedNPC)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (isSavedNPC)
            {
                Destroy(gameObject);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CharacterHitBox"&&!isInLobby)
        {
            //save npc
            Debug.Log("Save Npc");
            DataMangaer.instance.userData.savedNpcList.Add(npcID);
            DataMangaer.instance.saveData();
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LobbyManager : MonoBehaviour
{
    

    public static LobbyManager instance;
    public GameObject dungeonEnteranceAskPanel;//?????????????? ???? ????
    public Stack<GameObject> panelStack;
    public GameObject consumptionItemShopPanel;
    public CharacterController characterController;
    public GameObject character;
    public GameObject characterHand;



    public int connectedRoomUserCounter;



    private void Awake()
    {
        characterController.Character = character;
        characterController.hand = characterHand;
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }
        
        panelStack = new Stack<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        DataMangaer.instance.isInLobby = true;           
    }

    // Update is called once per frame
    void Update()
    {
       
        


    }
    public void AskEnterDungeon()
    {
        InGameUIManager.instance.PopUpPanel(dungeonEnteranceAskPanel);
    }
    [PunRPC]
    public void Ready()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {

        }
    }


    
   
    
    
}

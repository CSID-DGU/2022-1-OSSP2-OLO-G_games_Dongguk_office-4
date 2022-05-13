using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    

    public static LobbyManager instance;
    public GameObject dungeonEnteranceAskPanel;//?????????????? ???? ????
    public Stack<GameObject> panelStack;
    public GameObject consumptionItemShopPanel;
    public CharacterController characterController;

    public GameObject makeRoomPanel;
    public TMP_InputField makeRoomNameInputField;
    public GameObject waitingRoomPanel;



    public TMP_Text serverStatusText;


    public int connectedRoomUserCounter;



    private void Awake()
    {        
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
        //InGameUIManager.instance.PopUpPanel(dungeonEnteranceAskPanel);
    
    }

    public void ExitRoomEnterPanel()
    {
        dungeonEnteranceAskPanel.SetActive(false);
        
    }

    [PunRPC]
    public void Ready()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {

        }
    }

    public void PopUpWaitingRoomPanel()
    {
        if (waitingRoomPanel != null)
        {
            waitingRoomPanel.SetActive(true);
        }
        if (makeRoomPanel != null)
        {
            makeRoomPanel.SetActive(false);
        }
        if (dungeonEnteranceAskPanel != null)
        {
            dungeonEnteranceAskPanel.SetActive(false);

        }

    }
    
    public void PopUpMakeRoomPanelButton()
    {
        makeRoomPanel.SetActive(true);
    }
    public void CloseMakeRoomPanelButton()
    {
        makeRoomPanel.SetActive(false);
    }
    public void MakeRoom()
    {
        if (!string.IsNullOrEmpty(makeRoomNameInputField.text))
        {
            NetworkManager.instance.makeRoomByName(makeRoomNameInputField.text);
            makeRoomPanel.SetActive(false);
        }
        
    }





}

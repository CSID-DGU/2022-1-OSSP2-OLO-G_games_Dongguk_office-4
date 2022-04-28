using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
public class WaitingRoom : MonoBehaviour
{
    public static WaitingRoom instance;
    public TMP_Text ReadyText;
    public Dictionary<string,TMP_Text> userInfoDic;
    public TMP_Text[] joinedUserInfoTextList;

    public Button readyButton;
    public WaitingRoomUserInfo[] userInfoList;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        userInfoDic = new Dictionary<string, TMP_Text>();
        userInfoList = new WaitingRoomUserInfo[4];
        for(int i = 0; i < userInfoList.Length; i++)
        {

            userInfoList[i] = new WaitingRoomUserInfo();
            
            
            
        }
    }

    private void OnEnable()
    {
        
        if (Photon.Pun.PhotonNetwork.IsMasterClient)
        {
            ReadyText.text = "Start";
        }

    }
    public void SetWaitingRoomStatus()
    {       
        for(int i = 0; i < userInfoList.Length; i++)
        {
            if (!string.IsNullOrEmpty(userInfoList[i].userID))
            {
                joinedUserInfoTextList[i].text = userInfoList[i].userNick;
                joinedUserInfoTextList[i].gameObject.SetActive(true);
                if (userInfoList[i].isReady)
                {
                    joinedUserInfoTextList[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    joinedUserInfoTextList[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                joinedUserInfoTextList[i].text = "";
                joinedUserInfoTextList[i].transform.GetChild(0).gameObject.SetActive(false);
                joinedUserInfoTextList[i].gameObject.SetActive(false);
            }
        }
       

    }

    public void ExitRoom()
    {
        readyButton.interactable = true;
        NetworkManager.instance.exitRoom();
        //NetworkManager.instance.LeaveRoom();
        LobbyManager.instance.waitingRoomPanel.SetActive(false);
    }
    public void ReadyButton()
    {
        for(int i =0;i< userInfoList.Length;i++)
        {
            if(userInfoList[i].userID == Photon.Pun.PhotonNetwork.LocalPlayer.UserId)
            {
                DataMangaer.instance.inGameIndex = i;
            }
        }
        
        if (Photon.Pun.PhotonNetwork.IsMasterClient)
        {
            foreach(var i in userInfoList)
            {
                if (!string.IsNullOrEmpty(i.userID) && i.isReady == false&&i.userID!=Photon.Pun.PhotonNetwork.LocalPlayer.UserId)
                {
                    return;
                }
            }
            //loadLevel;
            NetworkManager.instance.LoadNetworkLevel("map1");
            Debug.Log("gameStart!!");
        }
        else
        {
            readyButton.interactable = false;
            NetworkManager.instance.GameReadyInWaitingRoom();
        }
        
        
    }
    
}

public class WaitingRoomUserInfo
{
    public string userID;
    public string userNick;
    
    public bool isReady = false;
}


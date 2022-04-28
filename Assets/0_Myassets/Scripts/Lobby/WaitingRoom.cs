using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WaitingRoom : MonoBehaviour
{
    public static WaitingRoom instance;
    public TMP_Text ReadyText;
    public Dictionary<string,TMP_Text> userInfoDic;
    public TMP_Text[] joinedUserInfoTextList;
    public Button readyButton;

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
        int index = 0;

        userInfoDic.Clear();
        foreach(var i in NetworkManager.instance.joinedPlayerList)
        {
            joinedUserInfoTextList[index].gameObject.SetActive(true);
            joinedUserInfoTextList[index].text = i.NickName;
            userInfoDic.Add(i.UserId, joinedUserInfoTextList[index++]);
            Debug.Log("added uid = " + i.UserId);
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
        readyButton.interactable = false;
        NetworkManager.instance.GameReadyInWaitingRoom();
        
    }
    
}


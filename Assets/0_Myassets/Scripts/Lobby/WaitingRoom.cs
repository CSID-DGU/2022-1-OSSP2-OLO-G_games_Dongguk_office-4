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
        foreach(var i in NetworkManager.instance.joinedPlayerList)
        {
            joinedUserInfoTextList[index].gameObject.SetActive(true);
            joinedUserInfoTextList[index].text = i.NickName;
            userInfoDic.Add(i.NickName, joinedUserInfoTextList[index++]);
        }

    }

    public void ExitRoom()
    {
        NetworkManager.instance.LeaveRoom();
        LobbyManager.instance.waitingRoomPanel.SetActive(false);
    }
    public void ReadyButton()
    {
        NetworkManager.instance.GameReadyInWaitingRoom();
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Newtonsoft.Json;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static NetworkManager instance;
    public GameObject roomListContent;
    public GameObject joinRoomButton;

    private string gameVersion = "1"; //???? ????

    public GameObject MyCharacter;
    public Photon.Realtime.Player[] joinedPlayerList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
       
    }
    private void Start()
    {
        PhotonNetwork.NickName = DataMangaer.instance.myNickName;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.AutomaticallySyncScene = true;
        
        ServerConnect();
        
    }
    public void ServerConnect()
    {
        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("ConnectUsingSettings");
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

       
        Debug.Log("OnConnectedToMaster");
        PhotonNetwork.JoinLobby();
        
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }
    public void Connect()
    {

    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
       
       
        
    }

    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
      
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        
        foreach(var i in roomList)
        {
            if (i.PlayerCount < 1)
            {
                for(int j = roomListContent.transform.childCount - 1; j >= 0; j--)
                {
                    if (roomListContent.transform.GetChild(j).GetComponent<JoinRoomButton>().roomName == i.Name)
                    {
                        Destroy(roomListContent.transform.GetChild(j).gameObject);
                    }
                }
            }
            else
            {
                bool isAlreadyShow = false;
                for (int j = 0; j < roomListContent.transform.childCount; j++)
                {
                    if (roomListContent.transform.GetChild(j).GetComponent<JoinRoomButton>().roomName == i.Name)
                    {
                        isAlreadyShow = true;
                        JoinRoomButton buttonScript = roomListContent.transform.GetChild(j).GetComponent<JoinRoomButton>();
                        buttonScript.roomName = i.Name;
                        buttonScript.maxPlayerCount = i.MaxPlayers;
                        buttonScript.nowPlayerCount = i.PlayerCount;
                        buttonScript.roomInfoText.text = i.Name + "\n" + i.PlayerCount.ToString() + "/" + i.MaxPlayers.ToString();
                    }
                }
                if (!isAlreadyShow)
                {
                    Debug.Log(i.Name + " : " + i.PlayerCount);
                    GameObject button = Instantiate(joinRoomButton, roomListContent.transform) as GameObject;
                    JoinRoomButton buttonScript = button.GetComponent<JoinRoomButton>();
                    buttonScript.roomName = i.Name;
                    buttonScript.maxPlayerCount = i.MaxPlayers;
                    buttonScript.nowPlayerCount = i.PlayerCount;
                    buttonScript.roomInfoText.text = i.Name + "\n" + i.PlayerCount.ToString() + "/" + i.MaxPlayers.ToString();
                }
               
            }
        }
       
       
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
     
    }

    public GameObject NetworkInstantiater(string name)
    {
        //???????? ?????? ???? ????
        GameObject obj = PhotonNetwork.Instantiate(name, Vector3.zero, Quaternion.identity) as GameObject;
        return obj;
    }

    public void makeRoomByName(string roomName)
    {
        
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4, EmptyRoomTtl = 0,PublishUserId=true });
    }

    public void joinRoomByName(string roomName)
    {
        //join room by name
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        joinedPlayerList = PhotonNetwork.PlayerList;
        LobbyManager.instance.PopUpWaitingRoomPanel();
        WaitingRoom.instance.SetWaitingRoomStatus();
        Debug.Log("join room by name success");
        photonView.RPC("SendMyInfoToMasterWhenJoinedRoom", RpcTarget.MasterClient,PhotonNetwork.LocalPlayer.UserId,PhotonNetwork.NickName);


    }
    
    [PunRPC]
    public void SendMyInfoToMasterWhenJoinedRoom(string uid, string nick)
    {
        for(int i = 0; i < WaitingRoom.instance.userInfoList.Length; i++)
        {
            if (string.IsNullOrEmpty(WaitingRoom.instance.userInfoList[i].userID))
            {
                WaitingRoom.instance.userInfoList[i].userID = uid;
                WaitingRoom.instance.userInfoList[i].userNick = nick;
                break;
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("UpdataRoomInfo", RpcTarget.All, JsonConvert.SerializeObject(WaitingRoom.instance.userInfoList));
        }
    }
    [PunRPC]
    public void UpdataRoomInfo(string infoListJsonData)
    {
        Debug.Log("jsonData = " + infoListJsonData);
        WaitingRoom.instance.userInfoList = JsonConvert.DeserializeObject<WaitingRoomUserInfo[]>(infoListJsonData);
        WaitingRoom.instance.SetWaitingRoomStatus();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
        
        
    }

    

  

    [PunRPC]
    public void UpdateCountOfPlayerInRoomInfo()
    {

        joinedPlayerList = PhotonNetwork.PlayerList;
        foreach (var i in joinedPlayerList)
        {
            Debug.Log(i.ActorNumber + "th player name:" + i.NickName);
        }
        
        WaitingRoom.instance.SetWaitingRoomStatus();
        
    }

    
    public void GameReadyInWaitingRoom()
    {
        photonView.RPC("setReadyStatisInWaitingRoom", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId);
    }

    [PunRPC]
    public void setReadyStatisInWaitingRoom(string uid)
    {
        foreach(var i in WaitingRoom.instance.userInfoList)
        {
            if (i.userID == uid)
            {
                i.isReady = true;
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("UpdataRoomInfo", RpcTarget.All, JsonConvert.SerializeObject(WaitingRoom.instance.userInfoList));
        }
    }
    public void exitRoom()
    {
        photonView.RPC("SendLeaveInfoToMaster", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.UserId);
        
        
        PhotonNetwork.LeaveRoom();
        foreach (var i in WaitingRoom.instance.userInfoList)
        {
            i.userID = "";
            i.userNick = "";
            i.isReady = false;
        }
    }
    [PunRPC]
    public void SendLeaveInfoToMaster(string uid)
    {
        foreach (var i in WaitingRoom.instance.userInfoList)
        {
            if (i.userID == uid)
            {
                i.userID = "";
                i.userNick = "";
                i.isReady = false;
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("UpdataRoomInfo", RpcTarget.All, JsonConvert.SerializeObject(WaitingRoom.instance.userInfoList));
        }
        
    }
    [PunRPC]
    public void exitPlayer(string playerName)
    {
        Debug.Log("user has been exited");
        //WaitingRoom.instance.userInfoDic[playerName].transform.GetChild(0).gameObject.SetActive(false);
        //WaitingRoom.instance.userInfoDic[playerName].gameObject.SetActive(false);
        //WaitingRoom.instance.userInfoDic.Remove(playerName);
    }

    public void LoadNetworkLevel(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

}

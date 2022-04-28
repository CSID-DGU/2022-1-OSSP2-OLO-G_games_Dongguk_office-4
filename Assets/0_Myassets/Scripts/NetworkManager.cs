using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


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
        Debug.Log("OnRoomListUpdated");
        for(int i = roomListContent.transform.childCount-1; i >= 0; i--)
        {
            Destroy(roomListContent.transform.GetChild(i).gameObject);
        }
        foreach(var i in roomList)
        {            
            if (i.PlayerCount > 0)
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
        photonView.RPC("UpdateCountOfPlayerInRoomInfo", RpcTarget.Others);


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
    public void TestFunc()
    {
        Debug.Log("RPCTEst");
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
        photonView.RPC("setReadyStatisInWaitingRoom", RpcTarget.All, PhotonNetwork.LocalPlayer.UserId);
    }

    [PunRPC]
    public void setReadyStatisInWaitingRoom(string playerName)
    {
        WaitingRoom.instance.userInfoDic[playerName].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void exitRoom()
    {
        photonView.RPC("exitPlayer", RpcTarget.All, PhotonNetwork.LocalPlayer.UserId);
        PhotonNetwork.LeaveRoom();
        
    }
    [PunRPC]
    public void exitPlayer(string playerName)
    {
        Debug.Log("user has been exited");
        WaitingRoom.instance.userInfoDic[playerName].transform.GetChild(0).gameObject.SetActive(false);
        WaitingRoom.instance.userInfoDic[playerName].gameObject.SetActive(false);
        WaitingRoom.instance.userInfoDic.Remove(playerName);
    }


}

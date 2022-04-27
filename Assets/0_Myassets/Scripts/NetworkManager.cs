using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private string gameVersion = "1"; //???? ????

    public GameObject MyCharacter;
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
        PhotonNetwork.NickName = "aa";
        PhotonNetwork.GameVersion = gameVersion;       
        PhotonNetwork.ConnectUsingSettings();
        
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("?????? ?????? ??????");
        Debug.Log("???????? ??: " + PhotonNetwork.CountOfPlayersOnMaster);
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
        PhotonNetwork.JoinRandomRoom();
       
        
    }

    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.CreateRoom("room2", new RoomOptions { MaxPlayers = 4, EmptyRoomTtl = 0 });
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        foreach(var i in roomList)
        {
            Debug.Log(i.Name + " : " + i.PlayerCount);
        }
       
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
     
    }

    public GameObject characterNetworkInstantiater()
    {
        //???????? ?????? ???? ????

        return null;
    }

    public void joinRoomByName(string roomName)
    {
        //join room by name
        PhotonNetwork.JoinRoom(roomName);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("join room by name success");
        PhotonNetwork.LoadLevel("Map1");
        
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        
    }
    




}

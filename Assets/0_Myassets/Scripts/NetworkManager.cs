using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;

    private string gameVersion = "1"; //게임 버전

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
        Debug.Log("마스터 서버와 연결됨");
        Debug.Log("플레이어 수: " + PhotonNetwork.CountOfPlayersOnMaster);
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

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.CreateRoom("room2", new RoomOptions { MaxPlayers = 4, EmptyRoomTtl = 0 });
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.CreateRoom("room2", new RoomOptions { MaxPlayers = 4, EmptyRoomTtl = 0 });
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("Map1");
        Debug.Log("룸 조인 완료");
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
    
   


}

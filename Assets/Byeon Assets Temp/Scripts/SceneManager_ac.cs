using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SceneManager_ac : MonoBehaviourPunCallbacks
{
    void Start()
    {
        this.ConnectToServer();
    }

    void ConnectToServer()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.NickName = "Guest " + Random.Range(0, 100);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();

            Debug.Log("Connect Attempt!");
        }
    }

    override public void OnConnectedToMaster()
    {
        Debug.Log("Connected!");

        PhotonNetwork.JoinOrCreateRoom("AC_ROOM", new RoomOptions { MaxPlayers = 5 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Character1", Vector3.zero, Quaternion.identity);
    }
}

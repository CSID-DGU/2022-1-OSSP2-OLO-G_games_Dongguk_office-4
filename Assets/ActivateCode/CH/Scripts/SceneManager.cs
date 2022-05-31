using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace ActiveCode.CH
{
    public class SceneManager : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            this.ConnectToServer();
        }

        // master 서버에 연결될 시 callback
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master!");

            this.ConnectToACRoom();
        }

        // 방에 들어갈 시 callback
        public override void OnJoinedRoom()
        {
            Debug.Log("Connected to room!");

            PhotonNetwork.Instantiate("Character1", Vector3.zero, Quaternion.identity);
        }

        // 서버(master)에 연결 or 이미 연결되어있으면 AC 방 들어감
        private void ConnectToServer()
        {
            if (PhotonNetwork.IsConnected)
            {
                this.ConnectToACRoom();
            }
            else
            {
                Debug.Log("Try to connect to server..");

                PhotonNetwork.NickName = "Guest " + Random.Range(0, 1000);
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.GameVersion = "1";
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        // AC_ROOM 방으로 들어감 (없으면 만듬)
        private void ConnectToACRoom()
        {
            PhotonNetwork.JoinOrCreateRoom("AC_ROOM", new RoomOptions { MaxPlayers = 4 }, null);
        }
    }
}

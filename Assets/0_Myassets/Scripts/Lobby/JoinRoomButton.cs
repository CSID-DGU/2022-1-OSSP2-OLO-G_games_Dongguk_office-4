using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class JoinRoomButton : MonoBehaviour
{
    public TMP_Text roomInfoText;
    public string roomName;
    public int maxPlayerCount;
    public int nowPlayerCount;
    // Start is called before the first frame update
    public void joinRoomButton()
    {
        if (nowPlayerCount < maxPlayerCount)
        {
            Debug.Log("join room");
            NetworkManager.instance.joinRoomByName(roomName);
        }
        else
        {
            Debug.Log("can not join room cuz nowPlayerCount >= maxPlayerCount");
        }
    }
}

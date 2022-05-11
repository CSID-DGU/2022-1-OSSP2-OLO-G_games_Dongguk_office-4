using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class Map_1_Manager : MonoBehaviour
{
    public Transform[] characterStartPositions;
    
    private void Awake()
    {
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }

    }
    void Start()
    {
        GameObject myCharacter = Photon.Pun.PhotonNetwork.Instantiate("Character1", Vector3.zero, Quaternion.identity, 0) as GameObject;
        myCharacter.transform.position = characterStartPositions[DataMangaer.instance.inGameIndex].position;
        BattleManager.instance.myCharacter = myCharacter;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

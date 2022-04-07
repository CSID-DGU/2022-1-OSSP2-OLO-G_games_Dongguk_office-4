using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;

public class Map_1_Manager : MonoBehaviour
{
    
    private void Awake()
    {
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }

    }
    void Start()
    {
        Photon.Pun.PhotonNetwork.Instantiate("Character1", new Vector3(0, 0, 0), Quaternion.identity, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

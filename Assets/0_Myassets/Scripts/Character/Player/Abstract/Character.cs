using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using Photon.Pun;

public abstract class Character : MonoBehaviourPun
{
   
    
   
    public GameObject hand;
    public bool isNeedRotation;

    protected void Awake()
    {
       

    }
    protected void Start()
    {
        if (!PhotonNetwork.InRoom)
        {
            DataMangaer.instance.myCharacter = this.gameObject;
            FollowingCamera.instance.targetCharacterTransform = this.transform;
            return;
        }
        if (!photonView.IsMine)
        {

            return;
        }
        else
        {
            DataMangaer.instance.myCharacter = this.gameObject;
        }

       
    }
   
    
    
    protected void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Debug.Log("test");
    }




    public abstract void SpecialBehavior();//???????????? ?????? ?????? ???? ????

    
}

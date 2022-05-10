using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public abstract class Item: MonoBehaviourPun
{
    //0:무기,  1:방어구,   2:소비
    public bool isOnGround = false;
    public int itemCode = -1;
   
    public string ItemName = null;
    public Sprite itemImage;
    public int itemPrice;
    public string description;
   
    public abstract void OnClick();

    public void PickUpItem()
    {
        OnPickUpItem();
        photonView.RPC("DestroyItem", RpcTarget.All);
    }

    [PunRPC]
    protected void DestroyItem()
    {
        Destroy(this.gameObject);
    }
    protected abstract void OnPickUpItem();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class DroppedItem : MonoBehaviourPun
{
    public int itemCode;
    public Sprite itemImage;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(itemCode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    abstract void PickUpItem();
    {
        Debug.Log("item" + itemCode.ToString() + " pick uped");
        photonView.RPC("DestroyItem", RpcTarget.All);
    }

    [PunRPC]
    protected void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}

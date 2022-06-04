using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class Gun : Weapone
{
    public Transform bulletPos;//?????? ???? ?? ????
    public GameObject bullet;//???? ??????
    public string bulletName;
    public bool isNeedRotation;
    protected override void Fire()
    {

        if (!photonView.IsMine)
        {
            return;
        }
        GameObject firedBullet = Photon.Pun.PhotonNetwork.Instantiate(bulletName, Vector3.zero, Quaternion.identity, 0) as GameObject;
        firedBullet.transform.position = bulletPos.position;
        firedBullet.transform.rotation = bulletPos.rotation;
        firedBullet.GetComponent<SpriteRenderer>().flipX = this.transform.parent.parent.transform.localScale.x > 0 ? true : false;
        //firedBullet.transform.localScale = this.transform.parent.parent.transform.localScale.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        firedBullet.GetComponent<Bullet>().SetTargetPosition(mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)));

        photonView.RPC("SetBulletOwner", RpcTarget.All, firedBullet.GetPhotonView().ViewID, nowUsingCharacter.GetPhotonView().ViewID);
      
        firedBullet.GetComponent<Bullet>().damage = DataMangaer.instance.gameStat.finalPhysicAtk+equipData.getDamage();
        firedBullet.GetComponent<Bullet>().atkType = AtkType.physic;//물리 공격임을 명시
        
    }



    [PunRPC]
    public void SetBulletOwner(int bulletID,int characterID)
    {
        PhotonView.Find(bulletID).GetComponent<Bullet>().ownerCharacter = PhotonView.Find(characterID).gameObject;
        PlayFireSound();
    }

    float angle;
    Vector2 target, mouse;
    protected override void Start()
    {
        base.Start();
        
    }
    protected override void Update()
    {
        base.Update();
      
        if (!photonView.IsMine)
        {
            return;
        }

        if (isNeedRotation)
        {
            target = nowUsingCharacter.transform.position;
            mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            if (this.transform.parent.parent.transform.localScale.x >= 0)
            {
                angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
            }
            else
            {
                angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg + 180;
            }



            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }
    

}

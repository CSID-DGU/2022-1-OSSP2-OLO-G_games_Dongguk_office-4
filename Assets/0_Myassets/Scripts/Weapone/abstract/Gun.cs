using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapone
{
    public Transform bulletPos;//?????? ???? ?? ????
    public GameObject bullet;//???? ??????
    public string bulletName;
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
        firedBullet.GetComponent<Bullet>().ownerCharacter = nowUsingCharacter;
        firedBullet.GetComponent<Bullet>().damage = DataMangaer.instance.gameStat.finalPhysicAtk+equipData.getDamage();
        firedBullet.GetComponent<Bullet>().atkType = AtkType.physic;//물리 공격임을 명시

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
    
        target = nowUsingCharacter.transform.position;
        mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,10.0f));
        if (this.transform.parent.parent.transform.localScale.x >= 0)
        {
            angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg+180;
        }
       
        
        
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
    

}

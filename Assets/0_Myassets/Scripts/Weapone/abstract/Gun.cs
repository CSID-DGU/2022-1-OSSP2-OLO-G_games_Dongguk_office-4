using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapone
{
    public Transform bulletPos;//?????? ???? ?? ????
    public GameObject bullet;//???? ??????
    protected override void Fire()
    {
        if (!DataMangaer.instance.isInLobby)
        {
            GameObject firedBullet = Instantiate(bullet) as GameObject;
            firedBullet.transform.position = bulletPos.position;
            firedBullet.transform.rotation = bulletPos.rotation;
            firedBullet.GetComponent<SpriteRenderer>().flipX = this.transform.parent.parent.transform.localScale.x > 0 ? true : false;
            //firedBullet.transform.localScale = this.transform.parent.parent.transform.localScale.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            firedBullet.GetComponent<Bullet>().SetTargetPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

}

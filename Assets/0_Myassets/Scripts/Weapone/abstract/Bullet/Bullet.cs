using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using ActiveCode.CH;
public abstract class Bullet : MonoBehaviourPun
{
    public GameObject ownerCharacter;//발사한 캐릭터
    [HideInInspector]
    public int damage;//데미지
    public float bulletSpeed;
    Vector3 targetPosition;//목표 위치
    Vector2 targetDirection;//타겟 방향

    public AtkType atkType;
    
  
   

 

    private void Awake()
    {
        if (!photonView.IsMine) return;

    }
    private void Start()
    {
        StartCoroutine(DestroyBulletByTimeCo());

    }
    IEnumerator DestroyBulletByTimeCo()
    {
        yield return new WaitForSecondsRealtime(20);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        this.transform.Translate(targetDirection*Time.deltaTime*bulletSpeed,Space.World);
            //(targetDirection * Time.deltaTime * bulletSpeed);
       
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        if (!photonView.IsMine) return;
        this.targetPosition = targetPosition;
        targetDirection = new Vector2( (targetPosition - this.transform.position).x, (targetPosition - this.transform.position).y).normalized;
        Debug.Log(targetDirection);
    }


  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
           
            Debug.Log("총알 적과 충돌");
            if (photonView.IsMine)
            {
                photonView.RPC("OnTriggerWithMonster", RpcTarget.All, collision.gameObject.GetComponent<Monster>().photonView.ViewID, damage);
         
                
            }
            
          
        }
        
    }

    [PunRPC]
    public void OnTriggerWithMonster(int monsterID,int damage)
    {
        GameObject hitMonster = PhotonView.Find(monsterID).gameObject;
        Monster monsterSC = hitMonster.GetComponent<Monster>();
        monsterSC.DecreaseHp(damage);
        if (PhotonNetwork.IsMasterClient)
        {
            if (monsterSC.target == monsterSC.spawnerPosition.gameObject)
            {
                monsterSC.target = ownerCharacter;
            }            
        }        
        Destroy(this.gameObject);
    }
    
 

}

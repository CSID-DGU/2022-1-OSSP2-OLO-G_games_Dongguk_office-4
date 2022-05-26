using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Bullet : MonoBehaviour
{

    int damage;//데미지
    protected float bulletSpeed;
    Vector3 targetPosition;//목표 위치
    Vector2 targetDirection;//타겟 방향
   

    protected abstract void OnHit();//총알이 닿은 경우

    private void Awake()
    {
        SetValues();
    }
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(targetDirection*Time.deltaTime*bulletSpeed,Space.World);
            //(targetDirection * Time.deltaTime * bulletSpeed);
       
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        targetDirection = new Vector2( (targetPosition - this.transform.position).x, (targetPosition - this.transform.position).y).normalized;
        Debug.Log(targetDirection);
    }
    protected abstract void SetValues();

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            Debug.Log("총알 적과 충돌");
          
        }
        
    }
}

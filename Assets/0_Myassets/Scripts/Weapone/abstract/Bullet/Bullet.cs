using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    int damage;//데미지
    protected float bulletSpeed;
    Vector3 targetPosition;//목표 위치
    Vector2 targetDirection;//타겟 방향
    bool isLaser;//총알이 목표위치까지만 가는지(false), 아니면 어떠한 물체를 만날때 까지 가는지(true)

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
        if (isLaser)
        {

        }
    }
    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        targetDirection = new Vector2( (targetPosition - this.transform.position).x, (targetPosition - this.transform.position).y).normalized;
        Debug.Log(targetDirection);
    }
    protected abstract void SetValues();
}

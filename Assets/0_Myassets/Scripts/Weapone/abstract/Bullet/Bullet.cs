using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    int damage;//������
    protected float bulletSpeed;
    Vector3 targetPosition;//��ǥ ��ġ
    Vector2 targetDirection;//Ÿ�� ����
    bool isLaser;//�Ѿ��� ��ǥ��ġ������ ������(false), �ƴϸ� ��� ��ü�� ������ ���� ������(true)

    protected abstract void OnHit();//�Ѿ��� ���� ���

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

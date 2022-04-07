using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Enterance : MonoBehaviour
{
    float enterCooltime = 1.0f;//�����̽��� �Է°��� ����
    float timeCounter = 0;
    private void Update()
    {
        timeCounter += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space)&&timeCounter>enterCooltime)
        {                   
            if (collision.tag == "Character")
            {
                timeCounter = 0;
                EnterAction();
            }
        }
    }
    protected abstract void EnterAction();
    
}

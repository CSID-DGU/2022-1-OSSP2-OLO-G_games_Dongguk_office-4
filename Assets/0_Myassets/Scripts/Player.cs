using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float rollTime = 0.1f;
    float rollAccelerateMagnification = 3.5f;
    float speed = 2.4f;
    const float rollCooltime = 2.0f;
    float rollCooltimeCounter = 2.0f;
    public GameObject characterSprite;
    int left, right, up, down;
    Vector3 characterMovePos;
    bool isCharacterCanMove;
    

    void Start()
    {
        //for test, character always can move
        isCharacterCanMove = true;
    }
    void Update()
    {
        rollCooltimeCounter += Time.deltaTime;
        

    }
    private void FixedUpdate()
    {
        //캐릭터가 움직이다 물리 판정이 생길 수 있으니, fixedupdate에서 이동 처리
        left = 0; right = 0; up = 0; down = 0;

        if (Input.GetKey(KeyCode.A))
        {
            left = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            up = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            down = -1;
        }
       
        characterMovePos = new Vector3(left + right, up + down, 0).normalized;
        if (isCharacterCanMove)
        {
            if (Input.GetKey(KeyCode.Space)&&rollCooltimeCounter>rollCooltime)
            {
                rollCooltimeCounter = 0;
                StartCoroutine(PlayerRollCo());
            }
            else
            {
                this.transform.Translate(speed * Time.fixedDeltaTime * characterMovePos);
            }            
        }
       

    }

    IEnumerator PlayerRollCo()
    {
        Debug.Log("캐릭터 구르기");
        isCharacterCanMove = false;
        float rolledTime = 0;
        Vector3 rollStartCharacterDirection = characterMovePos;
        while (true)
        {            
            yield return new WaitForFixedUpdate();
            rolledTime += Time.fixedDeltaTime;
            if (rolledTime > rollTime)
            {
                break;
            }
            else
            {
                transform.Translate(speed * Time.fixedDeltaTime * rollAccelerateMagnification * rollStartCharacterDirection);
                characterSprite.transform.Rotate(0, 0, 360/rollTime*Time.fixedDeltaTime);
            }
        }
        isCharacterCanMove = true;
        characterSprite.transform.eulerAngles = Vector3.zero;
    }
}

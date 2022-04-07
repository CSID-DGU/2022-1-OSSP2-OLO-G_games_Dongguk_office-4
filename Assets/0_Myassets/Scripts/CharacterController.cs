using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Character;
    public GameObject hand;

    int left, right, up, down;//이동 관련 변수
    Vector3 characterMovePos;
    private void Awake()
    {
        foreach(var i in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (i.GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                Character = i;
                hand = i.transform.Find("Hand").gameObject;
                return;
            }            
        }
    }
    private void Start()
    {
        //Character = this.gameObject;
        DontDestroyOnLoad(this);
    }

    private void FixedUpdate()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            CharacterMoveController();
            handRotationController();
        }
        else
        {
            return;
        }
        
    }
    void CharacterMoveController()
    {
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


        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > Character.transform.position.x)
        {
            offset = 0;
            Character.transform.localScale = new Vector3(1, 1, 1);
            if ((left + right) > 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
            }
            else if ((left + right) < 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
            }
        }
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < Character.transform.position.x)
        {
            offset = 180;
            Character.transform.localScale = new Vector3(-1, 1, 1);
            if ((left + right) > 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
            }
            else if ((left + right) < 0)
            {
                Character.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
            }
        }



        characterMovePos = new Vector3(left + right, up + down, 0).normalized;
        if (characterMovePos != Vector3.zero)
        {
            Character.GetComponent<Animator>().SetBool("isMove", true);
        }
        else
        {
            Character.GetComponent<Animator>().SetBool("isMove", false);
        }
        Character.transform.Translate(2.0f * Time.fixedDeltaTime * characterMovePos);
    }
    float offset = 0f;
    void handRotationController()
    {
        //hand.transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition),new Vector3(0,0,1));
        RotateTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private void RotateTowards(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        hand.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

}

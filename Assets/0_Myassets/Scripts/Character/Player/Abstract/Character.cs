using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using Photon.Pun;

public abstract class Character : MonoBehaviourPun
{
    public int nowSelectedWeapone;
    public GameObject[] nowEquipedWeapones; 
   
    public GameObject hand;
    public bool isNeedRotation;
    public float speed = 3;
    DataMangaer myData;
    protected void Awake()
    {
        

    }
    protected void Start()
    {
        Debug.Log("character Instantiated"+photonView.IsMine);
        if (!PhotonNetwork.InRoom)
        {
            DataMangaer.instance.myCharacter = this.gameObject;
            FollowingCamera.instance.targetCharacterTransform = this.transform;
            FollowingCamera.instance.gameObject.transform.position = this.transform.TransformPoint(new Vector3(0, 0, -10));
            return;
        }
        if (!photonView.IsMine)
        {

            return;
        }
        else
        {
            DataMangaer.instance.myCharacter = this.gameObject;
            FollowingCamera.instance.targetCharacterTransform = this.transform;
        }
        
       
    }
    protected void Update()
    {
        if (!photonView.IsMine) return;
        //when player input 'k' weapon swap.
        if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("swap weaspone");
            SwapWeapone();
        }
        CharacterMoveController();
    }
    protected void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

     
    }

    protected void SwapWeapone()
    {
        if (nowSelectedWeapone == 0)
        {
            nowSelectedWeapone = 1;
            if (nowEquipedWeapones[1] != null)
            {
                nowEquipedWeapones[1].SetActive(true);
            }
            else
            {
                return;
            }
            if (nowEquipedWeapones[0] != null)
            {
                nowEquipedWeapones[0].SetActive(false);
            }
            
        }
        else
        {
            nowSelectedWeapone = 0;
            if (nowEquipedWeapones[0] != null)
            {
                nowEquipedWeapones[0].SetActive(true);
            }
            else
            {
                return;
            }
            if (nowEquipedWeapones[1] != null)
            {
                nowEquipedWeapones[1].SetActive(false);
            }
        }


       
    }


    

    int left, right, up, down;
    float offset = 0f;
    Vector3 characterMovePos;
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


        if (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)).x > this.transform.position.x)
        {
            
            offset = 0;
            this.transform.localScale = new Vector3(1, 1, 1);
            if ((left + right) > 0)
            {
                this.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
            }
            else if ((left + right) < 0)
            {
                this.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
            }
        }
        else if (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f)).x < this.transform.position.x)
        {
           
            offset = 180;
            this.transform.localScale = new Vector3(-1, 1, 1);
            if ((left + right) > 0)
            {
                this.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
            }
            else if ((left + right) < 0)
            {
                this.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
            }
        }



        characterMovePos = new Vector3(left + right, up + down, 0).normalized;
        if (characterMovePos != Vector3.zero)
        {
            this.GetComponent<Rigidbody2D>().velocity = characterMovePos * speed;
            //this.GetComponent<Rigidbody2D>().AddForce(120 * characterMovePos * speed);
            this.GetComponent<Animator>().SetBool("isMove", true);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Animator>().SetBool("isMove", false);
        }
        
        //this.transform.Translate();
    }

    
    public abstract void SpecialBehavior();//???????????? ?????? ?????? ???? ????
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "DroppedWeapone")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("pick up item");
                
            }
        }
    }

}

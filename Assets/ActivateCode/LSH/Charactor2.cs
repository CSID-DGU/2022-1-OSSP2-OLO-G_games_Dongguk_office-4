//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using CodeStage.AntiCheat.ObscuredTypes;
//using Photon.Pun;

//public abstract class Character : MonoBehaviourPun
//{

//    private var status = new Dictionary<string, double>()
//    {
//        {"HP", 1000.0 },
//        {"MP", 1000.0 },
//        {"Attack", 10.0 },
//        {"Defense", 10.0 },
//        {"Speed", 1.0 }
//    };

//    public GameObject hand;
//    public bool isNeedRotation;

//    protected void Awake()
//    {


//    }
//    protected void Start()
//    {
//        Debug.Log("character Instantiated" + photonView.IsMine);
//        if (!PhotonNetwork.InRoom)
//        {
//            DataMangaer.instance.myCharacter = this.gameObject;
//            FollowingCamera.instance.targetCharacterTransform = this.transform;
//            FollowingCamera.instance.gameObject.transform.position = this.transform.TransformPoint(new Vector3(0, 0, -10));
//            return;
//        }
//        if (!photonView.IsMine)
//        {

//            return;
//        }
//        else
//        {
//            DataMangaer.instance.myCharacter = this.gameObject;
//            FollowingCamera.instance.targetCharacterTransform = this.transform;
//        }


//    }
//    protected void Update()
//    {

//    }

//    protected void FixedUpdate()
//    {
//        if (!photonView.IsMine)
//        {
//            return;
//        }
//        CharacterMoveController();

//    }

//    int left, right, up, down;
//    float offset = 0f;
//    Vector3 characterMovePos;
//    void CharacterMoveController()
//    {
//        left = 0; right = 0; up = 0; down = 0;



//        if (Input.GetKey(KeyCode.A))
//        {
//            left = -1 * status["Speed"];
//        }
//        if (Input.GetKey(KeyCode.D))
//        {
//            right = 1 * status["Speed"];
//        }
//        if (Input.GetKey(KeyCode.W))
//        {
//            up = 1 * status["Speed"];
//        }
//        if (Input.GetKey(KeyCode.S))
//        {
//            down = -1 * status["Speed"];
//        }


//        if (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f)).x > this.transform.position.x)
//        {

//            offset = 0;
//            this.transform.localScale = new Vector3(1, 1, 1);
//            if ((left + right) > 0)
//            {
//                this.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
//            }
//            else if ((left + right) < 0)
//            {
//                this.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
//            }
//        }
//        else if (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f)).x < this.transform.position.x)
//        {

//            offset = 180;
//            this.transform.localScale = new Vector3(-1, 1, 1);
//            if ((left + right) > 0)
//            {
//                this.GetComponent<Animator>().SetFloat("moveSpeed", -1.0f);
//            }
//            else if ((left + right) < 0)
//            {
//                this.GetComponent<Animator>().SetFloat("moveSpeed", 1.0f);
//            }
//        }



//        characterMovePos = new Vector3(left + right, up + down, 0).normalized;
//        if (characterMovePos != Vector3.zero)
//        {
//            this.GetComponent<Animator>().SetBool("isMove", true);
//        }
//        else
//        {
//            this.GetComponent<Animator>().SetBool("isMove", false);
//        }
//        this.transform.Translate(2.0f * Time.fixedDeltaTime * characterMovePos);
//    }


//    public abstract void SpecialBehavior();//???????????? ?????? ?????? ???? ????


//}

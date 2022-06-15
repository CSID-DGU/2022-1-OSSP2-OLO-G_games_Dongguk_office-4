using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Map_1_Manager : MonoBehaviourPun
{
    public GameObject gameClearPanel;
    public GameObject bossHpBar;
    public GameObject bpssHpGauge;
    public Transform[] characterStartPositions;
    public GameObject goldPrefab;
    public static Map_1_Manager instance;
    public GameObject InteractableGate;
    public List<GameObject> Map1Monsters;
    public bool isMap1Cleared;
    
    public void GameClear()
    {
        gameClearPanel.SetActive(true);
    }
    public void GoLobby()
    {
        PhotonNetwork.LeaveRoom();
        
        SceneManager.LoadScene("Lobby");


    }
    public void CheckAllMap1MonsterDead()
    {
        if (Map1Monsters.Count == 0)
        {
            InteractableGate.SetActive(false);
            isMap1Cleared = true;
        }
    }
    private void Awake()
    {
        isMap1Cleared = false;
        Map1Monsters = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }

    }
    GameObject myCharacter;
    GameObject myWeapon;
    void Start()
    {
        myCharacter = Photon.Pun.PhotonNetwork.Instantiate("Character1", Vector3.zero, Quaternion.identity, 0) as GameObject;
        myCharacter.transform.position = characterStartPositions[DataMangaer.instance.inGameIndex].position;
        BattleManager.instance.myCharacter = myCharacter;
        myCharacter.transform.Find("Arrow").gameObject.SetActive(true);

        setWeapon();
        //

        GameObject.FindGameObjectWithTag("InGameUI").transform.Find("InventoryButton").GetComponent<Button>().interactable = false;

       
    }

    void setWeapon()
    {
        if (DataMangaer.instance.nowEquipData.weapon != null)
        {
            myWeapon = Photon.Pun.PhotonNetwork.Instantiate(DataMangaer.instance.nowEquipData.weapon.itemName, Vector3.zero, Quaternion.identity, 0) as GameObject;
            photonView.RPC("WeaponParenting", RpcTarget.AllBuffered, myWeapon.GetPhotonView().ViewID, myCharacter.GetPhotonView().ViewID);
            myWeapon.GetComponent<Weapone>().equipData = DataMangaer.instance.nowEquipData.weapon;
        }
        
    }
    
    [PunRPC]
    void SetWeaponDefaultTransform()
    {
        myWeapon.transform.parent = myCharacter.GetComponent<Character>().hand.transform;
        myWeapon.transform.localPosition = Vector3.zero;

        myWeapon.GetComponent<Weapone>().nowUsingCharacter = myCharacter;
        Debug.Log("SetWeapon");

    }

    [PunRPC]
    void GitParent()
    {
      
        Debug.Log("RPC Test");
        this.gameObject.transform.parent = GameObject.Find("Base").transform;
       
    }

    [PunRPC]
    public void WeaponParenting(int child, int parent)
    {
        var thischild = PhotonView.Find(child);
        var thisparent = PhotonView.Find(parent);
        thischild.transform.parent = thisparent.transform.Find("Hand");
        thischild.transform.localPosition = Vector3.zero;
        thischild.GetComponent<Weapone>().nowUsingCharacter = thisparent.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}

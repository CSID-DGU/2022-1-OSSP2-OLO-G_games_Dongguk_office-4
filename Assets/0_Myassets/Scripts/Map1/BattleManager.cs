using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BattleManager : MonoBehaviourPun
{
    public static BattleManager instance;
    public GameObject myCharacter;
    int myHp;
    int myMp;
    public GameObject GameOverPanel;
    private void Awake()
    {
    
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
      
    }

    public void DecreaseCharacterHP(int amount)
    {
        if (amount > 0&&!myCharacter.GetComponent<Character>().isDead)
        {
            DataMangaer.instance.gameStat.nowHp -= amount;
            if (DataMangaer.instance.gameStat.nowHp <= 0&&!myCharacter.GetComponent<Character>().isDead)
            {
                DataMangaer.instance.gameStat.nowHp = 0;
                //myCharacter.GetComponent<Animator>().SetBool("IsDead", true);
                CharacterDie();


            }
        }
       
        InGameUIManager.instance.UpdateStatUI();
        
    }
    public void IncreaseCharacterHp(int amount)
    {
        DataMangaer.instance.gameStat.nowHp += 100;
        if (DataMangaer.instance.gameStat.nowHp > DataMangaer.instance.gameStat.maxHp)
        {
            DataMangaer.instance.gameStat.nowHp = DataMangaer.instance.gameStat.maxHp;
        }
    }
    public void IncreaseCharacterMp(int amount)
    {
        DataMangaer.instance.gameStat.nowMp += 50;
        if (DataMangaer.instance.gameStat.nowMp > DataMangaer.instance.gameStat.maxMp)
        {
            DataMangaer.instance.gameStat.nowMp = DataMangaer.instance.gameStat.maxMp;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (DataMangaer.instance.userData.haveHpAmount > 0)
            {
                DataMangaer.instance.userData.haveHpAmount--;
                IncreaseCharacterHp(100);
                InGameUIManager.instance.UpdatePotionUi();
                InGameUIManager.instance.UpdateStatUI();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (DataMangaer.instance.userData.haveMpAmount > 0)
            {
                DataMangaer.instance.userData.haveMpAmount--;
                IncreaseCharacterMp(50);
                InGameUIManager.instance.UpdatePotionUi();
                InGameUIManager.instance.UpdateStatUI();
            }
        }
    }
    public void PopUpYouDiePanel()
    {
        StartCoroutine(FadeInGameOverPanel());
    }
    IEnumerator FadeInGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        CanvasGroup gameOverPanelCG = GameOverPanel.GetComponent<CanvasGroup>();
        while (true)
        {
            yield return null;
            gameOverPanelCG.alpha += Time.deltaTime/3;
            if (gameOverPanelCG.alpha >= 1)
            {
                break;
            }
        }
    }
    public void CheckGameOver()
    {
        //?????? ????????? ???????????? ??????
        foreach(var character in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (character.GetComponent<Character>().isDead == false)
            {
                return;
            }
        }
        //?????? ???????????? ???????????? ??????
        GameOver();
    }
    public void GameOver()
    {
        //?????? ????????? ?????? ?????????
        //1/2????????? ????????? npc?????????
        DataMangaer.instance.ClearInventoryWhenDead();
        DataMangaer.instance.saveData();

        //???????????? ??? ?????????
        
        PopUpYouDiePanel();

    }
    public void CharacterDie()
    {
        myCharacter.GetComponent<Character>().isDead = true;
        photonView.RPC("PunCharacterDie", RpcTarget.All, myCharacter.GetPhotonView().ViewID);
       
        GameObject.FindGameObjectWithTag("InGameUI").transform.Find("InventoryButton").GetComponent<Button>().interactable = true;
       
   
    }
    [PunRPC]
    public void PunCharacterDie(int characterID)
    {
        GameObject character = PhotonView.Find(characterID).gameObject;
        character.GetComponent<Character>().isDead = true;
        character.GetComponent<Animator>().SetTrigger("Dead");

        character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Destroy(character.transform.Find("Hand").gameObject);
        CheckGameOver();
    }


    public void LoadLobbyScene()
    {
        PhotonNetwork.LeaveRoom();
        
        SceneManager.LoadScene("Lobby");
    }
}

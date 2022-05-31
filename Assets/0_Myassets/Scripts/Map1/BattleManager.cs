using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BattleManager : MonoBehaviourPun
{
    public static BattleManager instance;
    public GameObject myCharacter;
    int myHp;
    int myMp;
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

    public void CharacterDie()
    {
        myCharacter.GetComponent<Character>().isDead = true;
        photonView.RPC("PunCharacterDie", RpcTarget.All, myCharacter.GetPhotonView().ViewID);
        
    }
    [PunRPC]
    public void PunCharacterDie(int characterID)
    {
        GameObject character = PhotonView.Find(characterID).gameObject;
        character.GetComponent<Animator>().SetTrigger("Dead");

        character.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Destroy(character.transform.Find("Hand").gameObject);
    }

}

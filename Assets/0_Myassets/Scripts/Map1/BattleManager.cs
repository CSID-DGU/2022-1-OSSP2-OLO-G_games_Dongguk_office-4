using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BattleManager : MonoBehaviour
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



}

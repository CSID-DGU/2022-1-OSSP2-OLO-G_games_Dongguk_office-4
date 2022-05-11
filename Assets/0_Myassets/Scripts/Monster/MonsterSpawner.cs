using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ActiveCode.CH;

public class MonsterSpawner : MonoBehaviourPun
{
    [SerializeField]
    string monsterNameInResources;
    private void Awake()
    {
        
    }
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject monster = PhotonNetwork.Instantiate(monsterNameInResources, this.transform.position, Quaternion.identity, 0) as GameObject;
            monster.GetComponent<Monster>().spawnerPosition = this.transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

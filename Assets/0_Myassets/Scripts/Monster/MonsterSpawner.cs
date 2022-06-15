using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ActiveCode.CH;

public class MonsterSpawner : MonoBehaviourPun
{
    [SerializeField]
    string monsterNameInResources;

    public string area;
    private void Awake()
    {
        
    }
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject monster = PhotonNetwork.Instantiate(monsterNameInResources, this.transform.position, Quaternion.identity, 0) as GameObject;

            monster.GetComponent<Monster>().spawnerPosition = this.transform;
            if (area == "map1")
            {
                Map_1_Manager.instance.Map1Monsters.Add(monster);
            }

        }
    
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

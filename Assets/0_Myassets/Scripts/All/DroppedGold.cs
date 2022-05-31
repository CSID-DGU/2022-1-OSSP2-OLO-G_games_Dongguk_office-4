using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedGold : MonoBehaviour
{
    // Start is called before the first frame update
    public int goldAmount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CharacterHitBox")
        {
            DataMangaer.instance.AddGold(goldAmount);
            InGameUIManager.instance.UpdateGold();
            Destroy(gameObject);
        }
    }
}

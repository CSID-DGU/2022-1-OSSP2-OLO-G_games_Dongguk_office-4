using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : Item
{
    public int hpRecoverAmount;

    public GameObject key;
    private void Start()
    {
       
    }
    public void TestFunc()
    {
        Debug.Log("��������Ʈ ����");
    }
    public override void OnClick()
    {
        Debug.Log("hp����");
        key.GetComponent<HotKey>().myDele = TestFunc;
        key.GetComponent<HotKey>().myDele();
    }
}

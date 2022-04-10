using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataMangaer : MonoBehaviour
{
    public static DataMangaer instance;
    public static UserData userData;
    public GameObject testObj;
    public GameObject[] hotKeys;

    private void Awake()
    {
       
        userData = new UserData();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void DeleteAllPrefs()
    {
       
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("playerData"))
        {
            Debug.Log("저장된 데이터 없음");
            userData.inventory = new Dictionary<int, int>();
            for (int i = 0; i < userData.hotKeyItems.Length; i++)
            {
                userData.hotKeyItems[i] = -1;
            }            
            userData.haveMoney = 20;
            saveData();
        }
        loadData();

    }

    public void AddItem(int itemCode)
    {
        if (userData.inventory.ContainsKey(itemCode))
        {
            userData.inventory[itemCode]++;
        }
        else
        {
            userData.inventory.Add(itemCode, 1);
        }
        
        saveData();
    }
    public void ConsumItem(int itemCode)
    {
        //소비아이템일 경우
        userData.inventory[itemCode]--;
        if (userData.inventory[itemCode] <= 0)
        {
            userData.inventory.Remove(itemCode);
        }
        saveData();
    }
    public void EquipItem(int itemCode)
    {
        saveData();
    }


    public void saveData()
    {        
        Debug.Log(ObjectToJson(userData));
        PlayerPrefs.SetString("playerData", ObjectToJson(userData));
    }
    public void loadData()
    {
        Debug.Log("데이터 로드");
        userData = JsonToOject(PlayerPrefs.GetString("playerData"));
        Debug.Log(PlayerPrefs.GetString("playerData"));
        for (int i = 0; i < hotKeys.Length; i++)
        {
            if (userData.hotKeyItems[i] != -1)
            {
                hotKeys[i].GetComponent<HotKey>().SetHotKey(userData.hotKeyItems[i]);
                hotKeys[i].GetComponent<HotKey>().myDele = delTest;
            }            
        }
        InGameUIManager.instance.UpdateGold();
        
    }
    public void delTest()
    {
        Debug.Log("delTest");
    }
   
    string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj);
    }
    UserData JsonToOject(string jsonData)
    {
        return JsonUtility.FromJson<UserData>(jsonData);
    }
}


public class UserData
{
    public int haveMoney;
    public int characterMaxHp;
    public int characterMaxMp;

    public int characterBaseDamage;

    [SerializeField]
    public Dictionary<int,int> inventory = new Dictionary<int, int>();

    public int[] hotKeyItems = new int[4];  
   
}


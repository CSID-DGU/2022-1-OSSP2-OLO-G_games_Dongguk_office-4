using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;


public class DataMangaer : MonoBehaviour
{    
    public GameObject myCharacter;
    public int inGameIndex;
    public static DataMangaer instance;
    public static UserData userData;
    public GameObject testObj;
    public GameObject[] hotKeys;
    public string myNickName;

    public bool isInLobby = true;
    private void Awake()
    {
        Application.runInBackground = true;
        myNickName = PlayerPrefs.GetString("NickName");
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
        PlayerPrefs.DeleteAll();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("playerData"))
        {
            userData.selectedCharacterName = "Character1";
            Debug.Log("?????? ?????? ????");
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

    public void AddItem(int itemCode,int amount)
    {
        if (userData.inventory.ContainsKey(itemCode))
        {
            userData.inventory[itemCode]+=amount;
        }
        else
        {
            userData.inventory.Add(itemCode, amount);
        }
        
        saveData();
    }
    public void ConsumItem(int itemCode)
    {
        //???????????? ????
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
        Debug.Log("?????? ????");
        userData = JsonToOject(PlayerPrefs.GetString("playerData"));
        Debug.Log(PlayerPrefs.GetString("playerData"));
        for (int i = 0; i < hotKeys.Length; i++)
        {
            if (userData.hotKeyItems[i] != -1)
            {
                hotKeys[i].GetComponent<HotKey>().SetHotKey(userData.hotKeyItems[i]);
             
            }            
        }
        InGameUIManager.instance.UpdateGold();
        InGameUIManager.instance.UpdateAllHotKeyInfo();
        
        
    }
  
   
    string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }
    UserData JsonToOject(string jsonData)
    {
        return JsonConvert.DeserializeObject<UserData>(jsonData);
    }
    public void AddGold(int amount)
    {
        userData.haveMoney += amount;
        saveData();
        InGameUIManager.instance.UpdateGold();
    }
}

[System.Serializable]
public class UserData
{
    public string selectedCharacterName;
    public int haveMoney;
    public int characterMaxHp;
    public int characterMaxMp;
    public int characterNowHp;
    public int characterNowMp;
    public int characterNowEquipWeaspone;
    

    public int characterBaseDamage;

    
    public List<int> savedNpcList;
 
    public Dictionary<int,int> inventory = new Dictionary<int, int>();

    public int[] hotKeyItems = new int[4];  
   
}


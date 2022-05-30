using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;


public class DataMangaer : MonoBehaviour
{
    const int MaxInventoryItemCount = 16;
    public GameObject myCharacter;
    public int inGameIndex;//멀티플레이 몇번째 플레이언지 인덱스
    public static DataMangaer instance;

    public UserData userData;
    public GameObject testObj;

    public string myNickName;

    public InGameStat gameStat;//인게임에 적용될 스탯(최종 합산상태)
    public PlayerEquipData nowEquipData;//현재 착용 장비


    public bool isInLobby = true;
    private void Awake()
    {
        Application.runInBackground = true;
        myNickName = PlayerPrefs.GetString("NickName");
        userData = new UserData();
        gameStat = new InGameStat();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (!PlayerPrefs.HasKey("playerData"))
        {
            userData.selectedCharacterName = "Character1";

            userData.equipInventory = new List<EquipData>();
            userData.consumeInventory = new List<ConsumeData>();
            userData.stat = new CharacterStatData();
            userData.stat.initData();           
            userData.haveMoney = 20;
            saveData();
        }

        loadData();

        UpdateStat();

    }
    private void Start()
    {

    }
    public void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    public void UpdateStat()
    {
        //캐릭터 스탯과 장비 스탯 합산
        int finalStr = userData.stat.str + nowEquipData.GetAllAddStr();
        int finalDex = userData.stat.dex + nowEquipData.GetAllAddDex();
        int finalInt = userData.stat.intelligent + nowEquipData.GetAllAddInt();
        int finalLuck = userData.stat.luck + nowEquipData.GetAllAddLuck();

        //합산한 스탯 기준으로 인게임 수치 계산
        //가중치로 밸런스 조절
        gameStat.maxHp = userData.stat.baseHp + nowEquipData.GetAllAddHp() + (finalStr*5);
        gameStat.maxMp = userData.stat.baseMp + nowEquipData.GetAllAddMp() + (finalInt*3);
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name== "Lobby")
        {
            //로비에서만 장비 장착 시 스탯 만땅
            gameStat.nowHp = gameStat.maxHp;
            gameStat.nowMp = gameStat.maxMp;
        }
        else
        {
            //그외 스탯 올라가지 않고 최대치까지 낮추기
            if (gameStat.nowHp > gameStat.maxHp)
            {
                gameStat.nowHp = gameStat.maxHp;
            }
            if (gameStat.nowMp > gameStat.maxMp)
            {
                gameStat.nowMp = gameStat.maxMp;
            }
        }
        
        gameStat.finalPhysicAtk = nowEquipData.GetAllAddAtk()+(finalStr/2);
        gameStat.finalMagicAtk =  nowEquipData.GetAllAddMagic()+(finalInt*3);
        gameStat.finalAccuracyRate = 0.5f+(finalDex/100); //dex 50이면 무조건 적중
        gameStat.finalAvoidenceRate = finalLuck/100; //luck 100이면 무조건 회피
        InGameUIManager.instance.UpdateStatUI();

    }
   

    public void AddItem(int itemCode,int amount)
    {
        saveData();
    }
    public void ConsumItem(int itemCode)
    {
       
        saveData();
    }
    public void EquipDataItem(int itemCode)
    {
        saveData();
    }


    public void saveData()
    {
        Debug.Log("저장");
        Debug.Log(ObjectToJson(userData));
        PlayerPrefs.SetString("playerData", ObjectToJson(userData));
    }
    public void loadData()
    {       
        userData = JsonToOject(PlayerPrefs.GetString("playerData"));
        nowEquipData.LoadData();
        InGameUIManager.instance.UpdateGold();
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
        //InGameUIManager.instance.UpdateGold();
    }
    public void AddEquip(EquipData data)
    {
        if (!isEquipInventoryFull())
        {
            userData.equipInventory.Add(data);
        }
        else
        {
            Debug.Log("인벤토리 포화상태");
        }
        saveData();
    }
    public bool isEquipInventoryFull()
    {
        if(userData.equipInventory.Count< MaxInventoryItemCount)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

[System.Serializable]
public class UserData
{
    public string selectedCharacterName;//나중에 여러 캐릭터 사용을 위함
    public int haveMoney;//소지 재화

    public CharacterStatData stat;//캐릭터 기본 스탯
   
 
    public List<EquipData> equipInventory = new List<EquipData>();//장비 인벤토리
    public List<ConsumeData> consumeInventory = new List<ConsumeData>();//소비 아이템 인벤토리

    public List<int> savedNpcList;//구한 npc리스트
}

[System.Serializable]
public class CharacterStatData
{
    public int baseHp;
    public int baseMp;
    public int str;//물공 관련
    public int dex;//명중관련
    public int intelligent;//마공 관련
    public int luck;//회피 관련

    public void initData()
    {
        baseHp = 100;
        baseMp = 30;
        str = 10;
        dex = 10;
        intelligent = 10;
        luck = 10;
    }
    
    
}

[System.Serializable]
public class PlayerEquipData
{
    public EquipData weapon;
    public EquipData head;//머리
    public EquipData amor;//갑옷
    public EquipData ring;//반지

    public void LoadData()
    {
      
        weapon = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Weapon).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
        head = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Head).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
        amor = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Amor).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
        ring = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Ring).Where(equip => equip.getIsNowEquip() == true).FirstOrDefault();
        var test = DataMangaer.instance.userData.equipInventory.Where(equip => equip.itemType == ItemType.Head);
        foreach(var i in test)
        {
            Debug.Log(i.itemName);
            Debug.Log(i.getIsNowEquip());
            Debug.Log(i.addHp);
        }
        
    }
    
    public int GetAllAddHp()
    {
        //장비 hp옵션 합
        return (weapon?.addHp ?? 0 )+ (head?.addHp ?? 0) + (amor?.addHp ?? 0) + (ring?.addHp ?? 0);
    }
    public int GetAllAddMp()
    {
        //장비 mp옵션 합
        return (weapon?.addMp ?? 0) + (head?.addMp ?? 0) + (amor?.addMp ?? 0) + (ring?.addMp ?? 0);
        
    }
    public int GetAllAddStr()
    {
        //장비 str옵션 합
        return (weapon?.addStr ?? 0) + (head?.addStr ?? 0) + (amor?.addStr ?? 0) + (ring?.addStr ?? 0);
        
    }
    public int GetAllAddDex()
    {
        //장비 dex옵션 합
        return (weapon?.addDex ?? 0) + (head?.addDex ?? 0) + (amor?.addDex ?? 0) + (ring?.addDex ?? 0);
        
    }
    public int GetAllAddInt()
    {
        //장비 Int옵션 합
        return (weapon?.addIntelligent ?? 0) + (head?.addIntelligent ?? 0) + (amor?.addIntelligent ?? 0) + (ring?.addIntelligent ?? 0);
       
    }
    public int GetAllAddLuck()
    {
        return (weapon?.addLuck ?? 0) + (head?.addLuck ?? 0) + (amor?.addLuck ?? 0) + (ring?.addLuck ?? 0);
        
    }
    public int GetAllAddAtk()
    {
        return (weapon?.addAtk ?? 0) + (head?.addAtk ?? 0) + (amor?.addAtk ?? 0) + (ring?.addAtk ?? 0);
        
    }
    public int GetAllAddMagic()
    {
        return (weapon?.addMagic ?? 0) + (head?.addMagic ?? 0) + (amor?.addMagic ?? 0) + (ring?.addMagic ?? 0);
        
    }
}

[System.Serializable]
public class InGameStat
{
    public int maxHp;
    public int maxMp;
    public int finalPhysicAtk;
    public int finalMagicAtk;
    public float finalAvoidenceRate;
    public float finalAccuracyRate;
    public int nowHp;
    public int nowMp;
}
//public class 

//캐릭터 아이템 소유정보, 착용정보, 스탯 분리해서 저장



public enum AtkType
{
    physic, magic
}

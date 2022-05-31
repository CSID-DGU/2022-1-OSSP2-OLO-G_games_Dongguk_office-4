//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Achievement : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        SaveData save;
//        AchievementData achieve;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        checkAchievement()
//    }
//}

//public class AchievementData
//{
//    public int ScoreN { get; set; }
//    public int PlayN { get; set; }
//    public static int BoostCount = 0;
//}

//public class SaveData
//{
//    [SerializeField] public int MaxStage;
//    [SerializeField] public bool Stage_1;
//    [SerializeField] public bool Stage_3;
//    [SerializeField] public bool Stage_5;

//    [SerializeField] public bool unlockScoreN;
//    [SerializeField] public int pointScoreN;

//    [SerializeField] public bool unlockPlayN;
//    [SerializeField] public int pointPlayN;

//    public SaveData()
//    {
//        MaxStage = 0;
//        unlockScoreN = false;
//        pointScoreN = 0;
//        unlockPlayN = false;
//        pointPlayN = 0;
//    }
//}

//public void checkAchievement()
//{
//    if (gameManager.play)
//    {
//        checkMaxStage();
//        checkScoreN();
//        checkPlayN();
//        checkBoostN2InAGame();
//        if (hasModified)
//        {
//            hasModified = false;
//            SaveData();
//        }
//    }
//}

//public void checkPlayN()
//{
//    //도전과제 달성률 확인용
//    if (saveData.pointScoreN < saveData.pointScoreN)
//    {
//        saveData.pointScoreN = saveData.pointScoreN;
//    }
//    if (!saveData.unlockPlayN)
//    {
//        if (AchievementData.ScoreN <= saveData.pointScoreN)
//        {
//            Achieve(out saveData.unlockPlayN, "Play 100 Times");
//        }
//    }
//}

//public void checkScoreN()
//{
//    //도전과제 달성률 확인용
//    if (saveData.pointPlayN < saveData.pointPlayN)
//    {
//        saveData.pointPlayN = saveData.pointPlayN;
//    }
//    if (!saveData.unlockPlayN)
//    {
//        if (AchievementData.PlayN <= saveData.pointPlayN)
//        {
//            Achieve(out saveData.unlockPlayN, "Kill");
//        }
//    }
//}
//public void checkMaxStage()
//{
//    //도전과제 달성률 확인용
//    if (saveData.MaxStage < saveData.MaxStage)
//    {
//        saveData.MaxStage = saveData.MaxStage;
//    }
//    if (saveData.MaxStage == 1)
//    {
//        Achieve(out saveData.Stage_1, "Welcome to the world!");
//    }
//    if (saveData.MaxStage == 3)
//    {
//        Achieve(out saveData.Stage_3, "Eecape from Beginner");
//    }
//    if (saveData.MaxStage == 5)
//    {
//        Achieve(out saveData.Stage_5, "Approach to the boss");
//    }
//}

//public void Achieve(out bool achieve, string mention)
//{
//    achieve = true;
//    hasModified = true;
//    _EffectManager.Achieve();
//    _EffectManager.LockOff();
//    _EffectManager.setAchieveText(mention);
//    Debug.Log(mention);
//}

//public void AchieveQuiet(out bool achieve, string mention)
//{
//    achieve = true;
//    hasModified = true;
//    Debug.Log(mention);
//}

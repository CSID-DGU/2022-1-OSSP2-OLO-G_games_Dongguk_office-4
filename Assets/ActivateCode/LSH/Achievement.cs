//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Achievement : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
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
//    [SerializeField] public int MaxLevel;

//    [SerializeField] public bool unlockScoreN;
//    [SerializeField] public int pointScoreN;

//    [SerializeField] public bool unlockPlayN;
//    [SerializeField] public int pointPlayN;

//    public SaveData()
//    {
//        MaxLevel = 0;
//        PlayCount = 0;
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
//        checkMaxScore();
//        checkDefault();
//        checkScoreN();
//        checkPlayN();
//        checkBoostNInAGame();
//        checkScoreN2();
//        checkExactN();
//        checkNoBoostTillN();
//        checkScoreN3();
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
//            Achieve(out saveData.unlockPlayN, "Play 100 Times");
//        }
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

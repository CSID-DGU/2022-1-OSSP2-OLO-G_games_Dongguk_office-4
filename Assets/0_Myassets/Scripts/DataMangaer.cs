using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class DataMangaer : MonoBehaviour
{
    // Start is called before the first frame update

    
    public void saveData()
    {
        PlayerPrefs.SetString("playerData", "");
    }
    public void loadData()
    {
 
        PlayerPrefs.GetString("playerData");
        
    }
}


public static class UserData
{

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    public GameObject dungeonEnteranceAskPanel;//?????????????? ???? ????
    public Stack<GameObject> panelStack;
    public GameObject consumptionItemShopPanel;
    private void Awake()
    {
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }
        
        panelStack = new Stack<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
       
        


    }
    public void AskEnterDungeon()
    {
        InGameUIManager.instance.PopUpPanel(dungeonEnteranceAskPanel);
    }
    public void EnterOnlineDungeon()
    {
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeOut(nextSceneName: "map1");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("map1");
        }
    }
    
   
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;
    Canvas canvas;
    Stack<GameObject> panelStack;

    public GameObject inventoryPanel;
    private void Awake()
    {
        instance = this;
        panelStack = new Stack<GameObject>();
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneLoaded;
        canvas = this.gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

    }
    void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        //when scene loaded, find camera for canvas render
        canvas.worldCamera = Camera.main;


        //when scene loaded, clear panelStack
        panelStack.Clear();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }
    public void PopUpInventoryPanel()
    {
        PopUpPanel(inventoryPanel);
        inventoryPanel.GetComponent<Inventory>();
    }
    public void PopUpPanel(GameObject panel)
    {
        panel.SetActive(true);
        panelStack.Push(panel);
        Debug.Log(panelStack.Count);
    }
    public void ClosePanel(GameObject panel = null)
    {        
        if(panel == null)
        {
            if (panelStack.Count > 0)
            {
                panelStack.Pop().SetActive(false);                
            }            
        }
        else
        {
            if (panelStack.Contains(panel))
            {
                Stack<GameObject> temp = new Stack<GameObject>();
                while (panelStack.Count > 0)
                {
                    GameObject peekedPanel = panelStack.Pop();
                    if(peekedPanel == panel)
                    {
                        peekedPanel.SetActive(false);
                    }
                    else{
                        temp.Push(peekedPanel);   
                    }
                    while (temp.Count > 0)
                    {
                        panelStack.Push(temp.Pop());
                    }
                }
            }
        }
        
    }
    public void InventoryCloseBtn()
    {
        ClosePanel(inventoryPanel);
    }
    
}

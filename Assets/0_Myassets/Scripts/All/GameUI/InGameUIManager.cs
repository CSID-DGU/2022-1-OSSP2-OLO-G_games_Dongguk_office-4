using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager instance;
    Canvas canvas;
    Stack<GameObject> panelStack;
    public GameObject[] HotKeys;
    public TMP_Text[] HotKeyNumberText;

    public TMP_Text goldTextBox;

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
                GameObject peekedPanel;
                while (panelStack.Count > 0)
                {
                    peekedPanel = panelStack.Pop();
                    if(peekedPanel == panel)
                    {
                        peekedPanel.SetActive(false);
                    }
                    else{
                        temp.Push(peekedPanel);   
                    }
                   
                }
                while (temp.Count > 0)
                {
                    panelStack.Push(temp.Pop());
                }
            }
        }
        
    }
    public void InventoryCloseBtn()
    {
        ClosePanel(inventoryPanel);
    }

    public void UpdateGold()
    {
        goldTextBox.text = "X "+DataMangaer.userData.haveMoney.ToString();
    }
    public bool isHotKeyAllocating = false;
    public int nowSelectedItemCode = -1;
    public void StartBlinkHotKeyNumber()
    {
        
        foreach (var i in HotKeys)
        {
            i.GetComponent<HotKey>().myDele = i.GetComponent<HotKey>().SetHotKey;
        }
        
        if (!isHotKeyAllocating)
        {
            BlinkHotKeyCoroutine = StartBlinkHotKeyNumberCo();
            isHotKeyAllocating = true;
            StartCoroutine(BlinkHotKeyCoroutine);
        }
       
    }
    IEnumerator BlinkHotKeyCoroutine;
    IEnumerator StartBlinkHotKeyNumberCo()
    {     
        while (true)
        {
            foreach(var i in HotKeyNumberText)
            {
                i.gameObject.SetActive(true);           
            }
            yield return new WaitForSeconds(0.2f);
            foreach (var i in HotKeyNumberText)
            {
              

                i.gameObject.SetActive(false);
               
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void StopBlinkHotKeyNumber()
    {
        if (isHotKeyAllocating)
        {
            StopCoroutine(BlinkHotKeyCoroutine);
        }        
        
    }

    public void UpdateAllHotKeyInfo()
    {
        foreach(var i in HotKeys)
        {
            i.GetComponent<HotKey>().UpdateHotKeyInfo();
        }
    }



}

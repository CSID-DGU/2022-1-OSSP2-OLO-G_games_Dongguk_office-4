using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{
    public TMP_Text pressKeyToStart;
    bool hasNickName;
    public GameObject createNickNamePanel;
    public TMP_InputField nickNameInputField;
    private void Awake()
    {
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }
        StartCoroutine(BlankKeyToStartTextCo());
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("NickName"))
        {
            hasNickName = true;
        }
        else
        {
            createNickNamePanel.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.anyKey&&hasNickName)
        {
            FadeInOutManager.instance.FadeOut(nextSceneName: "Lobby");
        }
    }
    IEnumerator BlankKeyToStartTextCo()
    {
        while (true)
        {
            while (pressKeyToStart.alpha > 0)
            {
                pressKeyToStart.alpha -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            while (pressKeyToStart.alpha < 1)
            {
                pressKeyToStart.alpha += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        
    }
    public void CreateNick()
    {
        if (!string.IsNullOrEmpty(nickNameInputField.text)) {

            hasNickName = true;
            PlayerPrefs.SetString("NickName", nickNameInputField.text);
            createNickNamePanel.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{
    public TMP_Text pressKeyToStart;
    private void Awake()
    {
        if (FadeInOutManager.instance != null)
        {
            FadeInOutManager.instance.FadeIn();
        }
        StartCoroutine(BlankKeyToStartTextCo());
    }
    private void Update()
    {
        if (Input.anyKey)
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
}

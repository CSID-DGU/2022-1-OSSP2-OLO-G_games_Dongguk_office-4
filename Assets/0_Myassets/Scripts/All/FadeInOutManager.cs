using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOutManager : MonoBehaviour
{
    const float DEFAULT_FADETIME = 1.0f;
    public static FadeInOutManager instance;
    public GameObject fadePanel;
    public Image fadePanelImage;
    bool isFading = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }     
    }

    public void FadeIn(float fadeTime=DEFAULT_FADETIME, string fadecolor = "black")
    {
        if (!isFading)
        {
            isFading = true;
            SetFadePanelColor(fadecolor);
            StartCoroutine(FadeInCo(fadeTime));
        }
    }
    IEnumerator FadeInCo(float fadeTime)
    {
        fadePanel.SetActive(true);
        Color color = fadePanelImage.color;
        float startTime = 0;
        while (color.a > 0) {
            fadePanelImage.color = color;
            yield return new WaitForEndOfFrame();
            color.a -= Time.deltaTime/fadeTime;
        }
        fadePanel.SetActive(false);
        isFading = false;
    }
    public void FadeOut(float fadeTime=DEFAULT_FADETIME, string fadecolor = "black",string nextSceneName = null)
    {
        if (!isFading)
        {
            isFading = true;
            SetFadePanelColor(fadecolor);
            StartCoroutine(FadeOutCo(fadeTime, nextSceneName));
        }
        
    }
    IEnumerator FadeOutCo(float fadeTime,string nextSceneName)
    {
        AsyncOperation async = null;
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            async = SceneManager.LoadSceneAsync(nextSceneName,LoadSceneMode.Single);
            async.allowSceneActivation = false;
        }
        fadePanel.SetActive(true);
        Color color = fadePanelImage.color;
        color.a = 0;
        float startTime = 0;
        while (color.a < 1)
        {          
            fadePanelImage.color = color;
            yield return new WaitForEndOfFrame();
            color.a += Time.deltaTime / fadeTime;
        }
        isFading = false;
        if (async !=null)
        {
            async.allowSceneActivation = true;
        }           
    }
   
    void SetFadePanelColor(string fadecolor)
    {
        switch (fadecolor)
        {
            case "black":
                fadePanelImage.color = Color.black;
                break;
            case "white":
                fadePanelImage.color = Color.white;
                break;
        }
    }
}

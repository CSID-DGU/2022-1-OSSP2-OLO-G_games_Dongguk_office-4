using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInAndFadeOut());
    }
    IEnumerator FadeInAndFadeOut()
    {
        FadeInOutManager.instance.FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeInOutManager.instance.FadeOut(nextSceneName: "Title");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

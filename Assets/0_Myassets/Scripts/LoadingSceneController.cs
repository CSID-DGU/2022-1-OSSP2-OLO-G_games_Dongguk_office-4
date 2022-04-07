using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class LoadingSceneController : MonoBehaviour
{
    public Animator animator;
    static string nextScene;

    [SerializeField] Image progressBar;

    public static void LoadScene(string sceneName) {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess() {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        
        // ·Îµù ¹Ù
        float timer = 0f;
        while (!op.isDone)
        { 
            yield return null;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else 
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f) 
                {
                    animator.SetTrigger("FadeOut");
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
        
    }
}

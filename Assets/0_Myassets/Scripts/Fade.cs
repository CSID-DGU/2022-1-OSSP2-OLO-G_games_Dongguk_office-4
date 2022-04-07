using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public string NextScene;
    public void GotoLoading()
    {
        LoadingSceneController.LoadScene(NextScene);
    }
}

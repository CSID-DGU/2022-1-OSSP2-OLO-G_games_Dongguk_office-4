using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaycastManager : MonoBehaviour
{
    public Camera camera;
    float maxDistance = 15.0f;
    Vector3 mousePosition;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        camera = Camera.main;
        SceneManager.sceneLoaded += SceneLoaded;
    }
    void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        camera = Camera.main;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {          
            mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, transform.forward, maxDistance);
            if (hit)
            {
                NPC targetNPC;
                if (hit.transform.TryGetComponent<NPC>(out targetNPC)) {
                    Debug.Log("targeted npc : " + targetNPC.gameObject.name);
                    targetNPC.OnRaycastTargeted();
                }
                
                //hit.transform.TryGetComponent<>
            }
        }
    }
}

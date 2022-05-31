using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NullTest : MonoBehaviour
{
    public TMP_Text infoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        infoText.text = "";
        if (!InventoryManager.instance)
        {
            infoText.text += "inventory manager is null\n";
        }
        if (!InGameUIManager.instance)
        {
            infoText.text += "InGameUIManager is null\n";
        }
    }
}

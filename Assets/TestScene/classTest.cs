using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classTest : MonoBehaviour
{
    // Start is called before the first frame update
    List<testC1> listTest;
    void Start()
    {
        listTest = new List<testC1>();
        listTest.Add(new testC1());
        listTest.Add(new testC2());
        Debug.Log(((testC2)listTest[1]).val2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class testC1
{
    public int val1 = 3;
}

public class testC2:testC1
{
    public int val2 = 5;
}

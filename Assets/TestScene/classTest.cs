using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;


namespace TestSpace
{
    public class classTest : MonoBehaviour
    {
        // Start is called before the first frame update
        List<testC1> listTest;
        void Start()
        {
            /*
            listTest = new List<testC1>();
            listTest.Add(new testC1());
            listTest.Add(new testC2());
            //Debug.Log(((testC2)listTest[1]).val2);

            var a = from abc in listTest
                    where abc.val1 > 3
                    select abc;
            foreach(var i in a)
            {
                //Debug.Log(i.val3);
            }
            var b = listTest.Where(aaa => aaa.val1 > 3).OrderBy(aaa => aaa.val1).Select(aaa=>aaa);
            foreach (var i in b)
            {
                Debug.Log(i.val3);
            }
            */

            TestDataClass jsonTestData = new TestDataClass();
            TestDataClass1 newData = new TestDataClass1();
            newData.myList.Add(new TestData2());
            newData.myList.Add(new TestData2());
            jsonTestData.myListPart.Add(newData);
            jsonTestData.myListPart.Add(newData);
            string jsonData = JsonConvert.SerializeObject(jsonTestData);
            Debug.Log(jsonData);

            var deserializedData = JsonConvert.DeserializeObject<TestDataClass>(jsonData);
            Debug.Log(deserializedData.myListPart[1].myList[1].myDataInt);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public class testC1
    {
        public int val1 { get; set; } = 5;
        public int val3  = 3;
    }

    public class testC2 : testC1
    {
        public int val2 = 5;
    }


    public class TestDataClass 
    {
        public List<TestDataClass1> myListPart = new List<TestDataClass1>();
    }

    public class TestDataClass1
    {
        public List<TestData2> myList = new List<TestData2>();
    }
    public class TestData2
    {
        public int myDataInt = 3;
        public int testGetData()
        {
            return 3;
        }
    }
}

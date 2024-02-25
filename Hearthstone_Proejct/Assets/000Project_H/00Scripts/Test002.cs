using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test002 : Test001
{

    private void Awake()
    {
        TestID = 001;
        Num = 222;

        FindAnyObjectByType<Test003>().testDic.Add(TestID, this);
    }
  

    public override void Test0010()
    {
        Debug.Log("Is Override Method(Test002)");
    }


    public void Test002Method()
    {
        Debug.Log("Test002__Method");
    }

}

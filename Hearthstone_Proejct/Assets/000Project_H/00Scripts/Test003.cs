using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum testEnum
{
    Test001 = 001,
    Test002
}
public class Test003 : MonoBehaviour
{
    public Dictionary<int, Test001> testDic = new Dictionary<int, Test001>();

    
    void Start()
    {
        StartCoroutine(TestC((int)testEnum.Test001));        
    }

    
    
    IEnumerator TestC(int cardKey_)
    {
        yield return new WaitForSeconds(3);

        testDic[cardKey_].Test0010();
        Debug.Log($"DicIdValue ID : {cardKey_}");
        Debug.Log($"Return Num : {testDic[cardKey_].Num}");

        
    }
}

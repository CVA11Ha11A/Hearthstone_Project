using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using System;

public class DataStringTest
{
    public string tempStr = string.Empty;
}
[System.Serializable]
public class Test001 : MonoBehaviour
{
    public DataStringTest data;

    private void Awake()
    {
        data = new DataStringTest();
        data.tempStr = "초기화했음";
        StartCoroutine(TESTCO());
    }
    IEnumerator TESTCO()
    {
        yield return new WaitForSeconds(3f);
        DE.Log($"Test002가 변경한게 잘 반영이 되었나? : {this.data.tempStr}");
    }

}

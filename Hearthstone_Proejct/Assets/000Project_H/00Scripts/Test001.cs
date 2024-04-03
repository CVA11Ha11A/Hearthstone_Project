using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using System;

[System.Serializable]
public class Test001 : MonoBehaviour
{
    public List<int> testList = null;
    public Test002 testroot = null;

    public int[] testArr = null;
    public bool TestSet;
    public bool SaveGo;
    public bool LoadGo;

    string dataFilePath = null;

    private void Awake()
    {
        testroot = new Test002();

        dataFilePath = Application.persistentDataPath +"/" + "TestSaveData";

        if (TestSet == true)
        {
            testList = new List<int>();
            if(testroot.testSirial == null)
            {
                testroot.testSirial = new List<int>();
            }
            for (int i = 0; i < 10; i++)
            {

                testroot.testSirial.Add(i + UnityEngine.Random.Range(0, 100));
            }
            testArr = new int[10];
            for (int i = 0; i < testArr.Length; i++)
            {
                testArr[i] = i + UnityEngine.Random.Range(0, 100);
            }

        }
        if (SaveGo == true)
        {
            
            string saveData = JsonUtility.ToJson(testroot, true);
            System.IO.File.WriteAllText(dataFilePath, saveData);
            DE.Log($"SAVE : {saveData}");

        }
        if (LoadGo == true)
        {
            string loadData = System.IO.File.ReadAllText(dataFilePath);
            testroot = JsonUtility.FromJson<Test002>(loadData);
            DE.Log($"Load : {loadData}");
        }


    }


}

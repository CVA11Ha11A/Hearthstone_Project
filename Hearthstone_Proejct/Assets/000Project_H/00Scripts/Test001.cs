using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using TMPro;
using UnityEngine.UI;



[System.Serializable]
public class Test001 : MonoBehaviour
{
    public GameObject testObj;


    private void Awake()
    {        


    }

    private void Start()
    {
        StartCoroutine(CTest());
    }

    IEnumerator CTest()
    {
        yield return new WaitForSeconds(2f);
        this.transform.GetComponent<Card>().MinionFieldSpawn(testObj);
    }



}








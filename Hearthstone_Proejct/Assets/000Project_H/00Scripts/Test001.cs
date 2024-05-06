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

    public bool testBool = default;
    public List<bool> LBool = null;



    private void Awake()
    {
       LBool = new List<bool>();
        testBool = true;
        LBool.Add(testBool);
        testBool = false;
        DE.Log($"testBool 의 값은 뭐지? -> true면 값이 복사되어 들어가는것 , false면 값이 참조형태롤 되는것\n값 : {LBool[0]}");
     


    }
    
   



}








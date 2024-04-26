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



[System.Serializable]
public class Test001 : MonoBehaviour
{
    
    private void Awake()
    {

        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.B))
        {
            Test();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.V))
        {
            Test1();
        }
        if (UnityEngine.Input.GetKeyDown(KeyCode.K))
        {
            PhotonNetwork.CreateRoom("TestRoom", new RoomOptions());
        }
    }

    public void Test()
    {
        SceneManager.LoadScene("InGameScene");
    }
    public void Test1()
    {
        PhotonNetwork.LoadLevel("InGameScene");
    }


}








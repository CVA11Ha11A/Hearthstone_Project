using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    /// <summary>
    /// 0 : 일반 하수인 1 : 주문
    /// </summary>
    public Image[] cardOutlines;
    /// <summary>
    /// 0 : 레어 1 : 에픽 2 : 전설
    /// </summary>
    public Material[] minionOutLines;

    private static LobbyManager instance = default;
    public static LobbyManager Instance 
    {
        get 
        {
            if(instance == null || instance == default)
            {
                GameObject lobbyManager = new GameObject("LobbyManager");
                lobbyManager.AddComponent<LobbyManager>();                
            }
            return instance; 
        } 
    }
    

    // Collection의 열리는 기능(함수)이 구독할 이벤트
    public event Action OpenCollectionEvent;


    private void Awake()
    {        
        if(instance == null || instance == default)
        {
            instance = this;
        }
        else { /*PASS*/ }
    }       // Awake()



    public void OpenCollection()
    {        
        OpenCollectionEvent?.Invoke();
    }       // OpenCollection()


}       // LobbyManager ClassEnd

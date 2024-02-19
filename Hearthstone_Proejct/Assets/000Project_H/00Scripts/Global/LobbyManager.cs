using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
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

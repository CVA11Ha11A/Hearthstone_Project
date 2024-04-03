using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{       // Player데이터 관련된것들을 도와줄 메니저
    private static PlayerDataManager instance = null;
    public static PlayerDataManager Instance
    {
        get
        {
            if(instance == null || instance == default)
            {
                GameObject tempObj = new GameObject("PlayerDataManager");
                tempObj.transform.AddComponent<PlayerDataManager>();
            }
            return instance;
        }
        set 
        {
            if(instance != value)
            {
                instance = value;
            }
        }
    }

    public PlayerDecks playerDeckRoot = null;       // LobbyManager의 Root와 중복됨 이거 좀 고민해야할듯

    private void Awake()
    {
        Instance = this;
    }       // Awake()

}

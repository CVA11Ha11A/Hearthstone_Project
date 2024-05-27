using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerDecks : MonoBehaviour
{       // 로컬에 저장되어있는 덱들을 가져올것이고 현재 플레이어가 가지고 있는 덱을 여기서 관리할것
 
    //public List<Deck> decksList = default;      // LEGACY
    public PlayerDeckData deckClass = null;    
    public const int MAX_DECK_COUNT = 9;

    private string filePath = default;


    private void Awake()
    {
        LobbyManager.Instance.playerDeckRoot = this;
        PlayerDataManager.Instance.playerDeckRoot = this;

        filePath = Application.persistentDataPath + "/PlayerDecks.json";
        LoadDecks();
    }

    private void Start()
    {
        LobbyManager.Instance.collectionCanvasRoot.deckListComponentRoot.UpdateOutputDeckList();
    }

    public void LoadDecks()
    {        
        if (File.Exists(filePath) == true)
        {            
            string loadData = File.ReadAllText(this.filePath);
            deckClass = JsonUtility.FromJson<PlayerDeckData>(loadData);            
        }
        else
        {
            deckClass = new PlayerDeckData();
        }
    }       // LoadDecks()

    public void SaveDecks()
    {                
        string saveData = JsonUtility.ToJson(deckClass);
        File.WriteAllText(filePath, saveData);        
    }       // SaveDecks()


}       // ClassEnd

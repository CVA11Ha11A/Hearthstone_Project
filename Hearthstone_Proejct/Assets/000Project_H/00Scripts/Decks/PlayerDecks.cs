using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerDecks : MonoBehaviour
{       // 로컬에 저장되어있는 덱들을 가져올것이고 현재 플레이어가 가지고 있는 덱을 여기서 관리할것

    public List<Deck> decksList = default;
    public const int MAX_DECK_COUNT = 9;

    private string filePath = default;
    public PlayerDecks()
    {
        
    }

    private void Awake()
    {
        LobbyManager.Instance.playerDeckRoot = this;
        filePath = Application.persistentDataPath + "/PlayerDecks.json";
        LoadDecks();
    }

    private void LoadDecks()
    {        
        if (File.Exists(filePath) == true)
        {
            string loadData = File.ReadAllText(this.filePath);           
            decksList = JsonUtility.FromJson<List<Deck>>(loadData);            
            DE.Log($"decksList : {decksList == null}\n");
            for (int i = 0; i < decksList[0].DEVELOP_Cards.Length; i++)
            {
                DE.Log($"CARD ID {decksList[0].DEVELOP_Cards[i]}");
            }

            
        }
        else
        {
            decksList = new List<Deck>(MAX_DECK_COUNT);
        }
    }       // LoadDecks()

    public void SaveDecks()
    {
        string saveData = JsonUtility.ToJson(decksList);
        File.WriteAllText(filePath, saveData);
        DE.Log($"저장완료 : {decksList}");        
    }       // SaveDecks()

}       // ClassEnd

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerDecks : MonoBehaviour
{       // 로컬에 저장되어있는 덱들을 가져올것이고 현재 플레이어가 가지고 있는 덱을 여기서 관리할것

    private List<Deck> decksList = default;

    private string filePath = default;
    public PlayerDecks()
    {
        
    }

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/PlayerDecks.json";
        LoadDecks();
    }

    private void LoadDecks()
    {        
        if (File.Exists(filePath) == true)
        {
            string loadData = File.ReadAllText(this.filePath);
            decksList = JsonUtility.FromJson<List<Deck>>(loadData);
            DEB.Log($"불러와졌나? : {decksList}");
        }
        else
        {
            decksList = new List<Deck>();
        }
    }       // LoadDecks()

    public void SaveDecks()
    {
        string saveData = JsonUtility.ToJson(decksList);
        File.WriteAllText(filePath, saveData);
        DEB.Log($"저장완료 : {decksList}");        
    }       // SaveDecks()

}       // ClassEnd

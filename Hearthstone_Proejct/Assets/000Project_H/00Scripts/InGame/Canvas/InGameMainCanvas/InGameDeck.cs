using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDeck : MonoBehaviour
{       // 플레이어의 덱을 관리 (저장 해둔 덱 X)
        // 덱을 새로 인스턴스 해야함
    private Deck inGameDeck = null;
    private GameObject[] cardObjs = null;

    private void Awake()
    {
        int cardsChildCount = this.transform.GetChild(0).childCount;
        cardObjs = new GameObject[cardsChildCount];

        for(int i = 0; i < cardsChildCount; i++)
        {
            cardObjs[i] = this.transform.GetChild(0).GetChild(i).gameObject;
        }

        if(this.transform.name == "MyDeck")
        {
            InGameManager.Instance.InGameMyDeckRoot = this;
        }
        else if(this.transform.name == "EnemyDeck")
        {
            InGameManager.Instance.InGameEnemyDeckRoot = this;
        }

    }
    void Start()
    {
        
    }

    public void DeckInit()
    {       // 
        inGameDeck = new Deck();
    }
    

    
}       // ClassEnd

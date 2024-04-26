using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDeck : MonoBehaviour
{       // 플레이어의 덱을 관리 (저장 해둔 덱 X)
        // 덱을 새로 인스턴스 해야함
    private Deck inGameDeck = null;
    public Deck InGamePlayerDeck
    {
        get
        {
            return this.inGameDeck;
        }
    }
    private GameObject[] cardObjs = null;
    private ClassCard deckClass = default;

    private void Awake()
    {
        int cardsChildCount = this.transform.GetChild(0).childCount;
        cardObjs = new GameObject[cardsChildCount];

        for (int i = 0; i < cardsChildCount; i++)
        {
            cardObjs[i] = this.transform.GetChild(0).GetChild(i).gameObject;
        }

        if (this.transform.name == "MyDeck")
        {
            InGameManager.Instance.InGameMyDeckRoot = this;
            this.deckClass = GameManager.Instance.inGamePlayersDeck.MyDeck.deckClass;
        }
        else if (this.transform.name == "EnemyDeck")
        {
            InGameManager.Instance.InGameEnemyDeckRoot = this;
            this.deckClass = GameManager.Instance.inGamePlayersDeck.EnemyDeck.deckClass;
        }

    }
    void Start()
    {

    }

    public void DeckInit(string[] cardIds_)
    {       // split된 인자가 들어옴
        inGameDeck = new Deck();
        for(int i = 0; i< cardIds_.Length; i++)
        {
            DE.Log(cardIds_[i]);
        }
        for (int i = 0; i < cardIds_.Length; i++)
        {
            DE.Log(cardIds_[i]);
            inGameDeck.AddCardInDeck((CardID)int.Parse(cardIds_[i]));
        }
        inGameDeck.deckClass = this.deckClass;


    }

}       // ClassEnd

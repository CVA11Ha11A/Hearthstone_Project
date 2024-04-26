using System.Collections;
using System.Collections.Generic;
using System.Text;
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
            DE.Log($"뭐가 Null이지 ? InGameManager.Instance :  {InGameManager.Instance == null}\nGameManager.Instance.inGamePlayerDeck null? : {GameManager.Instance.inGamePlayersDeck.EnemyDeck == null}");
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
        int parseValue = -1;
        
        for (int i = 0; i < cardIds_.Length; i++)
        {
            //DE.Log($"{cardIds_[i]}\nIsParse? : {int.TryParse(cardIds_[i],out delog)} , DeLog : {delog}");
            cardIds_[i] = cardIds_[i].Replace(" ","");
            parseValue = int.Parse(cardIds_[i]);
            if (parseValue == (int)CardID.StartPoint || parseValue == (int)CardID.EndPoint)
            {
                continue;
            }
            inGameDeck.AddCardInDeck((CardID)parseValue);
        }
        //inGameDeck.deckClass = this.deckClass; //이 값 말고 Lobby에서 초기화된 덱의 직업을 할당하는것이 맞는거같음


    }

}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


public class InGamePlayersDeck : MonoBehaviour
{

    private Deck myDeck = null;
    public Deck MyDeck
    {
        get
        {
            return this.myDeck;
        }
        set
        {
            if (this.myDeck != value)
            {
                this.myDeck = value;
            }
        }
    }

    private Deck enemyDeck = null;
    public Deck EnemyDeck
    {
        get
        {
            return this.enemyDeck;
        }
        set
        {
            if (this.enemyDeck != value)
            {
                enemyDeck = value;
            }
        }
    }

    public bool isMyDeckInIt = false;       // bool 값 2개는 현재 적과 나의 덱을 포톤으로 받은것으로 초기화 시켰는지 확인할 변수
    public bool isEnemyDeckInIt = false;
    

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameManager.Instance.inGamePlayersDeck = this;

        myDeck = new Deck();
        enemyDeck = new Deck();
        
    }

    public void ClearDecks()
    {   // 게임이 끝난후 새로운 매칭을 할때에는 새로 제작해야함
        this.myDeck = null;
        this.enemyDeck = null;
    }

    public void EnemyDeckSetting(string catchDeck_)
    {   //    "1-1,2,3,4,5,"
        int firstParse = default;

        string[] splitDeckData = catchDeck_.Split("-");  // index[0] == calss , index[1] == cardDatas
        this.EnemyDeck.deckClass = (ClassCard)int.Parse(splitDeckData[0]);   // ClassSet

        //DE.Log($"{splitDeckData[1]}");

        string[] deckCardIds = splitDeckData[1].Split(',');
        for (int i = 0; i < deckCardIds.Length; i++)
        {
            if (deckCardIds[i] == " " || deckCardIds[i] == "" || deckCardIds[i] == "," || deckCardIds[i] == "-")
            {
                continue;
            }
            deckCardIds[i].Replace(",", "");

            firstParse = int.Parse(deckCardIds[i]);
            // DE.Log($"{i}번째 순회/ 변환 시도할 카드 : {deckCardIds[i]}, int로 변환된 것 : {firstParse}");
            EnemyDeck.cardList[i] = (CardID)firstParse;

        }

    }       // EnemyDeckSetting()

    public void MyDeckSetting(int deckRefIndex_)
    {
        Deck tempRoot = LobbyManager.Instance.playerDeckRoot.decks.deckList[deckRefIndex_];

        this.myDeck.deckClass = tempRoot.deckClass;
        for (int i = 0; i < tempRoot.cardList.Length; i++)
        {
            this.MyDeck.cardList[i] = tempRoot.cardList[i];
        }
    }       // MyDeckSetting()

}   // ClassEnd

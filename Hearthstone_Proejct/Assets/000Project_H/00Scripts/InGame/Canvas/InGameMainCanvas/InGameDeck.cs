using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
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

      

    }
    void Start()
    {
        if (this.transform.name == "MyDeck")
        {
            InGameManager.Instance.InGameMyDeckRoot = this;
            this.deckClass = GameManager.Instance.inGamePlayersDeck.MyDeck.deckClass;
        }
        else if (this.transform.name == "EnemyDeck")
        {
            //DE.Log($"뭐가 Null이지 ? InGameManager.Instance :  {InGameManager.Instance == null}\nGameManager.Instance.inGamePlayerDeck null? : {GameManager.Instance.inGamePlayersDeck.EnemyDeck == null}");
            InGameManager.Instance.InGameEnemyDeckRoot = this;
            this.deckClass = GameManager.Instance.inGamePlayersDeck.EnemyDeck.deckClass;
        }
    }

    public void DeckInit(string[] cardIds_, ETarGet initTarget_)
    {       // split된 인자가 들어옴
        inGameDeck = new Deck();

        // Class설정
        if (initTarget_ == ETarGet.My)
        {
            inGameDeck.deckClass = GameManager.Instance.inGamePlayersDeck.MyDeck.deckClass;
        }
        else
        {
            inGameDeck.deckClass = GameManager.Instance.inGamePlayersDeck.EnemyDeck.deckClass;
        }

        int parseValue = -1;

        for (int i = 0; i < cardIds_.Length; i++)
        {
            //DE.Log($"{cardIds_[i]}\nIsParse? : {int.TryParse(cardIds_[i],out delog)} , DeLog : {delog}");
            cardIds_[i] = cardIds_[i].Replace(" ", "");
            parseValue = int.Parse(cardIds_[i]);
            if (parseValue == (int)CardID.StartPoint || parseValue == (int)CardID.EndPoint)
            {
                continue;
            }
            inGameDeck.AddCardInDeck((CardID)parseValue);
        }

        if (PhotonNetwork.IsMasterClient != true)
        {   // 클라이언트가 적의 덱을 초기화 완료했을 경우
            if (initTarget_ == ETarGet.Enemy)
            {
                InGameManager.Instance.IsCompleateDeckInItCheck();
            }
        }
        else if(PhotonNetwork.IsMasterClient == true)
        {
            if (initTarget_ == ETarGet.Enemy)
            {
                InGameManager.Instance.isCompleatMyDeckInit = true;
                InGameManager.Instance.IsCompleateDeckInItCheck();
            }
        }
        // DE.Log($"ingameDeck New 할당 완료 : IsNull? : {this.InGamePlayerDeck == null}");
    }       // DeckInit()

    public void ChangeDeckInIt(string[] changedCardIds_)
    {       // 변경된 -> 셔플된 카드로 덱을 다시 설정하는 함수

        this.InGamePlayerDeck.ClearCardList();
        for (int i = 0; i < changedCardIds_.Length; i++)
        {
            this.InGamePlayerDeck.AddCardInDeck((CardID)int.Parse(changedCardIds_[i]));
        }

    }       // ChangeDeckInIt()

    public void TestOutPut()
    {
        StringBuilder sb1 = new StringBuilder();
        for (int i = 0; i < this.InGamePlayerDeck.currentIndex; i++)
        {
            sb1.Append((int)this.InGamePlayerDeck.cardList[i]);
            sb1.Append(" ");
        }
        Debug.Log(sb1);
    }

}       // ClassEnd

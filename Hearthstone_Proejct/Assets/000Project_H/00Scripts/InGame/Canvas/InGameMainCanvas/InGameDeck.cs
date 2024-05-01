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

    private InGameHand targetHand = null;
    public InGameHand TargetHand
    {
        get
        {
            if (this.targetHand == null)
            {
                if (this.transform.name == "MyDeck")
                {
                    this.targetHand = InGameManager.Instance.mainCanvsRoot.handRoot.MyHand;
                }
                else if (this.transform.name == "EnemyDeck")
                {
                    this.targetHand = InGameManager.Instance.mainCanvsRoot.handRoot.EnemyHand;
                }
            }
            return this.targetHand;
        }
    }
    // ------------------------------------------------------- 유니티 사이클 -------------------------------------------------------------------------
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
            this.transform.parent.GetComponent<InGameDecks>().MyDeckSetter(this);
        }
        else if (this.transform.name == "EnemyDeck")
        {
            InGameManager.Instance.InGameEnemyDeckRoot = this;
            this.deckClass = GameManager.Instance.inGamePlayersDeck.EnemyDeck.deckClass;
            this.transform.parent.GetComponent<InGameDecks>().EnemyDeckSetter(this);
        }
    }
    // ------------------------------------------------------- 덱초기화 관련 함수들 -------------------------------------------------------------------------
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
        else if (PhotonNetwork.IsMasterClient == true)
        {
            if (initTarget_ == ETarGet.Enemy)
            {
                InGameManager.Instance.isCompleatMyDeckInit = true;
                InGameManager.Instance.IsCompleateDeckInItCheck();
            }
        }

        // 카드 오브젝트 초기화
        for (int i = 0; i < InGamePlayerDeck.cardList.Length; i++)
        {
            if (InGamePlayerDeck.cardList[i] == CardID.StartPoint || InGamePlayerDeck.cardList[i] == CardID.EndPoint)
            {
                continue;
            }
            cardObjs[i].gameObject.SetActive(true);
            CardManager.Instance.InItCardComponent(cardObjs[i], InGamePlayerDeck.cardList[i]);
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

    // 드로우
    public void DrawCard()
    {
        // 드로우 할떄마다 Deck의 PullDeck을 호출하면 땡겨짐 중간에 카드 드로우라면 인자를 넣어주면됨
        // 카드를 뽑을경우 
        int objIndex = -1;
        CardID targetCard = InGamePlayerDeck.cardList[0];

        // 타겟의 인덱스를 찾는 for
        for (int i = 0; i < cardObjs.Length; i++)
        {
            if (cardObjs[i] == null)
            {
                continue;
            }
            else if (cardObjs[i].activeSelf == false)
            {
                continue;
            }
            else if (cardObjs[i].GetComponent<Card>() == true)
            {
                if(cardObjs[i].GetComponent<Card>().cardId == targetCard)
                {
                    objIndex = i;
                }
            }            
        }

        // 디버깅용
        if(objIndex == -1)
        {
            DE.Log($"TargetCard를 찾지 못했음");
        }

        // 해당 게임 오브젝트가 핸드로 가면됨
        InGamePlayerDeck.cardList[0] = CardID.StartPoint;   // 뽑은카드는 덱의 데이터에서 제외        // 상대에게도 해줘야함
        InGamePlayerDeck.PullCardList();
        cardObjs[objIndex].transform.rotation = Quaternion.Euler(0,0,0);
        TargetHand.AddCardInHand(cardObjs[objIndex]);
        cardObjs[objIndex] = null;

    }       // DrawCard


    // -------------------------------------------------------- 테스트 ------------------------------------------------------------------
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

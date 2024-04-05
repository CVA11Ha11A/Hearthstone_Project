using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CollectionDeckCardList : MonoBehaviour
{   // 덱 생성 수정 관련한 컴포넌트
    public GameObject[] cardList = null;

    public ClassCard selectClass = default;
    private int currentIndex = default;      // Initalize == 0 
    private DeckInCard deckInCardRoot = null;

    public bool isCreatDeck = default;
    public bool isFixDeck = default;        // 덱 저장할때 해당 bool값 2개를 확인해서 리스트를 수정할지 아니면 추가할지 결정될것
    public int fixIndex = default;          // 수정시 변경되어야할 PlayerDeckList의 인덱스 

    private RectTransform moveObj = null;
    private Vector3 onOutputV3 = default;       // ScrollView의 Transform을 조정시켜줄 Vector3
    private Vector3 offOutputV3 = default;      // ScrollView의 Transform을 조정시켜줄 Vector3

    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().deckCardListRoot = this;

        moveObj = this.transform.parent.parent.GetComponent<RectTransform>();
        onOutputV3 = moveObj.anchoredPosition3D;
        offOutputV3 = onOutputV3;
        offOutputV3.x = 80f;

        isFixDeck = false;
        isCreatDeck = false;

        cardList = new GameObject[this.transform.childCount];
        for (int i = 0; i < cardList.Length; i++)
        {
            cardList[i] = this.transform.GetChild(i).gameObject;
        }
        SetActiveFlaseToChilds();
        CardListTransformSet(CollectionState.Looking);      // 이건 좀 좋지 않은 코드같음 하지만 현재기준 어쩔수 없이 하는 함수 2024.04.04

    }       // Awake()

    public void SetActiveFlaseToChilds()
    {
        for (int i = 0; i < cardList.Length; i++)
        {
            cardList[i].gameObject.SetActive(false);
        }
    }       // SetActiveFlaseToChilds()


    public void AddToCard(Card addCard_)
    {
        #region CardSetting
        cardList[currentIndex].SetActive(true);
        deckInCardRoot = cardList[currentIndex].GetComponent<DeckInCard>();
        #region LEGACY
        //deckInCardRoot.CardName = addCard_.cardName;
        //deckInCardRoot.CardCost = addCard_.cost;
        //deckInCardRoot.SetCardId(addCard_.cardId);
        //deckInCardRoot.cardImageRoot.sprite = addCard_.cardImage.sprite;
        #endregion LEGACY

        deckInCardRoot.datas.cardId = addCard_.cardId;
        deckInCardRoot.datas.cardCost = addCard_.cost;
        deckInCardRoot.datas.cardName = addCard_.cardName;
        //deckInCardRoot.datas.cardSprite = addCard_.cardImage.sprite;
        //TEST
        if (addCard_.cardImage != null)
        {
            deckInCardRoot.datas.cardSprite = addCard_.cardImage.sprite;
        }
        else
        {
            deckInCardRoot.datas.cardSprite = addCard_.GetCardSprite();
        }


        #endregion CardSetting

        #region SortCardList
        DeckInCard cardIRoot = default;
        DeckInCard cardJRoot = default;
        for (int i = 0; i < currentIndex; i++)
        {
            cardIRoot = cardList[i].GetComponent<DeckInCard>();

            for (int j = i + 1; j <= currentIndex; j++)
            {

                cardJRoot = cardList[j].GetComponent<DeckInCard>();
                if (cardIRoot.datas.cardCost > cardJRoot.datas.cardCost)
                {
                    DeckInCardData tempRoot = cardIRoot.datas;
                    cardIRoot.datas = cardJRoot.datas;
                    cardJRoot.datas = tempRoot;
                }
            }
        }
        #endregion SortCardList

        #region UpdataUI
        for (int i = 0; i <= currentIndex; i++)
        {
            cardList[i].GetComponent<DeckInCard>().UpdateUI();
        }
        #endregion UpdataUI

        currentIndex++;

    }       // AddToCard()

    // 덱속 카드를 제거해주는 함수
    public void RemoveToCard(CardID removeCardId_)
    {
        DeckInCard deckInCardRoot = null;
        for (int i = 0; i < cardList.Length; i++)
        {
            deckInCardRoot = cardList[i].GetComponent<DeckInCard>();
            if (deckInCardRoot.datas.cardId == removeCardId_)
            {
                deckInCardRoot.ClearData();
                currentIndex--;
            }
        }
    }   // RemoveToCard()

    public void DeckOutPut(int targetDeckIndex_)
    {   // 플레이어가 선택한 덱의 인덱스를 참조해서 덱의 현재 존재하는 카드들을 출력해주는 함수
        // DeckListCompoent.DeckOnClick() 가 호출할거임
        PlayerDeckData playerDeckRoot = LobbyManager.Instance.playerDeckRoot.decks;
        int deckMaxCard = playerDeckRoot.deckList[targetDeckIndex_].count;
        for (int i = 0; i < deckMaxCard; i++)
        {
            AddToCard(CardManager.cards[playerDeckRoot.deckList[targetDeckIndex_].cardList[i]]);
        }

    }       // DeckOutPut()

    public void CardListTransformSet(CollectionState collectionState_)
    {
        //return;
        if (collectionState_ == CollectionState.Looking)
        {
            moveObj.anchoredPosition3D = offOutputV3;
        }
        else if (collectionState_ == CollectionState.DeckBuild)
        {
            moveObj.anchoredPosition3D = onOutputV3;
        }
    }       // CardListTransformSet()

    public int GetCurrentIndex()
    {
        return this.currentIndex;
    }
    public void ClearCurrentIndex()
    {
        this.currentIndex = 0;
    }



}       // ClassEnd

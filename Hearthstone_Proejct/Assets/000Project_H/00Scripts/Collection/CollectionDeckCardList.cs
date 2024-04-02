using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionDeckCardList : MonoBehaviour
{
    public GameObject[] cardList = null;

    public ClassCard selectClass = default;
    private int currentIndex = default;      // Initalize == 0 
    private DeckInCard deckInCardRoot = null;

    public bool isCreatDeck = default;
    public bool isFixDeck = default;        // 덱 저장할때 해당 bool값 2개를 확인해서 리스트를 수정할지 아니면 추가할지 결정될것
    public int fixIndex = default;          // 수정시 변경되어야할 PlayerDeckList의 인덱스 

    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().deckCardListRoot = this;
        isFixDeck   = false;
        isCreatDeck = false;

        cardList = new GameObject[this.transform.childCount];
        for (int i = 0; i < cardList.Length; i++)
        {
            cardList[i] = this.transform.GetChild(i).gameObject;
        }
        SetActiveFlaseToChilds();

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
        deckInCardRoot.datas.cardSprite = addCard_.cardImage.sprite;        

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

    public int GetCurrentIndex()
    {
        return this.currentIndex;
    }
    public void ClearCurrentIndex()
    {
        this.currentIndex = 0;
    }



}       // ClassEnd

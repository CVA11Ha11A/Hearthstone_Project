using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionDeckCardList : MonoBehaviour
{
    GameObject[] cardList = null;
    private int currentIndex = default;      // Initalize == 0 

    private DeckInCard deckInCardRoot = null;

    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().deckCardListRoot = this;

        cardList = new GameObject[this.transform.childCount];
        for (int i = 0; i < cardList.Length; i++)
        {
            cardList[i] = this.transform.GetChild(i).gameObject;
        }
        SetActiveFlaseToChilds();

    }       // Awake()

    private void SetActiveFlaseToChilds()
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



}       // ClassEnd

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class Deck : IDeckFunction
{       // 덱이 가지고 있어야하는것들만 가지고 있을것

    public const int MAX_CARD_COUNT = 30;       // 카드리스트가 담을 수 있는 최대 카드 갯수
    public const int CARDLIST_MAXLOOP = 29;     // 카드리스트의 루프의 최대치

    public int currentIndex = default;
    public int count = default;
    public CardID[] cardList = default;

    public ClassCard deckClass = default; // 직업 
    


    public Deck()
    {
        cardList = new CardID[MAX_CARD_COUNT];
    }


    public void RemoveCard(CardID removeCardId_)
    {
        for (int i = 0; i < cardList.Length; i++)
        {
            if (this.cardList[i] == removeCardId_)
            {
                #region LEGACY
                //cardList[i] = CardID.StartPoint;
                //PullCardList();
                //currentIndex--;
                //count--;
                //return;
                #endregion LEGACY

                this.cardList[i] = this.cardList[currentIndex - 1];
                this.cardList[currentIndex - 1] = CardID.StartPoint;
                this.currentIndex--;
                this.count--;
                return;
            }
        }        
    }

    public void DrawCardRemoveCard()
    {
        cardList[0] = CardID.StartPoint;
        this.currentIndex--;
        this.count--;        
    }

    public void DrawCardRemoveCard(CardID removeCardId_)
    {
        for(int i = 0; i < cardList.Length; i++)
        {
            if(cardList[i] == removeCardId_)
            {
                cardList[i] = CardID.StartPoint;
                break;
            }
        }
        this.currentIndex--;
        this.count--;
    }

    public void PullCardList()
    {   // 첫 번째 인덱스 부터 순회하며 한칸씩 땡김
        for (int i = 0; i < cardList.Length; i++)
        {
            if (i + 1 >= cardList.Length)
            {   // IndexOutRange 예외처리
                break;
            }
            if (cardList[i] == CardID.StartPoint || cardList[i] == CardID.EndPoint)
            {                
                cardList[i] = cardList[i + 1];
                cardList[i + 1] = CardID.StartPoint;
            }
        }
    }       // PullCardList()

    public void PullCardList(int targetIndex_)
    {       // 카드 제거이후 빈공간 없이 땅기는 함수 / 어디서 부터 한칸씩 땡길지 인자로 보낼수 있음
        if (targetIndex_ == cardList.Length)
        {       // 마지막 카드가 지우는 카드라면
            cardList[targetIndex_] = CardID.StartPoint;
            return;
        }

        for (int i = targetIndex_; i < cardList.Length - 1; i++)
        {
            if (cardList.Length == i + 1)
            {   // 마지막 으로 왔으면
                cardList[i] = CardID.StartPoint;
                return;
            }

            cardList[i] = cardList[i + 1];
            if (cardList[i + 1] == CardID.StartPoint || cardList[i + 1] == CardID.EndPoint)
            {
                break;
            }
        }
    }       // PullCardList()

    #region InterfaceMethod
    public void AddCardInDeck(CardID addCardId_)
    {
        if (this.currentIndex == MAX_CARD_COUNT)
        {
            return;
        }

        this.cardList[this.currentIndex] = addCardId_;
        this.currentIndex++;
        this.count++;
    }       // AddCard()

    public void SetDeckClass(ClassCard heroClass_)
    {
        this.deckClass = heroClass_;
    }       // SetDeckClass()

    public void ClearDeck()
    {
        for(int i =0; i < this.cardList.Length; i++)
        {
            this.cardList[i] = CardID.StartPoint;
        }
        this.count = default;
        this.currentIndex = default;
        this.deckClass = ClassCard.None;
    }       // ClearDeck()

    public void ClearCardList()
    {
        for (int i = 0; i < this.cardList.Length; i++)
        {
            this.cardList[i] = CardID.StartPoint;
        }
        this.count = default;
        this.currentIndex = default;
    }
    #endregion InterfaceMethod



}       // DeckClassEnd

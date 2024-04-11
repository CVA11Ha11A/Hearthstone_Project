using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class Deck : IDeckFunction
{       // 덱이 가지고 있어야하는것들만 가지고 있을것

    public const int MAX_CARD_COUNT = 30;

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
            if (cardList[i] == removeCardId_)
            {
                //PullCardList(i);
                cardList[i] = CardID.StartPoint;
                PullCardList();
                break;
            }
        }
        currentIndex--;
        count--;
    }
    public void PullCardList()
    {   // 첫 번째 인덱스 부터 순회하며 한칸씩 땡김
        for (int i = 0; i < MAX_CARD_COUNT - 1; i++)
        {
            if (cardList[i] == CardID.StartPoint || cardList[i] == CardID.EndPoint)
            {
                if (i + 1 >= MAX_CARD_COUNT)
                {   // IndexOutRange 예외처리
                    break;
                }
                cardList[i] = cardList[i + 1];

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
        if (currentIndex == MAX_CARD_COUNT - 1)
        {
            return;
        }

        cardList[currentIndex] = addCardId_;
        currentIndex++;
        count++;
    }       // AddCard()

    public void SetDeckClass(ClassCard heroClass_)
    {
        this.deckClass = heroClass_;
    }       // SetDeckClass()
    #endregion InterfaceMethod



}       // DeckClassEnd

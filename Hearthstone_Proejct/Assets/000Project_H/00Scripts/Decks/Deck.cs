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
                //DE.Log($"제거 하려는 카드 : {removeCardId_}, 카드가 존재하던 인덱스 : {i}");
                PullCardList(i);
                break;
            }
        }
        currentIndex--;
        count--;
    }
    private void PullCardList(int targetIndex_)
    {       // 카드 제거이후 빈공간 없이 땅기는 함수
        StringBuilder test = new StringBuilder();
        if (targetIndex_ == cardList.Length)
        {       // 마지막 카드가 지우는 카드라면
            cardList[targetIndex_] = CardID.StartPoint;
            return;
        }

        for (int i = targetIndex_; i < cardList.Length; i++)
        {
            test.Clear();
            if (cardList.Length == i + 1)
            {   // 마지막 으로 왔으면
                cardList[i] = CardID.StartPoint;
                return;
            }


            for(int j = i; j < cardList.Length; j++)
            {
                test.Append((int)cardList[j]);
                test.Append(",");
            }
            DE.Log($"땡기기전 : {test}");
            test.Clear();

            cardList[i] = cardList[i + 1];
            if (cardList[i+1] == CardID.StartPoint || cardList[i + 1] == CardID.EndPoint)
            {
                break;
            }


            for (int k = i; k < cardList.Length; k++)
            {
                test.Append((int)cardList[k]);
                test.Append(",");
            }
            DE.Log($"땡긴후 : {test.ToString()}");
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

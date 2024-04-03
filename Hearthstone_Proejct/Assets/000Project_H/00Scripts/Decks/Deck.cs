using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


[System.Serializable]
public class Deck : IDeckFunction
{       // 덱이 가지고 있어야하는것들만 가지고 있을것

    public const int MAXCOUNT = 30;

    public int currentIndex = default;
    public int count = default;

    public CardID[] cardList = default;

    public ClassCard deckClass = default; // 직업

    public Deck()
    {
        cardList = new CardID[MAXCOUNT];
    }
    

    // LEGACY 
    //public void RemoveCard()
    //{
    //    // 이 함수는 덱 제작 드래그엔 드랍을 하면서 해봐야할듯 매개로 뭘보낼지가 변경될 우려가존재
    //    // 이 함수가 덱에 존재해야 할까? 변경후 저장할때 어차피 변경이 될텐데? [04.02]
    //    currentIndex--;
    //    count--;
    //}

    #region InterfaceMethod
    public void AddCardInDeck(CardID addCardId_)
    {
        if (currentIndex == MAXCOUNT - 1)
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

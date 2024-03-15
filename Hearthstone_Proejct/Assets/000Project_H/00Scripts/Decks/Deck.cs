using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

[Serializable]
public class Deck : MonoBehaviour
{       // 덱이 가지고 있어야하는것들만 가지고 있을것

    private const int MAXCOUNT = 30;

    private int currentIndex = default;
    private int count = default;

    Card[] cardList = default;

    private void Awake()
    {
        cardList = new Card[MAXCOUNT];
    }

    public void RemoveCard()
    {
        // 이 함수는 덱 제작 드래그엔 드랍을 하면서 해봐야할듯 매개로 뭘보낼지가 변경될 우려가존재
        currentIndex--;
        count--;
    }

    public void AddCard<T>(T addCard_) where T : Card
    {
        if(currentIndex == MAXCOUNT -1)
        {
            return;
        }
        Type cardType = addCard_.GetType();

        cardList[currentIndex] = (Card)Activator.CreateInstance(cardType);
        currentIndex++;
        count++;
    }       // AddCard()

}       // DeckClassEnd

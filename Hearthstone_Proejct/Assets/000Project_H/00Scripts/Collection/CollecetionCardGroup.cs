using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecetionCardGroup : MonoBehaviour
{
    public GameObject[] cardPrefabObj = default;
    private CollectionCanvasController controllerRoot = null;

    private const int START_INDEX = 1;
    private int currentIndex = default;
    public int CurrentIndex
    {
        get
        {
            if (currentIndex == default || currentIndex == 0)
            {
                DE.Log($"CurrentIndex는 1이상이여야만해 CurrentIndex : {CurrentIndex}\n대충 무언가 잘못계산했다는 의미" );
                currentIndex = START_INDEX;
            }
            return currentIndex;
        }
        set
        {
            if (currentIndex != value)
            {
                //DE.Log($"CurrentIndex 변경 : {currentIndex}");
                currentIndex = value;
            }
            if (currentIndex < START_INDEX)
            {
                currentIndex = START_INDEX;
            }
        }
    }
    private Array cardIdEnumArr = default;

    private void Awake()
    {
        AwakeInIt();
    }

    private void AwakeInIt()
    {
        controllerRoot = GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>();
        controllerRoot.cardGroupRoot = this;
        int cardCount = this.transform.childCount;
        cardPrefabObj = new GameObject[cardCount];
        cardIdEnumArr = Enum.GetValues(typeof(CardID));

        for (int i = 0; i < cardCount; i++)
        {
            cardPrefabObj[i] = this.transform.GetChild(i).GetChild(0).gameObject;
        }


    }       // AwakeInIt()

    public void OutPutCard(ClassCard targetClass_)
    {
        //DE.Log($"OutPutCard 호출 : {targetClass_}");
        MonoBehaviour cardRoot = null;
        for (int i = 0; i < cardPrefabObj.Length; i++)
        {
            if (cardPrefabObj[i].GetComponent<Card>() == true)
            {
                cardRoot = cardPrefabObj[i].GetComponent<Card>();
                Destroy(cardRoot);
            }

            CardID addCardId = SelectOutputCard(targetClass_);
            //DE.Log($"출력할 카드 ID : {addCardId}");
            if (addCardId != CardID.EndPoint)
            {
                cardPrefabObj[i].SetActive(true);
                CardManager.Instance.InItCardComponent(cardPrefabObj[i], addCardId);
            }
            else
            {                
                cardPrefabObj[i].SetActive(false);
                controllerRoot.pageRoot.isNextMove = false;
            }
        }

    }       // OutPutCard()

    private CardID SelectOutputCard(ClassCard targetClass_)
    {
        CardID cardId_ = default;

        for (int i = CurrentIndex; i < cardIdEnumArr.Length; i++)
        {
            cardId_ = (CardID)cardIdEnumArr.GetValue(CurrentIndex);
           
            if (cardId_ == CardID.EndPoint)
            {
                return CardID.EndPoint;
            }

            if (CardManager.Instance.cards[cardId_].classCard == targetClass_)
            {
                CurrentIndex++;
                return cardId_;
            }            
        }


        return CardID.EndPoint;
    }

}       // CollecetionCardGroup ClassEnd

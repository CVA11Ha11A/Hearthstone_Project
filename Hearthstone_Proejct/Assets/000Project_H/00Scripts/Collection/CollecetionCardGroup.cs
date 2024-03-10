using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecetionCardGroup : MonoBehaviour
{
    private GameObject[] cardPrefabObj = default;

    private int currentIndex = default;

    private void Awake()
    {
        AwakeInIt();
    }

    private void AwakeInIt()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().cardGroupRoot = this;
        int cardCount = this.transform.childCount;
        cardPrefabObj = new GameObject[cardCount];

        for (int i = 0; i < cardCount - 1; i++)
        {
            cardPrefabObj[i] = this.transform.GetChild(i).GetChild(0).gameObject;
        }
    }       // AwakeInIt()

    public void OutPutCard(ClassCard targetClass_)
    {
        MonoBehaviour desRoot = null;
        for (int i = 0; i < cardPrefabObj.Length; i++)
        {
            if (cardPrefabObj[i].GetComponent<Card>() == true)
            {
                desRoot = cardPrefabObj[i].GetComponent<Card>();
                Destroy(desRoot);
            }
        }
    }       // OutPutCard()

    private void SelectOutputCard(ClassCard targetClass_)
    {

        //CardManager.cards[(CardID)currentIndex]

        Array array = Enum.GetValues(typeof(CardID));

    }

}       // CollecetionCardGroup ClassEnd

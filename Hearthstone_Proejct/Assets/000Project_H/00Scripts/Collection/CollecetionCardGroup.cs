using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollecetionCardGroup : MonoBehaviour
{
    private GameObject[] cardPrefabObj;
    
    private void Awake()
    {
        GetCardObjs();
    }

    private void GetCardObjs()
    {
        int cardCount = this.transform.childCount;
        cardPrefabObj = new GameObject[cardCount];
        
        for (int i = 0; i < cardCount -1; i++) 
        {
            cardPrefabObj[i] = this.transform.GetChild(i).GetChild(0).gameObject;
        }
        
    }


}       // CollecetionCardGroup ClassEnd

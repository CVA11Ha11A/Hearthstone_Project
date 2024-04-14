using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckInCardData
{
    public Sprite cardSprite = null;
    public string cardName = null;
    public CardID cardId = default;
    public int    cardCost = default;

    public void ClearDatas()
    {
        //cardSprite = null;
        cardName = "Null";
        cardId = default;
        cardCost = default;
    }
}


public class DeckInCard : MonoBehaviour
{
    private DeckInCardData datas = null;
    public DeckInCardData Datas
    {
        get
        {
            return this.datas;
        }
        set
        {
            if (this.datas != value) 
            {
                this.datas = value;
                //DE.Log($"Datas의 참조가 변경이 되었음");
            }
        }
    }
    public Image cardImageRoot = null;
    private TextMeshProUGUI costTextRoot = null;
    private TextMeshProUGUI cardNameRoot = null;
    


    private void Awake()
    {
        Datas = new DeckInCardData();
        cardImageRoot = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        costTextRoot = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        cardNameRoot = this.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();

    }       // Awake()

    


    [Obsolete("필드 데이터 -> 클래스 참조 데이터")]
    public void SetCardId(CardID cardId_)
    {   // 2024.03.29 수정 [Obsolete 추가]
        //this.cardId = cardId_;
    }       // SetCardId()

    public void UpdateUI()
    {   // 업데이트 되어야하는 UI를 최신화 하는 함수

        //DE.Log($"UpdateUI 함수를 실행하는 개체 : {this.gameObject.name}");
        this.costTextRoot.text = this.Datas.cardCost.ToString();
        this.cardNameRoot.text = this.Datas.cardName.ToString();

        if (Datas.cardSprite != null)
        {
            this.cardImageRoot.sprite = this.Datas.cardSprite;
        }
        else
        {
            this.Datas.cardSprite = CardManager.cards[this.Datas.cardId].GetCardSprite();
            this.cardImageRoot.sprite = this.Datas.cardSprite;
        }
    }

    public void OnEnable()
    {
        this.transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
    }

    private void OnDisable()
    {
        this.transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
    }

}       // ClassEnd

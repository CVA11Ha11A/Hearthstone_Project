using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckInCardData
{
    public Sprite cardSprite = default;
    public CardID cardId = default;
    public string cardName = default;
    public int cardCost = default;

}

public class DeckInCard : MonoBehaviour
{
    public Image cardImageRoot = null;
    private TextMeshProUGUI costTextRoot = null;
    private TextMeshProUGUI cardNameRoot = null;
    public DeckInCardData datas = default;

    #region LEGACY
    //private CardID cardId = default;                // 중요한 것이기에 Private로 감춤
    //
    //private string cardName = default;
    //
    //public string CardName
    //{
    //    get { return this.cardName; }
    //    set
    //    {
    //        if (this.cardName != value)
    //        {
    //            this.cardName = value;
    //        }
    //        cardNameRoot.text = this.cardName;
    //    }
    //}
    //
    //private int cardCost = default;
    //public int CardCost
    //{
    //    get
    //    {
    //        return cardCost;
    //    }
    //    set
    //    {
    //        if (cardCost != value)
    //        {
    //            cardCost = value;
    //        }
    //        costTextRoot.text = CardCost.ToString();
    //    }
    //}
    #endregion LEGACY



    private void Awake()
    {
        datas = new DeckInCardData();
        cardImageRoot = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        costTextRoot = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        cardNameRoot = this.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();

    }       // Awake()

    void Start()
    {

    }

    [Obsolete("필드 데이터 -> 클래스 참조 데이터")]
    public void SetCardId(CardID cardId_)
    {   // 2024.03.29 수정 [Obsolete 추가]
        //this.cardId = cardId_;
    }       // SetCardId()

    public void UpdateUI()
    {
        costTextRoot.text = datas.cardCost.ToString();
        cardNameRoot.text = datas.cardName.ToString();
        cardImageRoot.sprite = datas.cardSprite;
    }
    public void ClearData()
    {
        this.datas.cardId = default;
        this.datas.cardCost = default;
        this.datas.cardName = null;
        this.datas.cardSprite = null;
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

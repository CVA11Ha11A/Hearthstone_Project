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
    public int cardCost = default;

}


public class DeckInCard : MonoBehaviour
{
    public DeckInCardData datas = null;
    public Image cardImageRoot = null;
    private TextMeshProUGUI costTextRoot = null;
    private TextMeshProUGUI cardNameRoot = null;    

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
    {   // 업데이트 되어야하는 UI를 최신화 하는 함수
        costTextRoot.text = datas.cardCost.ToString();
        cardNameRoot.text = datas.cardName.ToString();
        if (datas.cardSprite != null)
        {
            cardImageRoot.sprite = datas.cardSprite;
        }
        else
        {
            datas.cardSprite = CardManager.cards[this.datas.cardId].GetCardSprite();
            cardImageRoot.sprite = datas.cardSprite;
        }
    }
    public void ClearData()
    {   // datas의 데이터를 기본값으로 설정하는 함수
        this.datas.cardId = default;
        this.datas.cardCost = default;
        this.datas.cardName = null;
        this.datas.cardSprite = null;
    }
    public void CopyToPaste(DeckInCard copyTarget_)
    {   // 타겟을 매개로 보내면 실행한 곳에 카피할 데이터를 붙여넣는 함수
        this.datas.cardId = copyTarget_.datas.cardId;
        this.datas.cardCost = copyTarget_.datas.cardCost;
        this.datas.cardName = copyTarget_.datas.cardName;
        this.datas.cardSprite = copyTarget_.datas.cardSprite;
    }
    public void CopyToPaste(DeckInCard copyTarget_, ref string pasteName_ ,ref string copyName_ )
    {   // 타겟을 매개로 보내면 실행한 곳에 카피할 데이터를 붙여넣는 함수
        DE.Log($"copyTarget_.datas.cardName : {copyTarget_.datas.cardName}");
        this.datas.cardId = copyTarget_.datas.cardId;
        this.datas.cardCost = copyTarget_.datas.cardCost;
        pasteName_ = copyName_;
        this.datas.cardSprite = copyTarget_.datas.cardSprite;
    }
    public void InItDatas(CardID initCardId_)
    {
        this.datas.cardId = initCardId_;
        this.datas.cardCost = CardManager.cards[initCardId_].cost;
        this.datas.cardName = CardManager.cards[initCardId_].cardName;
        this.datas.cardSprite = CardManager.cards[initCardId_].GetCardSprite();
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

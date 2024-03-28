using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckInCard : MonoBehaviour
{
    public Image cardImageRoot = null;
    private TextMeshProUGUI costTextRoot = null;
    private TextMeshProUGUI cardNameRoot = null;
    private CardID cardId = default;                // 중요한 것이기에 Private로 감춤
    

    public string CardName
    {
        get { return this.CardName; }
        set
        {
            if(this.CardName != value)
            {
                this.CardName = value;
            }
            cardNameRoot.text = this.CardName;
        }
    }

        

    private int cardCost = default;
    public int CardCost
    {
        get
        {
            return cardCost;
        }
        set
        {            
            if(cardCost != value)
            {
                cardCost = value;
            }
            costTextRoot.text = CardCost.ToString();
        }
    }

    private void Awake()
    {
        cardImageRoot = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        costTextRoot = this.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        cardNameRoot = this.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();

    }       // Awake()

    void Start()
    {
        
    }

    
    public void SetCardId(CardID cardId_)
    {
        this.cardId = cardId_;
    }       // SetCardId()
    

}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{
    public TextMeshProUGUI[] cardTexts = default;
    public Image cardImage = default;      // 카드의 이미지 -> 주문, 하수인, 무기 공통적으로 필요
    public CardType cardType = default;      // 카드가 주문인지 하수인인지 구별해줄 열거형데이터
    protected ClassCard ClassCard = default;    // 카드가 공통카드인지 직업카드인지 구별해줄 열거형데이터
    public CardRank cardRank = default;         // 카드의 희소성이 어느정도인지 나타내는 열거형데이터

    /// <summary>
    /// 카드들은 고유적인 ID값을 가지며 해당 ID값으로 구별할 것임
    /// 하수인 : 1 ~ 9999 , 주문 : 10000 ~ 20000
    /// </summary>
    public int cardId = default;    
    public int cost = default;

    public string cardName = default;
    public string empect = default;
        
    protected virtual void Awake()
    {        
        GetCardComponents();
    }       // Awake()

    protected virtual void Start()
    {        
    }       // Start()

    public virtual void Empect()
    {
        // PASS 
        // 각 카드 Class에서 구현할것
    }

    /// <summary>
    /// 카드 Id 설정하는 함수 매개변수로 보낸 데이터로 기입됨
    /// </summary>
    /// <param name="cardId_"></param>
    protected void SetCardId(int cardId_)
    {
        this.cardId = cardId_;
    }       // SetCardId()

    protected void SetClassCard(ClassCard classCard_)
    {
        this.ClassCard = classCard_;
    }

    protected void SetCardType(CardType cardType_)
    {
        this.cardType = cardType_;
    }

    protected void SetCardRank(CardRank cardRank_)
    {
        this.cardRank = cardRank_;
    }

   
    protected void GetCardComponents()
    {
        cardTexts = new TextMeshProUGUI[this.transform.GetChild(0).GetChild(1).GetChild(0).childCount];
        cardImage = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        for (int i = 0; i < cardTexts.Length; i++)
        {
            cardTexts[i] = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>();
        }

        cardTexts[(int)C_Text.Name].gameObject.SetActive(true);
        cardTexts[(int)C_Text.Empect].gameObject.SetActive(true);
        cardTexts[(int)C_Text.Cost].gameObject.SetActive(true);

        if (this.cardType == CardType.Minion)
        {
            cardTexts[(int)C_Text.Hp].gameObject.SetActive(true);
            cardTexts[(int)C_Text.Damage].gameObject.SetActive(true);
        }

        else if (this.cardType == CardType.Spell)
        {
            cardTexts[(int)C_Text.Hp].gameObject.SetActive(false);
            cardTexts[(int)C_Text.Damage].gameObject.SetActive(false);
        }
    }       // MinionTextSetting()


    /// <summary>
    /// 텍스트 체력 데미지 코스트 한번에 업데이트 되는 함수
    /// </summary>
    protected virtual void UpdateUI()
    {
        cardTexts[(int)C_Text.Name].text = cardName;
        cardTexts[(int)C_Text.Empect].text = empect;
        cardTexts[(int)C_Text.Cost].text = cost.ToString();

    }       // UpdateUI()
  
}       // Card ClassEnd

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{
    protected StringBuilder sb = default;       // 연산에 필요한 string을 저장해두며 사용할 sb  (!코루틴엔 절대 사용 금지!)
    public TextMeshProUGUI[] cardTexts = default;
    public Image cardImage = default;            // 카드의 이미지 -> 주문, 하수인, 무기 공통적으로 필요         Base Awake에서 초기화
    protected AudioClip[] clips = default;       // 카드의 사운드
    public CardType cardType = default;          // 카드가 주문인지 하수인인지 구별해줄 열거형데이터             Minion, Spell Awake에서 초기화
    protected ClassCard classCard = default;     // 카드가 공통카드인지 직업카드인지 구별해줄 열거형데이터        최종 카드 스크립트에서 초기화
    public CardRank cardRank = default;          // 카드의 희소성이 어느정도인지 나타내는 열거형데이터           최종 카드 스크립트에서 초기화

    /// <summary>
    /// 카드들은 고유적인 ID값을 가지며 해당 ID값으로 구별할 것임
    /// 하수인 : 1 ~ 9999 , 주문 : 10000 ~ 20000
    /// </summary>
    public int cardId = default;                // 카드의 ID                                               최종 카드 스크립트에서 초기화
    public int cost = default;                  // 카드의 코스트                                            최종 카드 스크립트에서 초기화
                                                   
    public string cardName = default;           // 카드의 이름                                              최종 카드 스크립트에서 초기화
    public string empect = default;             // 카드의 효과                                              최종 카드 스크립트에서 초기화
    public string cardNameEn = default;         // 카드의 영문 이름 (영문이름 == Class이름)                   최종 카드 스크립트에서 초기화


    protected virtual void Awake()
    {        
        AwakeInIt();
        GetCardComponents();
        CardManager.Instance.CardSetting(this);
    }       // Awake()

    private void AwakeInIt()
    {       // Awake 에서 초기화 되어야할 종속성이 존재 하지 않은 것
        sb = new StringBuilder();
    }       // AwakeInIt()

    protected virtual void Start()
    {
        
    }       // Start()

    public virtual void Empect()
    {
        // PASS 
        // 각 카드 Class에서 구현할것
    }

    #region Set : ID, ClassCard, CardType, CardRank
    /// <summary>
    /// 카드 Id 설정하는 함수 매개변수로 보낸 데이터로 기입됨
    /// </summary>
    /// <param name="cardId_"></param>
    /// 
    protected void SetCardId(int cardId_)
    {
        this.cardId = cardId_;
    }       // SetCardId()

    protected void SetClassCard(ClassCard classCard_)
    {
        this.classCard = classCard_;
    }

    protected void SetCardType(CardType cardType_)
    {
        this.cardType = cardType_;
    }

    protected void SetCardRank(CardRank cardRank_)
    {
        this.cardRank = cardRank_;
    }
    #endregion #region Set : ID, ClassCard, CardType, CardRank

    protected void GetCardComponents()
    {   // 사용할 컴포넌트를 GetChild를 이용해서 참조하는 함수
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

    protected void GetCardSprite(string cardName_)
    {   // 스프라이트를 리소스폴더에서 가져오는 함수
        string defaultPath = default;
        string fixName = default;
        if (this.cardType == CardType.Minion)
        {
            fixName = "_HS";
            defaultPath = "MinionTextures/";
        }
        else
        {
            fixName = "_SP";
        }
        cardImage.sprite = Resources.Load<Sprite>(defaultPath + cardName_ + fixName);
    }       // GetCardSprite()

    protected virtual void GetAudioClip()    
    {   // 오디오 클립을 리소스 폴더에서 가져오는 함수
        // virtual        
    }

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

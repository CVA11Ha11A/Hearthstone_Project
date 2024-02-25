using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Card : MonoBehaviour
{
    protected Image cardImage = default;      // 카드의 이미지 -> 주문, 하수인, 무기 공통적으로 필요
    protected ClassCard ClassCard = default;

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
        // 상속받은곳에서 구현
    }       // Awake()

    protected virtual void Start()
    {
        // 상속받은곳에서 구현
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

}       // Card ClassEnd

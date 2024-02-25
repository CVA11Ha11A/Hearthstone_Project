using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    /// <summary>
    /// 카드들은 고유적인 ID값을 가지며 해당 ID값으로 구별할 것임
    /// 하수인 : 0 ~ 9999 , 주문 : 10000 ~ 20000
    /// </summary>
    public int cardId;

    public int cost;
    public string cardName;
    public string empect;


    public virtual void Empect()
    {
        // PASS 
        // 각 카드 Class에서 구현할것
    }
}       // Card ClassEnd

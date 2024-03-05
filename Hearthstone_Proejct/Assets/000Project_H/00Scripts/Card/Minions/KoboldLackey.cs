using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldLackey : Minion
{
    protected override void Awake()
    {
        base.Awake();
        SetCardId((int)CardID.KoboldLackey);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(1, 1, 1);
        cardName = "코볼트 졸개";
        empect = "전투의 함성: 피해를 2줍니다.";
        cardNameEn = "KoboldLackey";
        GetCardSprite(cardNameEn);
    }


    protected override void Start()
    {
        base.Start();

    }


    public override void Empect()
    {

    }       // Empect()
}       // ClassEnd

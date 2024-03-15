using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KoboldLackey : Minion
{
    public KoboldLackey()
    {
        SetCardId(CardID.KoboldLackey);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(1, 1, 1);
        cardName = "코볼트 졸개";
        empect = "전투의 함성: 피해를 2줍니다.";
        cardNameEn = "KoboldLackey";
    }

    protected override void Awake()
    {
        base.Awake();                                                        
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

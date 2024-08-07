using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SurlyMob : Spell
{
    public SurlyMob()
    {                
        SetCardId(CardID.SurlyMob);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.S_Epic);
        StatSetting(cost_: 2);
        cardName = "신경질적인 군중";
        empect = "무작위 적 하수인을 처치합니다.";
        cardNameEn = "SurlyMob";
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

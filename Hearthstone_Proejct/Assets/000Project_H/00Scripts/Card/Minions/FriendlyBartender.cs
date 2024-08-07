using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FriendlyBartender : Minion
{
    public FriendlyBartender()
    {
        SetCardId(CardID.FriendlyBartender);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(2, 2, 3);
        cardName = "친근한 바텐더";
        empect = "내 턴이 끝날 때, 내 영웅의 생명력을 1 회복시킵니다.";
        cardNameEn = "FriendlyBartender";
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

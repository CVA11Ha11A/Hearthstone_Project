using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBartender : Minion
{
    protected override void Awake()
    {
        base.Awake();
        SetCardId((int)CardID.FriendlyBartender);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(2, 2, 3);
        cardName = "친근한 바텐더";
        empect = "내 턴이 끝날 때, 내 영웅의 생명력을 1 회복시킵니다.";
        cardNameEn = "FriendlyBartender";
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

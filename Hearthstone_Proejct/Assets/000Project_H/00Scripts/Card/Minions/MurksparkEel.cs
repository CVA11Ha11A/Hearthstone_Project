using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurksparkEel : Minion
{
    public MurksparkEel()
    {
        SetCardId(CardID.MurksparkEel);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(2, 2, 3);
        cardName = "수렁불꽃 뱀장어";
        empect = "전투의 함성: 내 댁에 비용이 짝수인 카드만 있으면, 피해를 2줍니다.";
        cardNameEn = "MurksparkEel";
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

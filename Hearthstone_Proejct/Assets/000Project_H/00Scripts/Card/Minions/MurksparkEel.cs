using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MurksparkEel : Minion
{
    public MurksparkEel()
    {
        //DE.Log($"생성자가 빠른가? ");
        SetCardId(CardID.MurksparkEel);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(2, 2, 3);
        cardName = "수렁불꽃 뱀장어";
        cardNameEn = "MurksparkEel";
        empect = "전투의 함성: 내 댁에 비용이 짝수인 카드만 있으면, 피해를 2줍니다.";

    }       // ctor

    protected override void Awake()
    {        
        base.Awake();
        GetCardSprite(this.cardNameEn);

    }


    protected override void Start()
    {
       
        base.Start();
    }


    public override void Empect()
    {

    }       // Empect()



}       // ClassEnd

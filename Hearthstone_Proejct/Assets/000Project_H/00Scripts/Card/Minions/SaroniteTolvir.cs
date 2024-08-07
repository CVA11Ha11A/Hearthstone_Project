using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaroniteTolvir : Minion
{
    public SaroniteTolvir()
    {
        this.ability = M_Ability.Taunt;
        SetCardId(CardID.SaroniteTolvir);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(4, 3, 5);
        cardName = "사로나이트 톨비르";
        empect = "도발, 이 하수인이 공격받을 때마다 카드를 뽑습니다.";
        cardNameEn = "SaroniteTolvir";
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
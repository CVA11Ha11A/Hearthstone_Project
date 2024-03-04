using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurksparkEel : Minion
{
    protected override void Awake()
    {
        base.Awake();
        SetCardId((int)CardID.MurksparkEel);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(3, 2, 2);
        cardName = "수렁불꽃 뱀장어";
        empect = "전투의 함성: 내 댁에 비용이 짝수인 카드만 있으면, 피해를 2줍니다.";
        cardNameEn = "MurksparkEel";
        GetCardSprite(cardNameEn);
    }


    protected override void Start()
    {
        base.Start();        
    }


    public override void Empect()
    {

    }       // Empect()


}

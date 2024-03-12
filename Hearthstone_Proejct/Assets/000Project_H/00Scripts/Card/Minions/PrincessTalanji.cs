using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessTalanji : Minion
{
    public PrincessTalanji()
    {
        SetCardId(CardID.PrincessTalanji);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Legendry);
        StatSetting(8, 7, 5);
        cardName = "공주 탈란지";
        empect = "전투의 함성: 내 손에서 게임이 시작됐을 때 내 덱에 없던 모든 하수인을 소환합니다.";
        cardNameEn = "PrincessTalanji";
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

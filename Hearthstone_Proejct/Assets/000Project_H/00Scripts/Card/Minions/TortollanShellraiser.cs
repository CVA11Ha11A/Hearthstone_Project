using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TortollanShellraiser : Minion
{
    public TortollanShellraiser()
    {
        SetCardId(CardID.TortollanShellraiser);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(4, 2, 6);
        cardName = "토르톨란 껍질방패병";
        empect = "도발, 죽음의 메아리:무작위 아군 하수인에게 +1/ +1을 부여합니다.";
        cardNameEn = "TortollanShellraiser";
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

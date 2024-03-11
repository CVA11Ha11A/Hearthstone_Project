using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StubbornGastropod : Minion
{
    protected override void Awake()
    {
        base.Awake();
        SetCardId(CardID.StubbornGastropod);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(2, 1, 2);
        cardName = "완강한 복족이";
        empect = "도발, 독성";
        cardNameEn = "StubbornGastropod";
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

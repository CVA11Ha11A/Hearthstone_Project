using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Norgannon : Minion
{

    protected override void Awake()
    {
        base.Awake();
        SetCardId((int)CardID.Norgannon);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Legendry);
        StatSetting(6, 6, 10);
        cardName = "노르간논";
        empect = "마법사 티탄";
        cardNameEn = "Norgannon";
        GetCardSprite(cardNameEn);
    }
    
      
    protected override void Start()
    {
        base.Start();
        

    }


    public override void Empect()
    {

    }       // Empect()



}       // Norgannon ClassEnd

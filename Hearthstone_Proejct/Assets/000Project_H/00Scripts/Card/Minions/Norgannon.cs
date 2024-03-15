using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Norgannon : Minion
{
    public Norgannon()
    {        
        SetCardType(CardType.Minion);

        SetCardId(CardID.Norgannon);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Legendry);
        StatSetting(6, 6, 10);
        cardName = "노르간논";
        empect = "마법사 티탄";
        cardNameEn = "Norgannon";
    }
    protected override void Awake()
    {
        base.Awake();
        #region LEGACY (Awake -> Ctor)
        //SetCardId(CardID.Norgannon);
        //SetClassCard(ClassCard.Common);
        //SetCardRank(CardRank.M_Legendry);
        //StatSetting(6, 6, 10);
        //cardName = "노르간논";
        //empect = "마법사 티탄";
        //cardNameEn = "Norgannon";
        #endregion LEGACY (Awake -> Ctor)
        GetCardSprite(cardNameEn);
    }
    
      
    protected override void Start()
    {
        base.Start();        
        //TestLegendryAudioTest();
    }


    public override void Empect()
    {

    }       // Empect()




}       // Norgannon ClassEnd

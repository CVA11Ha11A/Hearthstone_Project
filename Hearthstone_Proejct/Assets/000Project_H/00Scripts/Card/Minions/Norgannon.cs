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
        cardName = "노르간논";
    }
    
      
    protected override void Start()
    {
        base.Start();
        CardManager.cards.Add(this.cardId, this);
    }


    public override void Empect()
    {

    }       // Empect()



}       // Norgannon ClassEnd

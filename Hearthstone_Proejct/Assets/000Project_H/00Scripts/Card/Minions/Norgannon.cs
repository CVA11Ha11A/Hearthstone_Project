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

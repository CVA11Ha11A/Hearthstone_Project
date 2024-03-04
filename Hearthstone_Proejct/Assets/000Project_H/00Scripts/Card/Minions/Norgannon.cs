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
        StatSetting(10, 6, 6);
        cardName = "노르간논";
        empect = "마법사 티탄";
        cardNameEn = "Norgannon";
        GetCardSprite(cardNameEn);
    }
    
      
    protected override void Start()
    {
        base.Start();
        
        // 사운드 테스트 : Play, Stinger
        //GameObject test = new GameObject("TEST");
        //AudioSource testA = test.AddComponent<AudioSource>();
        //testA.clip = this.clips[(int)M_Clip.Play];
        //testA.Play();
        //GameObject test2 = new GameObject("TEST2");
        //AudioSource testB = test2.AddComponent<AudioSource>();
        //testB.clip = this.clips[(int)M_Clip.Stinger];
        //testB.Play();

    }


    public override void Empect()
    {

    }       // Empect()



}       // Norgannon ClassEnd

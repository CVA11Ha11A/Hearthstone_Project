using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minion : Card
{    
    

    public int damage = default;
    public int heath = default;

    
    

    protected override void Awake()
    {
        base.Awake();        
        SetCardType(CardType.Minion);        
        clips = new AudioClip[Enum.GetValues(typeof (M_Clip)).Length];
        
    }       // Awake()

    protected override void Start()
    {
        base.Start();        
        GetAudioClip();
        UpdateUI();

    }       // Start()


    /// <summary>
    /// 이것도 AddComponent된 이후에 호출 당해야함
    /// </summary>
    protected override void UpdateUI()
    {
        base.UpdateUI();
        cardTexts[(int)C_Text.Hp].text = heath.ToString();
        cardTexts[(int)C_Text.Damage].text = damage.ToString();
    }

    protected void StatSetting(int hp_, int damage_, int cost_)
    {
        this.damage = damage_;
        this.cost = cost_;
        this.heath = hp_;
    }       // StatSetting()

    protected override void GetAudioClip()
    {   // 오디오 소스를 가져오는 함수        
        string defaultAudioPath = "Audios/";        
        sb.Clear().Append(this.cardNameEn + "_Play");        
        clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb);

        sb.Clear().Append(this.cardNameEn + "_Attack");
        clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb);

        sb.Clear().Append(this.cardNameEn + "_Attack");
        clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb);

        sb.Clear();
        if(this.cardRank == CardRank.M_Legendry)
        {
            sb.Append(this.cardNameEn + "_Stinger");
            clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else { /*PASS*/ }
    }       // GetAudioClip()

}       // Minion ClassEnd

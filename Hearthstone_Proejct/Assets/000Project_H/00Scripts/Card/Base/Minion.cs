using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minion : Card
{
    protected M_Ability ability =  default;
    protected int damageDefault = default;
    protected int heathDefault = default;
    public int damage = default;
    public int heath = default;

    public Minion()
    {
        SetCardType(CardType.Minion);        
        clips = new AudioClip[Enum.GetValues(typeof(M_Clip)).Length];
    }


    protected override void Awake()
    {
        base.Awake();
        GetCardComponents();
                        
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

    protected void StatSetting(int cost_, int damage_, int hp_)
    {
        this.damageDefault = damage_;
        this.damage = this.damageDefault;
        this.heathDefault = hp_;
        this.heath = heathDefault;
        this.cost = cost_;
    }       // StatSetting()

    protected override void GetAudioClip()
    {   // 오디오 소스를 가져오는 함수        
        string defaultAudioPath = "Audios/";
        string defaultName = "MinionDefault";
        sb.Clear().Append(this.cardNameEn + "_Play");
        if(clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            sb.Clear().Append(defaultName).Append("_Play");
            clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }

        sb.Clear().Append(this.cardNameEn + "_Attack");
        if(clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            sb.Clear().Append(defaultName).Append("_Attack");
            clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }

        sb.Clear().Append(this.cardNameEn + "_Death");
        if(clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {            
            clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            sb.Clear().Append(defaultName).Append("_Death");
            clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        

        sb.Clear();
        if(this.cardRank == CardRank.M_Legendry)
        {
            sb.Append(this.cardNameEn + "_Stinger");
            if(clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb))
            {
                clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb);
            }
            else
            {
                sb.Clear().Append(defaultName).Append("_Stinger");
                clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb);
            }
        }
        else { /*PASS*/ }
    }       // GetAudioClip()

    protected override void GetCardComponents()
    {
        base.GetCardComponents();
        cardTexts[(int)C_Text.Hp].gameObject.SetActive(true);
        cardTexts[(int)C_Text.Damage].gameObject.SetActive(true);
    }       // GetCardComponents()

}       // Minion ClassEnd

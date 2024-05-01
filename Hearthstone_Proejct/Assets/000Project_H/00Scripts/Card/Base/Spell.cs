using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spell : Card
{
    public Spell()
    {
        SetCardType(CardType.Spell);
        clips = new AudioClip[Enum.GetValues(typeof(S_Clip)).Length];
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

  
    protected override void UpdateUI()
    {
        base.UpdateUI();
    }

    protected void StatSetting(int cost_)
    {        
        this.cost = cost_;        
    }       // StatSetting()

    protected override void GetAudioClip()
    {   // 오디오 소스를 가져오는 함수 ! 조건을 체크해서 없으면 디폴트 주문사운드 가져오도록 할것임
        
        string defaultAudioPath = "Audios/";
        string defaultSpellAudio = "SpellDefualtAudio";
        sb.Clear().Append(this.cardNameEn + "_Play");
        if(clips[(int)S_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)S_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            clips[(int)S_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + defaultSpellAudio + "_Play");
        }

        sb.Clear().Append(this.cardNameEn + "_SpellReady");
        if (clips[(int)S_Clip.SpellReady] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)S_Clip.SpellReady] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            clips[(int)S_Clip.SpellReady] = Resources.Load<AudioClip>(defaultAudioPath + defaultSpellAudio + "_SpellReady");
        }

    }       // GetAudioClip()

    protected override void GetCardComponents()
    {
        base.GetCardComponents();
        cardTexts[(int)C_Text.Hp].gameObject.SetActive(false);
        cardTexts[(int)C_Text.Damage].gameObject.SetActive(false);
    }
}       // Spell ClassEnd

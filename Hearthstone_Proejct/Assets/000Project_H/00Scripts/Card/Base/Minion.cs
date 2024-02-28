using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minion : Card
{    
    protected AudioClip[] clips = default;

    public int damage = default;
    public int heath = default;

    
    

    protected override void Awake()
    {
        base.Awake();
        SetCardType(CardType.Minion);
    }       // Awake()

    protected override void Start()
    {
        base.Start();
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

}       // Minion ClassEnd

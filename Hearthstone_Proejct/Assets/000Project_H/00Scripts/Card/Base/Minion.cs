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

    protected override void UpdateUI()
    {
        base.UpdateUI();
        cardTexts[(int)C_Text.Hp].text = heath.ToString();
        cardTexts[(int)C_Text.Damage].text = damage.ToString();
    }


}       // Minion ClassEnd

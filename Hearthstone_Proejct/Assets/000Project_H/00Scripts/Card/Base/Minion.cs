using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minion : Card
{

    protected TextMeshProUGUI[] minionTexts = default;
    protected AudioClip[] clips = default;

    public int damage = default;
    public int heath = default;

    protected string minionNameText = default;
    protected string minionEmpectText = default;

    protected override void Awake()
    {
        minionTexts = new TextMeshProUGUI[this.transform.GetChild(0).GetChild(1).GetChild(0).childCount];
        cardImage = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        MinionTextsSetting();
    }       // Awake()

    protected override void Start()
    {
        UpdateUI();        
    }       // Start()

    protected void MinionTextsSetting()
    {
        for (int i = 0; i < minionTexts.Length; i++)
        {
            minionTexts[i] = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }       // MinionTextSetting()


    /// <summary>
    /// 텍스트 체력 데미지 코스트 한번에 업데이트 되는 함수
    /// </summary>
    protected virtual void UpdateUI()
    {
        minionTexts[(int)M_Text.Name].text = minionNameText;
        minionTexts[(int)M_Text.Empect].text = minionEmpectText;
        minionTexts[(int)M_Text.Hp].text = heath.ToString();
        minionTexts[(int)M_Text.Damage].text = damage.ToString();
        minionTexts[(int)M_Text.Cost].text = cost.ToString();
    }       // UpdateUI()

}       // Minion ClassEnd

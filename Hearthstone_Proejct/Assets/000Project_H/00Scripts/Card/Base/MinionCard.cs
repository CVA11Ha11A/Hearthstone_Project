using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Play : 소환대사 Attack : 공격 Death : 죽음, Stinger : 소환음악
/// </summary>
public enum M_Clip
{
    Play = 0,
    Attack,
    Death,
    Stinger
}
public enum M_Text
{
    Name = 0,
    Empect,
    Cost,
    Hp,
    Damage
}

public class MinionCard : MonoBehaviour
{


    protected Image minionImage = default;
    protected TextMeshProUGUI[] minionTexts = default;
    protected AudioClip[] clips = default;

    protected string minionNameText;
    protected string minionEmpectText;
    public int damage = default;
    public int heath = default;
    public int cost = default;


    protected virtual void Awake()
    {        
        minionTexts = new TextMeshProUGUI[this.transform.GetChild(0).GetChild(1).GetChild(0).childCount];
        minionImage = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        MinionTextSetting();

    }       // Awake()

    protected void Start()
    {
        UpdateUI();   
    }

    protected void MinionTextSetting()
    {
        
        for(int i = 0; i < minionTexts.Length; i++)
        {
            minionTexts[i] = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }       // MinionTextSetting()

    protected virtual void Empect()
    {

    }       // Empect()

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

}       // MinionCard ClassEnd

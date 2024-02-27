using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public enum CardID
{
    Norgannon = 1
}

public enum CardType
{
    Minion = 0,
    Spell = 1,
    Weapon = 2
}
public enum ClassCard
{
    Common = 0,
    Prist = 1,
    Mage = 2
}

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
public enum C_Text
{
    Name = 0,
    Empect,
    Cost,
    Hp,
    Damage
}

public enum CardRank
{
    M_Rare = 0,
    M_Epic = 1,
    M_Legendry = 2,
    S_Epic = 3
}

public enum C_Material
{
    M_Rare = 0,
    M_Epic = 1,
    M_Legendry = 2,
    S_Epic = 3
}
public enum C_MaskImage
{
    Minion = 0,
    Spell = 1
}

public class CardManager : MonoBehaviour
{
    public static Dictionary<int, Card> cards = default;

    private static CardManager instance = null;
    public static CardManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                GameObject managerObj = new GameObject("CardManager");
                managerObj.AddComponent<CardManager>();
            }
            return instance;
        }
    }

    public Material[] cardOutLineMaterials;
    public Image[] cardMaskImages;

    private void Awake()
    {
        cardMaskImages = new Image[2];
        cardOutLineMaterials = new Material[4];
        ResourceLoad();
        FirstCardSetting();
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void FirstCardSetting()
    {
        if (cards == null)
        {
            cards = new Dictionary<int, Card>
            {
                { (int)CardID.Norgannon, new Norgannon() }
            };
        }

        Debug.Log(cards[(int)CardID.Norgannon].cardName);
    }

    private void Start()
    {
    }


    public void CardSetting(Card card_)
    {
        if (card_.cardType == CardType.Minion)
        {
            MinionSetting(card_);
        }
        else
        {
            SpellSetting(card_);
        }
    }


    private void MinionSetting(Card card_)
    {
        RectTransform rect = new RectTransform();
        Vector3 namePos = new Vector3(0f, -0.2f, 0f);
        Vector3 nameRotation = new Vector3(0f, 0f, 3f);
        Vector3 costPos = new Vector3(0.7f, -0.75f, -0.1f);

        //card_.cardTexts[(int)C_Text.Name].fontStyle   // 폰트 스타일을 어디서 바꾸는지 못찾겠음
        rect = card_.cardTexts[(int)C_Text.Name].GetComponent<RectTransform>();
        rect.anchoredPosition3D = namePos;
        rect.rotation = Quaternion.Euler(nameRotation);

        rect = card_.cardTexts[(int)C_Text.Cost].GetComponent<RectTransform>();
        rect.anchoredPosition3D = costPos;

        GameObject temp = GameManager.Instance.GetTopParent(card_.transform);
        temp.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardMaskImages[(int)C_MaskImage.Minion].sprite;
        temp.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = cardOutLineMaterials[(int)GetCardRank(card_)];

    }       // MinionSetting()
    private void SpellSetting(Card card_)
    {

    }

    private void ResourceLoad()
    {
        cardMaskImages[(int)C_MaskImage.Minion] = Resources.Load<Image>("CardManager/MinionMask");
        cardMaskImages[(int)C_MaskImage.Spell] = Resources.Load<Image>("CardManager/SpellMask_v1");

        cardOutLineMaterials[(int)C_Material.M_Rare] = Resources.Load<Material>("CardManager/Card_Rare");
        cardOutLineMaterials[(int)C_Material.M_Epic] = Resources.Load<Material>("CardManager/Card_Epic");
        cardOutLineMaterials[(int)C_Material.M_Legendry] = Resources.Load<Material>("CardManager/Card_Legendry");
        cardOutLineMaterials[(int)C_Material.S_Epic] = Resources.Load<Material>("CardManager/Card_Spell");

    }       // ResourceLoad()

    private C_Material GetCardRank(Card targetCard_)
    {
        if (targetCard_.cardRank == CardRank.M_Rare)
        {
            return C_Material.M_Rare;
        }
        else if (targetCard_.cardRank == CardRank.M_Epic)
        {
            return C_Material.M_Epic;
        }
        else if (targetCard_.cardRank == CardRank.M_Legendry)
        {
            return C_Material.M_Legendry;
        }
        else
        {
            return C_Material.S_Epic;
        }

    }

}       // CardManager ClassEnd


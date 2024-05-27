using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

/// <summary>
/// 카드들은 고유적인 ID값을 가지며 해당 ID값으로 구별할 것임
/// 하수인 : 1 ~ 9999 , 주문 : 10000 ~ 20000
/// </summary>
public enum CardID
{
    StartPoint = 0,

    // 하수인
    Norgannon = 1,
    MurksparkEel = 2,
    TortollanShellraiser = 3,
    StubbornGastropod = 4,
    KoboldLackey = 5,
    PrincessTalanji = 6,
    FriendlyBartender = 7,
    SaroniteTolvir = 8,

    // 주문
    SurlyMob = 10000,

    EndPoint = int.MaxValue
}

public enum CardType
{
    Minion = 0,
    Spell = 1,
    Weapon = 2
}
public enum ClassCard
{
    None = 0,
    Prist = 1,
    Mage = 2,
    Common,
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

// Battlecry : 전투의 함성, Deathratte : 죽음의메아리, Lifesteal : 생명력 흡수, Discover : 발견, Taunt : 도발, Poisonous : 독성

[Flags]
public enum M_Ability
{
    None = 0,
    Battlecry = 1 << 1,
    Deathratte = 1 << 2,
    Lifesteal = 1 << 3,
    Discover = 1 << 4,
    Taunt = 1 << 5,
    Poisonous = 1 << 6,
    Rush = 1 << 7

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
    S_Epic = 3,
    M_InGameLine = 4

}
public enum C_MaskImage
{
    Minion = 0,
    Spell = 1
}

public enum S_Clip
{
    SpellReady = 0,
    Play = 1
}

public class CardManager : MonoBehaviour
{
    public Dictionary<CardID, Card> cards = default;

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

    public GameObject cardPrefab = default;
    public Material[] cardOutLineMaterials = default;
    public Sprite[] cardMaskSprites = default;
    public Sprite[] classSprites = default;

    #region 카드 사이즈 조절을 위한 Vecotor3들
    // 초기화용
    Vector3 initVector3 = new Vector3(999f, 999f, 999f);
    //하수인
    Vector3 minionEmpectPos = new Vector3(0.0f, 1f, -0.1f);
    Vector3 minionNamePos = new Vector3(0f, -0.2f, 0f);
    Vector3 minionNameRotation = new Vector3(0f, 0f, 3f);
    Vector3 minionCostPos = new Vector3(0.7f, -0.75f, -0.1f);
    Vector3 minionMaskScale = new Vector3(1.82f, 1.82f, 1.82f);
    Vector3 minionCardImageSclae = new Vector3(0.65f, 0.65f, 0.65f);
    // 주문
    Vector3 spellEmpectPos = new Vector3(0.09f, 1f, -0.1f);
    Vector3 spellNamePos = new Vector3(0.09f, -0.08f, -0.1f);
    Vector3 spellCostPos = new Vector3(1f, -0.6f, -0.1f);
    Vector3 spellCardMaskScale = new Vector3(1.15f, 0.9f, 1f);
    Vector3 spellCardImageSclae = new Vector3(1f, 1.2f, 1f);
    #endregion 카드 사이즈 조절을 위한 Vecotor3들




    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            instance.ManagerInIt();
        }
        else if(instance != null)
        {
            Destroy(this.gameObject);
        }


    }

    private void ManagerInIt()
    {
        const int CARD_MASK_SPRITE_COUNT = 2;
        const int CARD_OUTLINE_MATERIAL_COUNT = 5;

        instance = this;
        cardMaskSprites = new Sprite[CARD_MASK_SPRITE_COUNT];
        cardOutLineMaterials = new Material[CARD_OUTLINE_MATERIAL_COUNT];
        classSprites = new Sprite[Enum.GetValues(typeof(ClassCard)).Length];
        ResourceLoad();
        FirstCardSetting();
        DontDestroyOnLoad(this);
    }

    private void FirstCardSetting()
    {   // 처음 카드 메니저 컬렉션에 카드들을 새로 할당하는 함수

        if (cards == null)
        {
            //cards = new Dictionary<CardID, Card> ();
            cards = new Dictionary<CardID, Card>
            {
                // 하수인
                { CardID.Norgannon, new Norgannon() },
                { CardID.MurksparkEel, new MurksparkEel() },
                { CardID.TortollanShellraiser, new TortollanShellraiser() },
                { CardID.StubbornGastropod, new StubbornGastropod() },
                { CardID.KoboldLackey, new KoboldLackey() },
                { CardID.PrincessTalanji, new PrincessTalanji() },
                { CardID.FriendlyBartender, new FriendlyBartender() },
                { CardID.SaroniteTolvir, new SaroniteTolvir() },

                // 주문
                { CardID.SurlyMob, new SurlyMob() },
            };



        }

    }       // FirstCardSetting()




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
    }       // CardSetting()


    private void MinionSetting(Card card_)
    {   // 프리펩을 하수인에 맞도록 셋팅하는 함수
        RectTransform rect;

        //card_.cardTexts[(int)C_Text.Name].fontStyle   // 폰트 스타일을 어디서 바꾸는지 못찾겠음
        rect = card_.cardTexts[(int)C_Text.Name].GetComponent<RectTransform>();
        rect.anchoredPosition3D = minionNamePos;
        rect.rotation = Quaternion.Euler(minionNameRotation);

        rect = card_.cardTexts[(int)C_Text.Cost].GetComponent<RectTransform>();
        rect.anchoredPosition3D = minionCostPos;

        rect = card_.cardTexts[(int)C_Text.Empect].GetComponent<RectTransform>();
        rect.anchoredPosition3D = minionEmpectPos;

        GameObject cardObj = card_.gameObject;
        cardObj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardMaskSprites[(int)C_MaskImage.Minion];
        rect = cardObj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        rect.localScale = minionMaskScale;

        rect = rect.transform.GetChild(0).GetComponent<RectTransform>();
        rect.transform.localScale = minionCardImageSclae;
        cardObj.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = cardOutLineMaterials[(int)GetCardRank(card_)];

    }       // MinionSetting()
    private void SpellSetting(Card card_)
    {
        RectTransform rect;

        rect = card_.cardTexts[(int)C_Text.Name].GetComponent<RectTransform>();
        rect.anchoredPosition3D = spellNamePos;
        rect.rotation = Quaternion.Euler(0f, 0f, 0f);
        // 텍스트 모드 Nomal로 변경해야함

        rect = card_.cardTexts[(int)C_Text.Cost].GetComponent<RectTransform>();
        rect.anchoredPosition3D = spellCostPos;

        rect = card_.cardTexts[(int)C_Text.Empect].GetComponent<RectTransform>();
        rect.anchoredPosition3D = spellEmpectPos;

        GameObject cardObj = card_.gameObject;

        cardObj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = cardMaskSprites[(int)C_MaskImage.Spell];
        rect = cardObj.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>();
        rect.localScale = spellCardMaskScale;

        rect = rect.transform.GetChild(0).GetComponent<RectTransform>();
        rect.transform.localScale = spellCardImageSclae;

        cardObj.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = cardOutLineMaterials[(int)GetCardRank(card_)];
    }       // SpellSetting()

    private void ResourceLoad()
    {   // 카드의 이미지 마스크 , 카드 테두리를 가져오는함수
        cardMaskSprites[(int)C_MaskImage.Minion] = Resources.Load<Sprite>("CardManager/MinionMask");
        cardMaskSprites[(int)C_MaskImage.Spell] = Resources.Load<Sprite>("CardManager/SpellMask_v1");

        cardOutLineMaterials[(int)C_Material.M_Rare] = Resources.Load<Material>("CardManager/Card_Rare");
        cardOutLineMaterials[(int)C_Material.M_Epic] = Resources.Load<Material>("CardManager/Card_Epic");
        cardOutLineMaterials[(int)C_Material.M_Legendry] = Resources.Load<Material>("CardManager/Card_Legendry");
        cardOutLineMaterials[(int)C_Material.S_Epic] = Resources.Load<Material>("CardManager/Card_Spell");
        cardOutLineMaterials[(int)C_Material.M_InGameLine] = Resources.Load<Material>("CardManager/Card_InGameLine");

        cardPrefab = Resources.Load<GameObject>("Card");

        classSprites[(int)ClassCard.Prist - 1] = Resources.Load<Sprite>("ClassSprites/Anduin");
        //DE.Log($"이미지 리소스 Load해옴");
        // TODO : 제이나 제작뒤 여기에 제이나 리소스 추가

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




    public void InItCardComponent(GameObject targetObj_, CardID cardId_)
    {   // 카드의 프리펩에 카드의 기능을 넣어주는 함수        
        targetObj_.AddComponent(cards[cardId_].GetType());

    }       // InItCardComponent()


    public void InItCardComponent(Transform targetTrans_, CardID cardId_)
    {        
        targetTrans_.gameObject.AddComponent(cards[cardId_].GetType());        
    }


}       // CardManager ClassEnd


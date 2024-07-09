using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KoboldLackey : Minion
{
    private int empectDamage = default;

    public KoboldLackey()
    {
        SetCardId(CardID.KoboldLackey);
        SetClassCard(ClassCard.Common);
        SetCardRank(CardRank.M_Rare);
        StatSetting(1, 1, 1);
        this.isPreparation = true;
        this.isBattleCryTarget = true;
        this.empectTargetLayer = 1 << 12 | 1 << 13;
        this.empectDamage = 2;
        cardName = "코볼트 졸개";
        empect = "전투의 함성: 피해를 2줍니다.";
        cardNameEn = "KoboldLackey";
    }

    protected override void Awake()
    {
        base.Awake();
        GetCardSprite(cardNameEn);
    }


    protected override void Start()
    {
        base.Start();
    }


    public override void Empect()
    {
        if (this.isBattleCryTarget == true)
        {
            StartCoroutine(CEmpect());
        }
        else
        {
            // Pass
        }
    }       // Empect()

    protected override void BattlecryEmpect()
    {

    }

    protected override IEnumerator CEmpect()
    {       // 효과 내용 함수
        yield return StartCoroutine(CBattlecryTargetChoice());
        bool isAttackMinion = false;
        if (this.empectTarget.GetComponent<Minion>())
        {
            isAttackMinion = true;
            if (this.empectTarget.transform.parent.parent.CompareTag("Enemy"))
            {
                this.isEmpectTargetEnemy = true;
            }
            else
            {
                this.isEmpectTargetEnemy = false;
            }
            empectTarget.GetComponent<Minion>().Heath -= empectDamage;
        }
        else if (this.empectTarget.GetComponent<HeroImage>())
        {
            isAttackMinion = false;
            if (this.empectTarget.transform.CompareTag("Enemy"))
            {
                this.isEmpectTargetEnemy = true;
            }
            else
            {
                this.isEmpectTargetEnemy = false;
            }
            this.empectTarget.GetComponent<HeroImage>().HeroHp -= empectDamage;
        }

        int targetIdx_ = default;
        if (isAttackMinion == false)
        {
            if (isEmpectTargetEnemy == false)
            {
                targetIdx_ = 200;
            }
            else
            {
                targetIdx_ = 100;
            }
        }
        else
        {
            if (isEmpectTargetEnemy == false)
            {
                targetIdx_ = this.transform.parent.transform.GetSiblingIndex();
                targetIdx_ = targetIdx_ * 10;
            }
            else
            {
                targetIdx_ = this.transform.parent.transform.GetSiblingIndex();
            }
        }

        InGameManager.Instance.MinionBattlecrySync(this.transform.parent.GetSiblingIndex(), targetIdx_);
    }       // CEmpect()

    public override void EmpectSync(int callMinionIdx_, int targetIndex_)
    {
        if (targetIndex_ == 100)
        {
            InGameManager.Instance.mainCanvasRoot.heroImagesRoot.EnemyHeroImage.HeroHp -= empectDamage;
            return;
        }
        else if(targetIndex_ == 200)
        {
            InGameManager.Instance.mainCanvasRoot.heroImagesRoot.MyHeroImage.HeroHp -= empectDamage;
            return;
        }
        
        if(targetIndex_ > 10)
        {
            InGameManager.Instance.mainCanvasRoot.fieldRoot.EnemyField.transform.
                GetChild(targetIndex_ / 10).GetChild(0).GetComponent<Minion>().Heath -= empectDamage;
            return;
        }
        else
        {
            InGameManager.Instance.mainCanvasRoot.fieldRoot.MyField.transform.
                GetChild(targetIndex_).GetChild(0).GetComponent<Minion>().Heath -= empectDamage;
        }
        
    }


}       // ClassEnd

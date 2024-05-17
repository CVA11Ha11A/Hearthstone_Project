using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PristHeroPower : HeroPower
{

    private int healValue = default;

    void Start()
    {
        isNeedTarget = true;
        heroPowerEmpectCost = 2;
        this.healValue = 2;
    }

    public override void TargetHeroPowerEmpect(Transform target_)
    {
        if(target_.GetComponent<HeroImage>())
        {
            target_.GetComponent<HeroImage>().HeroHp += healValue;
        }
        else if(target_.GetComponent<Minion>())
        {
            target_.GetComponent<Minion>().heath += healValue;
        }
        else
        {
            DE.Log($"안두인 영능 타겟에서 HeroImage를 가져올 수 없음");
        }
        
    }       // TargetHeroPowerEmpect()



}

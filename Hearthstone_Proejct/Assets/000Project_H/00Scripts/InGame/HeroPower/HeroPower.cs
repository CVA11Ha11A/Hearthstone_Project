using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPower : MonoBehaviour
{
    public bool isNeedTarget = default;         // 드래그를 해야하는 영능인가
    public int heroPowerEmpectCost = default;   // 지불해야하는 코스트는 얼마인가?

    public virtual void NonTargetHeroPowerEmpect()
    {
        // 타겟을 요구하지 않는 영웅능력
    }

    public virtual void TargetHeroPowerEmpect(Transform target_)
    {
        // 타겟을 요구하는 영웅능력
    }


}       // ClassEnd

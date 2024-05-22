using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPower : MonoBehaviour
{
    public bool isNeedTarget = default;         // 드래그를 해야하는 영능인가
    public int heroPowerEmpectCost = default;   // 지불해야하는 코스트는 얼마인가?
    // 타겟 관련 bool
    protected bool targetIsMinion = false;
    protected bool targetIsHero = false;
    protected bool targetIsEnemy = false;
    protected bool targetIsMy = false;

    private void Awake()
    {
        this.transform.parent.GetComponent<HeroImage>().heroPower = this;
    }

    public virtual void NonTargetHeroPowerEmpect()
    {
        // 타겟을 요구하지 않는 영웅능력
    }

    protected void TargetInIt()
    {
        this.targetIsMinion = false;
        this.targetIsHero = false;
        this.targetIsEnemy = false;
        this.targetIsMy = false;
    }

    protected bool CostCheck()
    {
        if(InGameManager.Instance.mainCanvasRoot.costRoot.MyCost.NowCost >= heroPowerEmpectCost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }       // CostCheck()

    public virtual void TargetHeroPowerEmpect(Transform target_, bool isRPC = false)
    {
        

        // 타겟을 요구하는 영웅능력
        // 여기는 상속받은 곳에서 능력을 실행하고 base를 통해서 호출될것임
        if (isRPC == false)
        {
            // 적인이 아군인지 부터 확인해야함        // 접근해야할 것에 차이가 생김
            // 1 개체가 무엇인지 , Minion, Hero, 2 아군인지 적인지

            TargetInIt();       // 현재 타겟 상황 모두 false로 해줌

            if (target_.GetComponent<HeroImage>())
            {
                targetIsHero = true;
                if(target_.transform.CompareTag("My"))
                {
                    targetIsMy = true;
                }
                else
                {
                    targetIsEnemy = true;
                }
            }
            else if (target_.GetComponent<Minion>())
            {
                targetIsMinion = true;
                if(target_.parent.parent.CompareTag("My"))
                {
                    targetIsMy = true;
                }
                else
                {
                    targetIsEnemy = true;
                }
            }
            // Sync 인자 , bool 4개 int 1개?  누군지 bool , index int 1개

            int targetIndex = -100;
             
            if (targetIsMinion == true)
            {
                targetIndex = target_.parent.GetSiblingIndex();
            }
            else
            {   // 타겟이 영웅 일경우
                if(targetIsMy == true)
                {
                    targetIndex = 200;
                }
                else
                {
                    targetIndex = 100;
                }
            }
            InGameManager.Instance.mainCanvasRoot.costRoot.MyCost.NowCost -= heroPowerEmpectCost;
            InGameManager.Instance.HeroPowerOnTargetSync(targetIsMy, targetIndex);
        }
        else
        {
            InGameManager.Instance.mainCanvasRoot.costRoot.EnemyCost.NowCost -= heroPowerEmpectCost;
        }

    }


}       // ClassEnd

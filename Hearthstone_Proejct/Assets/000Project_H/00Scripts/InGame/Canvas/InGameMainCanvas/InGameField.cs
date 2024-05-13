using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameField : MonoBehaviour
{

    private int nowMinionCount = default;
    public int NowMinionCount
    {
        get
        {
            this.nowMinionCount = this.transform.childCount;
            return this.nowMinionCount;
        }
        set
        {
            if (this.nowMinionCount != value)
            {
                this.nowMinionCount = value;
            }
        }
    }
    public const int MAX_MINON_COUNT = 6;

    private GameObject recentFieldObjRoot = null;
    public GameObject RecentFieldObjRoot
    {
        get
        {
            return this.recentFieldObjRoot;
        }
    }

    private void Awake()
    {
        if (this.transform.name == "MyField")
        {
            this.transform.parent.GetComponent<InGameFields>().MyFieldSetter(this);
        }
        else if (this.transform.name == "EnemyField")
        {
            this.transform.parent.GetComponent<InGameFields>().EnemyFieldSetter(this);
        }
        
        
    }

    void Start()
    {
        


    }

    public void SpawnMinion()
    {   // 하수인 소환전 필드의 자리를 잡아두는 함수
        GameObject fieldObj = new GameObject("FieldObj");
        fieldObj.transform.parent = this.transform;
        fieldObj.AddComponent<RectTransform>();
        fieldObj.transform.localPosition = Vector3.zero;
        recentFieldObjRoot = fieldObj;
    }       // SpawnMinion()

    public bool IsTargetAtteckAble(Transform attackTarget_)
    {
        bool isTaunt = false;
        //DE.Log($"타겟오브젝트의 이름이 무었이지? : {attackTarget_.transform.name}");
        // 아군을 공격하는지 확인
        if(attackTarget_.transform.parent.parent.CompareTag("Enemy"))
        {
            // PASS
        }
        else if(attackTarget_.transform.CompareTag("Enemy"))
        {
            // PASS
        }
        else { DE.Log("대상이 Enemy라는 태그가 아님"); return false; }


        // 도발하수인이 존재하는지 확인
        for(int i = 0; i < this.transform.childCount; i++)
        {
            if((this.transform.GetChild(i).GetChild(0).GetComponent<Minion>().ability & M_Ability.Taunt) == M_Ability.Taunt)
            {   // 도발의 무언가를 가지고 있다면
                isTaunt = true;
                break;
            }
        }

        //내가 도발이 존재하면 내가 공격하는 대상이 도발이 달려있는지
        if(isTaunt == true)
        {   // if : 도발이 존재
            if(attackTarget_.transform.GetComponent<Minion>() == false)
            {   // 도발이 존재하는데 공격하는대상이 Minion컴포넌트가 없다면 공격 불가
                return false;
            }
            if((attackTarget_.transform.GetComponent<Minion>().ability & M_Ability.Taunt) == M_Ability.Taunt)
            {
                return true;
            }
        }
        DE.Log($"공격가능? 함수 진입 결과\n 도발하수인 존재하지 않음 공격 대상 정상 판정");
        return true;

    }       // IsTargetAtteckAble()

    public bool IsTargetAtteckAble(GameObject attackTarget_)
    {
        bool isTaunt = false;

        // 아군을 공격하는지 확인
        if (attackTarget_.transform.parent.parent.CompareTag("Enemy"))
        {
            // PASS
        }
        else { DE.Log("대상이 Enemy라는 태그가 아님"); return false; }


        // 도발하수인이 존재하는지 확인
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if ((this.transform.GetChild(i).GetChild(0).GetComponent<Minion>().ability & M_Ability.Taunt) == M_Ability.Taunt)
            {   // 도발의 무언가를 가지고 있다면
                isTaunt = true;
                break;
            }
        }

        //내가 도발이 존재하면 내가 공격하는 대상이 도발이 달려있는지
        if (isTaunt == true)
        {   // if : 도발이 존재
            if (attackTarget_.transform.GetComponent<Minion>() == false)
            {   // 도발이 존재하는데 공격하는대상이 Minion컴포넌트가 없다면 공격 불가
                return false;
            }
            if ((attackTarget_.transform.GetComponent<Minion>().ability & M_Ability.Taunt) == M_Ability.Taunt)
            {
                return true;
            }
        }
        DE.Log($"공격가능? 함수 진입 결과\n 도발하수인 존재하지 않음 공격 대상 정상 판정");
        return true;

    }       // IsTargetAtteckAble()

}       // ClassEnd

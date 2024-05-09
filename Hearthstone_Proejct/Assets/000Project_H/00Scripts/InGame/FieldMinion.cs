using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMinion : MonoBehaviour
{   // 필드에 소환되었을때 하수인의 기능을 담당하는 컴포넌트

    private bool isAttack = false;          // 공격이 가능한 상태인지
    public bool alreadyAttacked = false;    // 해당턴에 이미 공격을 했는지
    private int maxHP = 100;
    private int hp = 100;
    private int damage = 100;

    private void Awake()
    {
        InGameManager.Instance.gameSycleRoot.MinionAttackPossibleEvent += IsAttackTrue;
    }

    void Start()
    {
        if(this.transform.GetComponent<Minion>().ability == M_Ability.Rush)
        {   // 돌진이면 바로 공격가능
            this.isAttack = true;
        }
        hp = this.transform.GetComponent<Minion>().heath;
        damage = this.transform.GetComponent<Minion>().damage;
        maxHP = hp;

    }

    private void IsAttackTrue(bool attackPossible_)
    {
        this.isAttack = attackPossible_;
        this.alreadyAttacked = attackPossible_;
    }

    private void OnDestroy()
    {
        InGameManager.Instance.gameSycleRoot.MinionAttackPossibleEvent -= IsAttackTrue;
    }

    public void HealHp(int healValue_)
    {        // HP힐을  할때 호출되어야하는 함수
        hp += healValue_;
        if(hp >= maxHP)
        {
            hp = maxHP;
        }
    }       // HealHp()

    /// <summary>
    /// 공격받을 경우 호출되야하는 함수
    /// </summary>
    /// <param name="damage_">받는 데미지</param>
    public void BeAttacked(int damage_)
    {
        hp -= damage_;
    }       // BeAttacked()


}       // ClassEnd()
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{   // 데미지를 받을 수 있는 개체인지    


    public void IAttacked(int damage_);

    public IEnumerator CIAttackAnime(Transform targetTrans_);

}       // interfaceEnd

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InGameSycle : MonoBehaviourPun
{       // 게임의 사이클을 관리해줄 Class

    private PhotonView PV = null;
    private void Awake()
    {
        this.PV = GetComponent<PhotonView>();
    }
    void Start()
    {

    }

    public void StartMulligan()
    {       // 로컬에서만 할까? 아니면 실시간으로 계속 받을까? 핸드 관리는 적도 강제 드로우 시키고 볼까?
        // 1.드로우 시스템 제작  (Deck이 가지고 있음)
        // 2.선공 후공에 따라 카드를 드로우시킴
        // 3. EnemyDrow를 제작해서 내가 드로우하면 상대도 로컬에서 드로우 시키는 기능이 필요할듯(씬 내부에서 Standard를 X -180 으로 돌려놔서 안돌려도 될듯)

        StartCoroutine(CTest());
    }
    private void OnDestroy()
    {

    }

    IEnumerator CTest()
    {
        for (int i = 0; i < 6; i++)
        {
            InGameManager.Instance.InGameMyDeckRoot.DrawCard();
            yield return new WaitForSeconds(3f);
        }
    }

}       // ClassEnd

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;

public class InGameSycle : MonoBehaviourPun
{       // 게임의 사이클을 관리해줄 Class
        
    private PhotonView PV = null;

    // 멀리건 변수
    private bool isMyMulliganCompleat = false;
    private bool isEnemyMulliganCompleat = false;

    // 자기 자신의 턴이 무엇인지는 InGameManager가 들고 있으며 해당 컴포넌트에서는 현재턴, 다음턴 턴 실행시 무었을 할지만 정해줌
    private ETurn nowTurn = ETurn.StartPoint;
    public ETurn NowTurn
    { 
        get
        {
            return this.nowTurn;
        }
        set
        {
            if(this.nowTurn != value)
            {
                this.nowTurn = value;
            }

        }
    }

    public event Action MinionAttackPossibleEvent;    // 소환되는 하수인은 해당 이벤트를 구독하며 자신의 턴 시작시 해당 이벤트가 호출 될것임

    private void Awake()
    {        
        this.PV = GetComponent<PhotonView>();
        isEnemyMulliganCompleat = false;
        isMyMulliganCompleat = false;
    }
    void Start()
    {
        InGameManager.Instance.gameSycleRoot = this;   
    }
    private void OnDestroy()
    {

    }


    public void StartMulligan()
    {       // 로컬에서만 할까? 아니면 실시간으로 계속 받을까? 핸드 관리는 적도 강제 드로우 시키고 볼까?
        // 1.드로우 시스템 제작  (Deck이 가지고 있음)
        // 2.선공 후공에 따라 카드를 드로우시킴
        // 3. EnemyDrow를 제작해서 내가 드로우하면 상대도 로컬에서 드로우 시키는 기능이 필요할듯(씬 내부에서 Standard를 X -180 으로 돌려놔서 안돌려도 될듯)
        // 영웅 HP 이미지 켜기

        InGameManager.Instance.mainCanvasRoot.heroImagesRoot.MyHeroImage.HPImageOn();
        InGameManager.Instance.mainCanvasRoot.heroImagesRoot.EnemyHeroImage.HPImageOn();
        InGameManager.Instance.mainCanvasRoot.heroImagesRoot.MyHeroImage.HeroPowerSetting();
        InGameManager.Instance.mainCanvasRoot.heroImagesRoot.EnemyHeroImage.HeroPowerSetting();

        InGameManager managerRoot = InGameManager.Instance;
        managerRoot.mainCanvasRoot.transform.GetChild(0).GetChild(7).GetComponent<DiscoveryCanvas>()
            .Mulligan(managerRoot.InGameMyDeckRoot.InGamePlayerDeck.cardList[0], managerRoot.InGameMyDeckRoot.InGamePlayerDeck.cardList[1],
            managerRoot.InGameMyDeckRoot.InGamePlayerDeck.cardList[2]);


    }       // StartMulligan()



    public void TurnStart()
    {
        if(InGameManager.Instance.TurnSystem == this.NowTurn)
        {   // 자신의 턴이라면            
            StartCoroutine(CTurnSetting());
            this.MinionAttackPossibleEvent?.Invoke();
        }
        else
        {   // 자신의 턴이 아니라면
            // 카드와의 상호작용을 불가능 하도록(카드 내기불가능)
            // 상대방의 UI를 상대방입장에서 로컬로 증가 감소 시키기
            InGameManager.Instance.mainCanvasRoot.costRoot.EnemyCost.TurnStartCostSetting();
            InGameManager.Instance.mouseRoot.transform.GetComponent<MyTurnMouse>().enabled = false;
            //InGameManager.Instance.mouseRoot.transform.GetComponent<MyTurnMouse>().enabled = true;  // Test
            

        }
    }       // TurnStart()


    private IEnumerator CTurnSetting()
    {
        // 턴시작 UI 애니메이션 실행
        yield return StartCoroutine(InGameManager.Instance.mainCanvasRoot.turnUIRoot.CYourTurnAnime());
        InGameManager.Instance.mainCanvasRoot.costRoot.MyCost.TurnStartCostSetting();   // NowMaxCost++후 현재 코스트를 nowMaxCost로 하는 기능
        InGameManager.Instance.mouseRoot.transform.GetComponent<MyTurnMouse>().enabled = true;
        InGameManager.Instance.InGameMyDeckRoot.DrawCard(); // 드로우  [드로우 내부에서 동기화 함]
    }       // CTurnSetting()


    
}       // ClassEnd

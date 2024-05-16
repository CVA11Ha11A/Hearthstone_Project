using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using UnityEngine.AI;

public enum ETarGet
{
    My = 100,
    Enemy = 200
}
public enum ETurn
{
    StartPoint = 0,
    GoFirst = 1,
    GoSecond = 2,
    EndPoint = 3
}



public class InGameManager : MonoBehaviourPunCallbacks
{   // 인게임에서 필요한 사이클 , 덱 초기화 , 랜덤 등 동기화되어야하는 기능들을 담을 것
    // ! 여러 군데에서 IngameManager의 도움을 받아서 기능수행을 할 것이기 때문에 최대한 프로퍼티 활용으로 참조 목록 확인 가능하도록 제작

    #region Roots
    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("InGameManager");
                obj.AddComponent<InGameManager>();
                obj.AddComponent<InGameSycle>();
                obj.AddComponent<PhotonView>();
            }
            return instance;
        }
    }

    public InGameMainCanvas mainCanvasRoot = null;
    public FrontGroundCanvas frontCanvas = null;

    private StringBuilder sb = null;

    private InGameDeck inGameMyDeckRoot = null;
    public InGameDeck InGameMyDeckRoot
    {
        get
        {
            if (this.inGameMyDeckRoot == null)
            {   // 만약 해당 컴포넌트보다 InGameDeck의 컴포넌트가 먼저 라이프 사이클을 돈다면 들어올 조건문
                this.inGameMyDeckRoot = GameObject.Find("InGameMainCanvas").transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<InGameDeck>();
                return this.inGameMyDeckRoot;
            }
            else
            {
                return this.inGameMyDeckRoot;
            }
        }

        set
        {
            if (this.inGameMyDeckRoot != value)
            {
                this.inGameMyDeckRoot = value;
            }
        }
    }

    private InGameDeck inGameEnemyDeckRoot = null;
    public InGameDeck InGameEnemyDeckRoot
    {
        get
        {
            if (this.inGameEnemyDeckRoot == null)
            {
                this.inGameEnemyDeckRoot = transform.Find("MyDeck").GetComponent<InGameDeck>();
                return this.inGameEnemyDeckRoot;
            }
            else
            {
                return this.inGameEnemyDeckRoot;
            }
        }
        set
        {
            if (this.inGameEnemyDeckRoot != value)
            {
                this.inGameEnemyDeckRoot = value;
            }
        }
    }

    public InGameSycle gameSycleRoot = null;

    private PhotonView PV = null;

    public Mouse mouseRoot = null;


    #endregion Roots


    public bool isCompleatMyDeckInit = false;
    public bool isCompleatEnemyDeckInit = false;

    public bool isCompleateMyMulligan = false;
    public bool isCompleateEnemyMulligan = false;

    public bool[] masterClientWaiters = null;
    private ETurn turnSystem = default;
    public ETurn TurnSystem
    {
        get
        {
            return this.turnSystem;
        }
        set
        {
            if (this.turnSystem != value)
            {
                this.turnSystem = value;

            }
        }
    }


    private void Awake()
    {


        //PhotonNetwork.AllocateViewID(this.PV);

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }

    }
    void Start()
    {
        this.sb = new StringBuilder();
        this.PV = this.transform.GetComponent<PhotonView>();
        this.PV.observableSearch = PhotonView.ObservableSearch.AutoFindAll;
        this.PV.ViewID = 700;

        int enumCount = 10;
        this.masterClientWaiters = new bool[enumCount];
        for (int i = 0; i < enumCount; i++)
        {
            this.masterClientWaiters[i] = false;
        }

        FirstPlayersDeckInit();
    }




    #region 플레이어들의 덱 초기화 관련


    public void FirstPlayersDeckInit()
    {   // 각자 자신의 덱 연산후 상대에게 자신의 덱을 보내주며 초기화시키는 함수

        // 1. 내덱을 렌덤하게 섞어야함 여기서 Cardid가 StartPoint || EndPoint 는 제외되어야함 (상대덱도 포함)
        // 2. 내덱(MasterClient)을 string으로 붙여서 자신 덱 초기화 하는 함수 실행
        // 3. 상대덱을 string으로 붙여서 상대 덱 초기화 하는 함수 실행
        // ! 상대에게 보낼때는 MasterClient기준으로 내 덱이 상대에겐 상대덱임 !

        //GameManager.Instance.inGamePlayersDeck.TOutPutDeck(ETarGet.My);
        //GameManager.Instance.inGamePlayersDeck.TOutPutDeck(ETarGet.Enemy);

        List<CardID> deckList = new List<CardID>(35);   // 최대 30장까지 초기화 할것이기 때문에 여유있게 할당        
        Deck myDeckRoot = GameManager.Instance.inGamePlayersDeck.MyDeck;
        int existenceCardCount = default;
        //DE.Log($"1 : 어느 for문에서 멈출까?");
        for (int i = 0; i < myDeckRoot.cardList.Length; i++)
        {
            if (myDeckRoot.cardList[i] == CardID.StartPoint || myDeckRoot.cardList[i] == CardID.EndPoint)
            {   // 카드로 존재하지 않는 것이라면 다음으로 넘어가기
                continue;
            }
            else
            {
                deckList.Add(myDeckRoot.cardList[i]);
                existenceCardCount++;
            }
        }

        // 여기선 리스트에 마스터클라이언트 덱에 카드가 존재하는 것들만 들어가 있을 것임            
        // 1. 여기서 나의 덱을 섞어야함
        int shuffleCount = 5;   // 카드를 몇변 for문 돌리면서 랜덤하게 섞을지
        int nowShuffleCount = default;  // 0  아래 for문에서 현재 몇번 셔플 했는지 
        int shuffleIndex = default;     // 섞일 시작할 카드 아래 For문에서 Random으로 결정될 변수
        int shuffleGoalIndex = default; // 위에서 지정된 idnex가 들어갈 index
        CardID tempCardId = default;
        for (int i = 0; i < existenceCardCount; i++)
        {
            shuffleIndex = Random.Range(0, existenceCardCount);
            tempCardId = deckList[shuffleIndex];
            shuffleGoalIndex = Random.Range(0, existenceCardCount);
            deckList[shuffleIndex] = deckList[shuffleGoalIndex];
            deckList[shuffleGoalIndex] = tempCardId;
            nowShuffleCount++;
            // 위에 sb가 이 아래로 존재 해야 할 거갗다고도 생각이 듦
            // 왜냐하면 섞고 결과물을 Client에게 보내야하는데 위에 섞지 않은 결과를 보내면
            // 고정된 순서에 카드가 나올것임
            if (i + 1 == existenceCardCount && nowShuffleCount < shuffleCount)
            {
                i = 0;
            }
        }   // For : 카드 셔플 

        // 2. 카드를 sb를 이용해서 string화 시킴
        sb.Clear();
        int addCardId = -1;
        for (int i = 0; i < deckList.Count; i++)
        {
            addCardId = (int)deckList[i];
            if (i + 1 >= deckList.Count)
            {   // 다음 순회 조건에서 조건이 맞지 않은 경우
                // 뒤에 ,를 뺴야함
                sb.Append(addCardId);
            }
            else
            {
                sb.Append(addCardId);
                sb.Append(",");
            }
        }   // for : 덱속 카드 sb에 추가하는 for

        // 3. 상대에게 덱 초기화 하라고 함수 실행 여기선 StringBuilder는 Id , Id , Id 구조를 가지게됨
        IngameDeckInitalize(sb.ToString(), ETarGet.My);
        this.isCompleatMyDeckInit = true;
        //DE.Log($"어떤 것이 NUll이지?\nSb : {sb == null}, PhotonView : {PV == null}");
        this.PV.RPC("IngameDeckInitalize", RpcTarget.Others, sb.ToString(), ETarGet.Enemy);

        // 여기서 이제 게임을 시작

        //StartCoroutine(Tess());


    }       // FirstPlayersDeckInit()


    [PunRPC]
    public void IngameDeckInitalize(string deckIdsStr_, ETarGet initTarget_)
    {   // 인게임에서 상대덱을 초기화 하는 함수
        string[] deckCardIdArr = deckIdsStr_.Split(',');

        if (initTarget_ == ETarGet.Enemy)
        {
            this.InGameEnemyDeckRoot.DeckInit(deckCardIdArr, initTarget_);
            mainCanvasRoot.heroImagesRoot.EnemyHeroImage.HeroSetting();
        }
        else if (initTarget_ == ETarGet.My)
        {
            this.InGameMyDeckRoot.DeckInit(deckCardIdArr, initTarget_);
            mainCanvasRoot.heroImagesRoot.MyHeroImage.HeroSetting();
        }
        else
        {
            DE.LogError($"잘못된 Target : {initTarget_}");
        }
    }   // IngameEnemyDeckInitalize()


    /// <summary>
    /// 덱을 현재 덱의 CurrentIndex만큼 순회하며 섞는 함수
    /// </summary>
    /// <param name="targetDeck_">카드가 섞일 덱의 Root</param>
    /// <param name="shuffleTarget">섞는 타겟을 보내야함</param>
    [PunRPC]
    public void ShuffleCards(InGameDeck targetDeck_, ETarGet shuffleTarget)
    {       // 카드를 섞는 함수 ! 덱이 초기화 되어있다는 가정하에 제작된 함수임
            // 

        int shuffleCount = 5;   // 카드를 몇변 for문 돌리면서 랜덤하게 섞을지
        int nowShuffleCount = default;  // 0  아래 for문에서 현재 몇번 셔플 했는지 
        int shuffleIndex = default;     // 섞일 시작할 카드 아래 For문에서 Random으로 결정될 변수
        int shuffleGoalIndex = default; // 위에서 지정된 idnex가 들어갈 index
        CardID tempCardId = default;

        for (int i = 0; i < targetDeck_.InGamePlayerDeck.currentIndex; i++)
        {
            shuffleIndex = Random.Range(0, targetDeck_.InGamePlayerDeck.currentIndex);
            tempCardId = targetDeck_.InGamePlayerDeck.cardList[shuffleIndex];

            shuffleGoalIndex = Random.Range(0, targetDeck_.InGamePlayerDeck.currentIndex);
            targetDeck_.InGamePlayerDeck.cardList[shuffleIndex] = targetDeck_.InGamePlayerDeck.cardList[shuffleGoalIndex];

            targetDeck_.InGamePlayerDeck.cardList[shuffleGoalIndex] = tempCardId;
            nowShuffleCount++;
            // 위에 sb가 이 아래로 존재 해야 할 거갗다고도 생각이 듦
            // 왜냐하면 섞고 결과물을 Client에게 보내야하는데 위에 섞지 않은 결과를 보내면
            // 고정된 순서에 카드가 나올것임
            if (i + 1 == targetDeck_.InGamePlayerDeck.currentIndex && nowShuffleCount < shuffleCount)
            {
                i = 0;
            }
        }   // For : 카드 셔플 

        // 이제 새로운 덱을 string화 시켜서 다른사람에게 넘겨주고 새롭게 초기화된 덱으로 변경 시켜야함
        sb.Clear();
        for (int i = 0; i < targetDeck_.InGamePlayerDeck.currentIndex; i++)
        {
            if (i + 1 == targetDeck_.InGamePlayerDeck.currentIndex)
            {   // 마지막 순회라면
                sb.Append((int)targetDeck_.InGamePlayerDeck.cardList[i]);
            }
            else
            {
                sb.Append((int)targetDeck_.InGamePlayerDeck.cardList[i]);
                sb.Append(',');
            }
        }   // for : 새로 섞인 덱을 string으로 합쳐주는 순회

        if (shuffleTarget == ETarGet.My)
        {
            PV.RPC("ChangedDeckSet", RpcTarget.Others, sb.ToString(), ETarGet.My);
        }
        else
        { // (shuffleTarget == ETarGet.Enemy)
            PV.RPC("ChangedDeckSet", RpcTarget.Others, sb.ToString(), ETarGet.Enemy);
        }

    }       // ShuffleCards()


    [PunRPC]
    public void ChangedDeckSet(string changedCardIdsStr_, ETarGet changedTarget)
    {       // ShuppleCards() 가 RPC로 호출해줄 함수

        string[] splitCardIds = changedCardIdsStr_.Split(',');

        if (changedTarget == ETarGet.My)
        { // 호출자 기준으로 자신이기에 적의 Root를 접근해야함
            this.InGameEnemyDeckRoot.ChangeDeckInIt(splitCardIds);
        }
        else
        { // 호출자 기준으로 적이기에 자신의 Root를 접근해야함
            this.InGameMyDeckRoot.ChangeDeckInIt(splitCardIds);
        }
    }       // ChangedDeckSet()

    #endregion 플레이어들의 덱 초기화 관련

    #region 덱 초기화이후 첫 인사 까지
    [PunRPC]
    public void GameStart()
    {   // 여기서 선공, 후공이 정해질 것임
        this.turnSystem = (ETurn)Random.Range((int)ETurn.GoFirst, (int)ETurn.EndPoint);     // 턴 설정
        ETurn enemyTurn = default;
        if (this.turnSystem == ETurn.GoFirst)
        {
            enemyTurn = ETurn.GoSecond;
        }
        else
        {
            enemyTurn = ETurn.GoFirst;
        }
        PV.RPC("TurnSet", RpcTarget.Others, (int)enemyTurn);  // 상대방의 턴 설정
        PV.RPC("FristGreeting", RpcTarget.All);

    }



    public void IsCompleateDeckInItCheck()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CWaitForDeckInIt());
        }
        else
        {
            ClientEnemyDeckInItCompleat();
        }
    }

    IEnumerator CWaitForDeckInIt()
    {
        //DE.Log($"마스터 클라이언트가 여기는 잘 들어오나?");
        while (isCompleatEnemyDeckInit == false || isCompleatMyDeckInit == false)
        {
            yield return null;
        }
        PV.RPC("CallFadeIn", RpcTarget.All);

        if (PhotonNetwork.IsMasterClient)
        {
            GameStart();
        }
    }
    [PunRPC]
    public void ClientEnemyDeckInItCompleat()
    {
        if (PhotonNetwork.IsMasterClient != true)
        {
            PV.RPC("ClientEnemyDeckInItCompleat", RpcTarget.MasterClient);
        }
        else if (PhotonNetwork.IsMasterClient == true)
        {
            this.isCompleatEnemyDeckInit = true;
        }
    }

    [PunRPC]
    public void CallFadeIn()
    {
        this.frontCanvas.FadeIn();
    }
    #region 테스트 함수

    private IEnumerator Tess()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("적의 덱을 출력");
        this.InGameEnemyDeckRoot.TestOutPut();
        Debug.Log("나의 덱을 출력");
        this.InGameMyDeckRoot.TestOutPut();
        ShuffleCards(this.InGameMyDeckRoot, ETarGet.My);
        yield return new WaitForSeconds(5f);
        Debug.Log($"새로 덱을 섞은후");
        Debug.Log("적의 덱을 출력");
        this.InGameEnemyDeckRoot.TestOutPut();
        Debug.Log("나의 덱을 출력");
        this.InGameMyDeckRoot.TestOutPut();

    }

    #endregion  테스트 함수

    [PunRPC]
    public void FristGreeting()
    {
        StartCoroutine(CFIrstGreeting());
    }

    IEnumerator CFIrstGreeting()
    {
        // 내 직업에 접근해서 인사 음성 출력시켜야함
        AudioManager.Instance.PlaySFM(false, mainCanvasRoot.heroImagesRoot.MyHeroImage.EmoteClip[(int)EEmoteClip.Start]);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlaySFM(false, mainCanvasRoot.heroImagesRoot.EnemyHeroImage.EmoteClip[(int)EEmoteClip.Start]);
        yield return new WaitForSeconds(2f);
        // 인사끝 선공 후공에 따라 카드 뽑고 멀리건 교체할지 선택하는 기능 실행되어야함
        // 여기부터 InGameSycle Class에서 관리하며 기능 실행할거임
        this.transform.GetComponent<InGameSycle>().StartMulligan();
    }
    #endregion 덱 초기화이후 첫 인사 까지



    #region 멀리건
    [PunRPC]
    public void DrawEnemy()
    {
        this.InGameEnemyDeckRoot.EnemyDrawCard();
    }


    public void CallEnemyMulliganDraw(int drawCard_)
    {
        PV.RPC("MulliganDraw", RpcTarget.Others, drawCard_);
    }

    [PunRPC]
    public void MulliganDraw(int drawCardId)
    {
        this.InGameEnemyDeckRoot.EnemyDrawCard(drawCardId);
    }

    #endregion 멀리건


    #region 인게임 기능 함수들
    [PunRPC]
    public void TurnSet(int turn_)
    {
        this.turnSystem = (ETurn)turn_;
    }

    public void DrawCard()
    {   // 상대 기준으로 드로우 시키는 함수
        // 드로우 시키는 함수를 만들어서 여기서 Call해야할거같음
        // 누가 함수를 가져야하지? 덱? 카드? 메니저? 핸드?    : 덱

        this.InGameMyDeckRoot.DrawCard();
        PV.RPC("DrawEnemy", RpcTarget.Others);
    }       // DrawCard


    #endregion 인게임 기능 함수들

    #region 멀리건 대기 함수
    public void MasterClientMulliganWait()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(CMasterClientMulliganWait());
        }
    }

    IEnumerator CMasterClientMulliganWait()
    {
        while (this.isCompleateMyMulligan != true || this.isCompleateEnemyMulligan != true)
        {
            yield return null;
        }
        PV.RPC("StartTurnSet", RpcTarget.All);
        // 여기서 턴 시작하면됨
    }

    [PunRPC]
    public void StartTurnSet()
    {   // 모든 플레이어의 첫 시작 턴을 선공에게 주는 함수
        this.transform.GetComponent<InGameSycle>().NowTurn = ETurn.GoFirst;
        this.transform.GetComponent<InGameSycle>().TurnStart();
    }

    public void CompleateMulligan()
    {
        if (PhotonNetwork.IsMasterClient == true)
        {
            this.isCompleateMyMulligan = true;
            mouseRoot.isRayCast = true;
        }
        else
        {
            this.isCompleateMyMulligan = true;
            PV.RPC("CompleateMulliganSetter", RpcTarget.MasterClient, this.isCompleateMyMulligan);
            mouseRoot.isRayCast = true;
        }
    }
    [PunRPC]
    public void CompleateMulliganSetter(bool isCompleate_)
    {
        this.isCompleateEnemyMulligan = isCompleate_;
    }



    #endregion 멀리건
    #region 동기화 함수
    public void DrawSync()
    {   // 플레이어가 드로우 할떄 상대입장에서도 상대드로우 똑같이 기능 실행해주는 함수
        PV.RPC("DrawSyncRPC", RpcTarget.Others, null);
    }
    [PunRPC]
    public void DrawSyncRPC()
    {
        InGameEnemyDeckRoot.DrawCardCallRPC();
    }

    public void ThrowMinionSync(int targetHandCardIndex_)
    {
        PV.RPC("ThrowMinionSyncRPC", RpcTarget.Others, targetHandCardIndex_);
    }

    [PunRPC]
    public void ThrowMinionSyncRPC(int targetHandCardIndex_)
    {       // 적입장에서 카드를 내는것이기 떄문에 적핸드의 타겟카드가 적의 필드로 소환되어야함
        mainCanvasRoot.fieldRoot.EnemyField.SpawnMinion();

        mainCanvasRoot.handRoot.EnemyHand.RemoveCardInHand(mainCanvasRoot.handRoot.EnemyHand.transform.
            GetChild(0).GetChild(targetHandCardIndex_).gameObject);

        mainCanvasRoot.handRoot.EnemyHand.transform.GetChild(0).GetChild(targetHandCardIndex_).transform.rotation = Quaternion.Euler(0, 0, 0);

        mainCanvasRoot.handRoot.EnemyHand.transform.GetChild(0).GetChild(targetHandCardIndex_).GetComponent<Card>().
            MinionFieldSpawn(mainCanvasRoot.fieldRoot.EnemyField.RecentFieldObjRoot);


    }

    public void TurnEndSync()
    {
        ETurn turnParam = default;
        if (this.TurnSystem == ETurn.GoFirst)
        {
            turnParam = ETurn.GoSecond;
        }
        else
        {
            turnParam = ETurn.GoFirst;
        }
        PV.RPC("TurnEndSyncRPC", RpcTarget.All, (int)turnParam);

    }
    [PunRPC]
    public void TurnEndSyncRPC(int turnParam_)
    {
        gameSycleRoot.NowTurn = (ETurn)turnParam_;
        gameSycleRoot.TurnStart();

    }


    // 하수인 공격 동기화
    public void MinionAttackSync(int attackObjChildNum_, int attackedObjChildNum_)
    {
        PV.RPC("MinionAttackSyncRPC", RpcTarget.Others, attackObjChildNum_, attackedObjChildNum_);
    }


    [PunRPC]
    public void MinionAttackSyncRPC(int attackObjChildNum_, int attackedObjChildNum_)
    {

        //DE.Log($"인자로 넘어온 수\n공격하는 하수인 : {attackObjChildNum_}, 공격 받는 하수인 : {attackedObjChildNum_}");
        Transform attackedTrans = null;
        // 100 이라면 영웅을 때리는것임
        if (attackedObjChildNum_ == 100)
        {
            attackedTrans = mainCanvasRoot.heroImagesRoot.MyHeroImage.transform;
        }
        else
        {
            attackedTrans = mainCanvasRoot.fieldRoot.MyField.transform.GetChild(attackedObjChildNum_).GetChild(0).transform;
        }

        // 공격할 하수인 구해야함
        //DE.Log($"공격하는 것의 Name : {mainCanvasRoot.fieldRoot.EnemyField.transform.GetChild(attackObjChildNum_).GetChild(0).name}\n공격 받는것의 이름 : {attackedTrans.name}");
        StartCoroutine(mainCanvasRoot.fieldRoot.EnemyField.transform.GetChild(attackObjChildNum_).
            GetChild(0).GetComponent<Minion>().CIAttackAnime(attackedTrans, isRPC: true));
        

    }

    public void GameEnd()
    {
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    #endregion 동기화 함수


}       // ClassEnd

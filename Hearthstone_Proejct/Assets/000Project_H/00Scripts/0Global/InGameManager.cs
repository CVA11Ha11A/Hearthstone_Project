using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;

public class InGameManager : MonoBehaviourPunCallbacks
{   // 인게임에서 필요한 사이클 , 덱 초기화 , 랜덤 등 동기화되어야하는 기능들을 담을 것
    // ! 여러 군데에서 IngameManager의 도움을 받아서 기능수행을 할 것이기 때문에 최대한 프로퍼티 활용으로 참조 목록 확인 가능하도록 제작

    private static InGameManager instance;
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("InGameManager");
                obj.AddComponent<InGameManager>();
                obj.AddComponent<PhotonView>();
            }
            return instance;
        }
    }

    private StringBuilder sb = null;

    private InGameDeck inGameMyDeckRoot = null;
    public InGameDeck InGameMyDeckRoot
    {
        get
        {
            if (this.inGameMyDeckRoot == null)
            {   // 만약 해당 컴포넌트보다 InGameDeck의 컴포넌트가 먼저 라이프 사이클을 돈다면 들어올 조건문
                this.inGameMyDeckRoot = transform.Find("MyDeck").GetComponent<InGameDeck>();
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


    private PhotonView photonView = null;

    private void Awake()
    {
        instance = this;
        this.photonView = this.transform.GetComponent<PhotonView>();
        this.sb = new StringBuilder();
    }
    void Start()
    {

    }

    #region 플레이어들의 덱 초기화 관련
    public void FirstPlayersDeckInit()
    {   // 마스터 클라이언트만 연산
        if (PhotonNetwork.IsMasterClient == true)
        {
            // 1. 내덱을 렌덤하게 섞어야함 여기서 Cardid가 StartPoint || EndPoint 는 제외되어야함 (상대덱도 포함)
            // 2. 내덱(MasterClient)을 string으로 붙여서 자신 덱 초기화 하는 함수 실행
            // 3. 상대덱을 string으로 붙여서 상대 덱 초기화 하는 함수 실행
            // ! 상대에게 보낼때는 MasterClient기준으로 내 덱이 상대에겐 상대덱임 !

            List<string> deckList = new List<string>(35);   // 최대 30장까지 초기화 할것이기 때문에 여유있게 할당
            #region 마스터 클라이언트 덱을 초기화하고 클라이언트에게 마스터 클라이언트 덱을 보내는 기능
            Deck masterClientDeckRoot = GameManager.Instance.inGamePlayersDeck.MyDeck;
            int existenceCardCount = default;
            for (int i = 0; i < masterClientDeckRoot.cardList.Length; i++)
            {
                if (masterClientDeckRoot.cardList[i] == CardID.StartPoint || masterClientDeckRoot.cardList[i] == CardID.EndPoint)
                {   // 카드로 존재하지 않는 것이라면 다음으로 넘어가기
                    continue;
                }
                else
                {
                    deckList.Add(masterClientDeckRoot.cardList[i].ToString());
                    existenceCardCount++;
                }
            }
            // 여기선 리스트에 마스터클라이언트 덱에 카드가 존재하는 것들만 들어가 있을 것임
            // 인게임 에서 직업 고유 번호도 보내야 할까? -> 확장성을 고려한다면 보내야함 (이유 : 상대직업 무작위 카드를 가져옵니다 제작 가능)
            sb.Clear();

            for (int i = 0; i < deckList.Count; i++)
            {
                if (i + 1 < deckList.Count == false)
                {   // 다음 순회 조건에서 조건이 맞지 않은 경우
                    // 뒤에 ,를 뺴야함
                    sb.Append(deckList[i]);
                }
                else
                {
                    sb.Append(deckList[i]);
                    sb.Append(",");
                }
            }   // for : 덱속 카드 sb에 추가하는 for

            // 여기서 나의 덱을 섞어야함
            int shuffleCount = 5;   // 카드를 몇변 for문 돌리면서 랜덤하게 섞을지
            int nowShuffleCount = default;  // 0  아래 for문에서 현재 몇번 셔플 했는지 
            int shuffleIndex = default; // 아래 For문에서 Random으로 결정될 변수
            for (int i = 0; i < existenceCardCount; i++)
            {
                shuffleIndex = Random.Range(0, existenceCardCount);
                // 여기부터 다시 시작해야함
                // 위에 sb가 이 아래로 존재 해야 할 거갗다고도 생각이 듦
                // 왜냐하면 섞고 결과물을 Client에게 보내야하는데 위에 섞지 않은 결과를 보내면 
                // 고정된 순서에 카드가 나올것임
                if (i + 1 == existenceCardCount && nowShuffleCount < shuffleCount)
                {
                    i = 0;
                }
            }   // For : 카드 셔플 


            // 여기선 StringBuilder는 Id , Id , Id 구조를 가지게됨
            this.photonView.RPC("IngameEnemyDeckInitalize", RpcTarget.Others, sb.ToString());
        }

        

        #endregion 마스터 클라이언트 덱을 초기화하고 클라이언트에게 마스터 클라이언트 덱을 보내는 기능
        



        else { /*PASS*/ }   // 마스터 클라이언트만 연산
    }       // FirstPlayersDeckInit()
    #endregion 플레이어들의 덱 초기화 관련

    [PunRPC]
    public void IngameEnemyDeckInitalize(string enemyDeckStr_)
    {   // 인게임에서 상대덱을 초기화 하는 함수
        string[] enemyCardIdArr = enemyDeckStr_.Split(',');


    }   // IngameEnemyDeckInitalize()

}       // ClassEnd

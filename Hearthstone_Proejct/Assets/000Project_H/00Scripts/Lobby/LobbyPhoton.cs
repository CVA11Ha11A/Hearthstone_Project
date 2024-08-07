using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Text;

public class LobbyPhoton : MonoBehaviourPunCallbacks
{
    public bool isConnectedPhoton = false;     // 포톤의 마스터 클라이언트와 연결이 되었는지 확인할 bool값 (이게 true일때 매칭이 되야함)
    public bool startMatching = false;

    private StringBuilder sb = null;
    private WaitForSeconds waitForSeconds = null;
    private RoomOptions roomOptions = null;
    public PhotonView photonView = null;

    private float addTime = default;
    private float currentTime = default;
    private float maxTime = default;

    private bool isReadyToStart = false;

    private bool isMasterClient = false;        // 자신이 방을 생성한것인지 확인할 bool값

    private int lastRoomCount = default;        // 클라이언트가 방에 들어가기 위해서 방리스트를 업데이트 했을때 존재하는 방의 갯수 

    private void Awake()
    {
        this.isConnectedPhoton = false;
        this.startMatching = false;
        this.addTime = 0.1f;
        this.maxTime = 3f;

        this.transform.GetComponent<GameStartSelectDeckCanvas>().StartMatchingEvent += StartMatching;

        this.waitForSeconds = new WaitForSeconds(this.addTime);
        this.sb = new StringBuilder();
        this.roomOptions = new RoomOptions();
        this.photonView = this.gameObject.GetComponent<PhotonView>();

        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.CleanupCacheOnLeave = true;

        PhotonNetwork.AutomaticallySyncScene = true;
    }       // Awake()



    #region 포톤 서버 연결
    public void ConnectPhotonServer()
    {   // 포톤 서버와 연결을 시도하는 함수
        PhotonNetwork.ConnectUsingSettings();   //  포톤 서버에 연결하는 함수, 연결이 완료되면 OnConnectedToMaster() 콜백 함수가 호출
    }
    public override void OnConnectedToMaster()
    {   // 포톤 서버와 연결이 되었을경우 호출되는 콜백함수
        //DE.Log($"포톤 서버와 연결 성공");
        this.isConnectedPhoton = true;
        this.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "게임시작";
        this.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
    }       // OnConnectedToMaster()
    #endregion 포톤 서버 연결

    #region 매칭 관련
    private void StartMatching()
    {
        if (isConnectedPhoton == false)
        {
            DE.Log("포톤과 연결이 되지 않음!");
            return;
        }

        StartCoroutine(RoomSerchAndJoin());

    }   // StartMatching()

    #region 방 생성 관련 (마스터 클라이언트)

    private void RoomCreate()
    {   // 방을 생성하는 함수
        //DE.Log($"방 생성 함수 진입");
        if (PhotonNetwork.NetworkClientState == ClientState.Leaving)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        this.isMasterClient = true;
        StopAllCoroutines();
        PhotonNetwork.CreateRoom(null, this.roomOptions);
    }       // RoomCreate()

    private IEnumerator MasterClientWait() // 일반 함수로 변경후 해당 코루틴을 따로 만들어야함
    {

        //DE.Log($"마스터 클라이언트 대기 함수 진입");
        this.currentTime = 0f;
        while (this.currentTime < this.maxTime + 3f)
        {
            currentTime += addTime;
            #region LEGACY if
            //if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount > 1)
            //{   // 현재 다른 플레이어가 방에 들어왔다면                                

            //    DE.Log($"마스터 방에 누군가 접속");
            //    MyRoomClientIn();
            //    // TODO : 게임 시작 준비 (덱을 들고 씬을 이동하기)
            //}
            #endregion LEGACY if
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {   // 현재 다른 플레이어가 방에 들어왔다면                               
                //DE.Log($"마스터 방에 누군가 접속\nPlayerCount : {PhotonNetwork.CurrentRoom.PlayerCount}");
                MyRoomClientIn();
                // TODO : 게임 시작 준비 (덱을 들고 씬을 이동하기)
            }
            yield return waitForSeconds;
        }

        if (this.isReadyToStart == true)
        {
            // 혹시모를 예외처리
            StopAllCoroutines();
        }


        if (isReadyToStart == false)
        {   // 지정된 시간동안 다른 플레이어가 들어오지 않았다면

            if (PhotonNetwork.CurrentRoom != null)
            {
                PhotonNetwork.CurrentRoom.IsVisible = false;    // 방이 리스트에서 더 이상 보이지 않도록 설정합니다.
                PhotonNetwork.CurrentRoom.IsOpen = false;       // 방이 닫히도록 설정합니다.
                PhotonNetwork.CurrentRoom.EmptyRoomTtl = 1;     // 빈 방이 몇 초 후에 삭제되도록 설정합니다.
                PhotonNetwork.CurrentRoom.PlayerTtl = 1;        // 방에 있는 플레이어가 몇 초 후에 삭제될지 설정합니다.
                PhotonNetwork.CurrentRoom.RemovedFromList = true; // 방이 방 목록에서 제거되도록 설정합니다.
                PhotonNetwork.LeaveRoom();  // 현재방 나가기
            }

            this.isMasterClient = false;
            StartCoroutine(RoomSerchAndJoin()); // 방찾으며 들어가기
        }
        else { /*PASS*/ }

    }       // RoomCreateAndWait()



    #endregion 방 생성 관련 (마스터 클라이언트)

    #region 방 찾기 및 들어가기 기능 관련 (클라이언트)
    private IEnumerator RoomSerchAndJoin()
    {   // 방을 찾으며 접속하는 함수
        this.currentTime = 0;
        //DE.Log($"방찾기 및 접속 함수 진입\n{PhotonNetwork.CurrentRoom}\n NetworkState : {PhotonNetwork.NetworkClientState}");
        if (PhotonNetwork.NetworkClientState == ClientState.Leaving)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        while (PhotonNetwork.NetworkClientState != ClientState.ConnectedToMasterServer)
        {
            yield return null;
        }

        while (this.currentTime < this.maxTime)
        {
            this.currentTime += addTime;
            // TODO : 생성된 방이 존재한다면 들어가기            
            try
            {
                //DE.Log($"NetworkState : {PhotonNetwork.NetworkClientState}");
                if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
                {   // 마지막으로 변동된 값이 기본값이 아닐 경우 랜덤 접속
                    PhotonNetwork.JoinRandomRoom();

                }

                //if (lastRoomCount != default)
                //{   // 마지막으로 변동된 값이 기본값이 아닐 경우 랜덤 접속
                //    PhotonNetwork.JoinRandomRoom();
                //}
            }
            catch (System.Exception e)
            {
                /*! 방이 존재하지 않으면 예외가 던져짐 ! */
                // 의도된 예외기에 걱정 ㄴㄴ
            }
            finally { /*PASS*/ }
            yield return waitForSeconds;
        }
        RoomCreate();

    }       // RoomSerchAndJoin()

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {   // 방 갯수의 변동이 없을경우 호출 X 
        // 사용 가능한 방 목록이 업데이트될 때 호출됩니다.
        // 이 함수는 클라이언트가 마스터 서버에 연결되어 있고, 방 목록에 변경이 생길 때마다 호출됩니다.
        base.OnRoomListUpdate(roomList);
        //DE.Log($"방갯수 변동 : {roomList.Count}\n 현재 스크립트의 int : {this.lastRoomCount}");
        lastRoomCount = roomList.Count;
    }       // OnRoomListUpdate()

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //DE.Log($"랜덤방 접속 실패 : {message}");


    }
    #endregion 방 찾기 및 들어가기 기능 관련 (클라이언트)


    public override void OnJoinedRoom()
    {   // 방에 접속 성공 했을경우
        // this.isReadyToStart = true; // 여기에 존재하면 안됨
        StopAllCoroutines();
        //DE.Log($"방접속 함수 진입");
        if (this.isMasterClient == true)
        {       // 자신이 방을 만들어서 들어온 경우 (마스터 클라이언트)
            //DE.Log($"방접속 함수속 마스터클라이언트 if 진입");
            PhotonNetwork.CurrentRoom.IsVisible = true; // 방이 리스트에서 보이도록 설정합니다.
            PhotonNetwork.CurrentRoom.IsOpen = true; // 방이 열리도록 설정합니다.        
            StartCoroutine(MasterClientWait());

        }
        else
        {   // 자신이 만든방이 아닌 타인의 방의 접속한경우 (클라이언트)
            // 클라이언트가 먼저 채팅으로 자신의 덱 리스트를 보내야 할듯
        }

    }       // OnJoinedRoom()

    private void MyRoomClientIn()
    {   // 마스터 클라이언트 함수
        //DE.Log($"내방에 누군가 들어옴");
        this.transform.GetChild(5).GetComponent<OnMatchingCanvas>().StopMatchingButtonDisable();
        StopAllCoroutines();
        isReadyToStart = true;
        #region 클라이언트에게 덱 보내고 초기화시키는 함수 진행
        // sb에 현제 내 정보 넣고 상대에게 해당 인자를 이용해서 적 덱을 셋팅할 수 있도록 실행
        int deckRefIndex = this.gameObject.GetComponent<GameStartSelectDeckCanvas>().SelectDeckIndex;
        Deck tempDeckRoot = LobbyManager.Instance.playerDeckRoot.deckClass.deckList[deckRefIndex];
        sb.Clear();
        sb.Append((int)tempDeckRoot.deckClass);  // class
        sb.Append('-');                          // class
        for (int i = 0; i < tempDeckRoot.cardList.Length; i++)
        {
            if (i < tempDeckRoot.cardList.Length + 1)
            {
                sb.Append($"{(int)tempDeckRoot.cardList[i]},");
            }
            else
            {
                sb.Append($"{(int)tempDeckRoot.cardList[i]}");
            }
        }

        string result = sb.ToString();
        photonView.RPC("ClientDeckEnemyDeckSetting", RpcTarget.Others, result);
        #endregion 클라이언트에게 덱 보내고 초기화시키는 함수 진행
    }       // MyRoomClientIn()

    [PunRPC]
    public void ClientDeckEnemyDeckSetting(string enemyDeck_) // 클라이언트 키준 Enemy
    {       // RPC 함수 호출자 (this에서 MyRoomClientIn 가 호출)
        // 받은 인자를 이용해서 상대덱 초기화
        this.transform.GetChild(5).GetComponent<OnMatchingCanvas>().StopMatchingButtonDisable();
        GameManager.Instance.inGamePlayersDeck.EnemyDeckSetting(enemyDeck_);

        // 마스터클라이언트에게 보낼 클라이언트의 덱을 string으로 압축
        int deckRefIndex = this.gameObject.GetComponent<GameStartSelectDeckCanvas>().SelectDeckIndex;
        Deck tempDeckRoot = LobbyManager.Instance.playerDeckRoot.deckClass.deckList[deckRefIndex];
        sb.Clear();
        sb.Append((int)tempDeckRoot.deckClass);  // class
        sb.Append('-');                          // class
        for (int i = 0; i < tempDeckRoot.cardList.Length; i++)
        {
            if (i < tempDeckRoot.cardList.Length + 1)
            {
                sb.Append($"{(int)tempDeckRoot.cardList[i]},");
            }
            else
            {
                sb.Append($"{(int)tempDeckRoot.cardList[i]}");
            }
        }
        string result = sb.ToString();  // 압축된 결과
        // 마스터 클라이언트의 덱 초기화 함수 호출
        photonView.RPC("MasterClientEnemyDeckSetting", RpcTarget.MasterClient, result);

    }       // DeckEnemyDeckSetting()

    [PunRPC]
    public void MasterClientEnemyDeckSetting(string enemyDeck_)
    {
        GameManager.Instance.inGamePlayersDeck.EnemyDeckSetting(enemyDeck_);
        // 여기서 마스터 클라이언트가 게임을 시작해야함 -> 씬을 옮겨야함
        // 여기서 로드 전에 매칭 에니메이션 작동을 바꾸어주는것이 좋을듯? 
        photonView.RPC("GameStartAnimeStart", RpcTarget.All);
    }       // MasterClientDeckEnemyDeckSetting()

    [PunRPC]
    private void GameStartAnimeStart()
    {
        this.transform.GetChild(5).GetChild(0).GetComponent<MacthingStartScrollController>().GameStartAnime();
    }

    public void InGameScene()
    {        
        AudioManager.Instance.AllStopAudios();
        AudioManager.Instance.ClearAllAudios();
        StopAllCoroutines();
        if (PhotonNetwork.IsMasterClient == true)
        {
            //GameManager.Instance.inGamePlayersDeck.TOutPutDeck(ETarGet.My);
            //GameManager.Instance.inGamePlayersDeck.TOutPutDeck(ETarGet.Enemy);                        
            PhotonNetwork.LoadLevel("InGameScene");            
        }
    }
    #endregion 매칭관련

}       // LobbyPhoton Class End

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class LobbyPhoton : MonoBehaviourPunCallbacks
{
    public bool startMatching = false;

    private WaitForSeconds waitForSeconds = null;

    private float addTime = default;
    private float currentTime = default;
    private float maxTime = default;
    private bool isReadyToStart = false;

    private void Awake()
    {                
        this.startMatching = false;
        this.addTime = 0.1f;
        this.maxTime = 3f;
        this.waitForSeconds = new WaitForSeconds(this.addTime);
        this.transform.GetComponent<GameStartSelectDeckCanvas>().StartMatchingEvent += StartMatching;
    }

    private void StartMatching()
    {
        StartCoroutine(RoomCreateAndWait());

    }   // StartMatching()

    private IEnumerator RoomCreateAndWait()
    {
        this.currentTime = 0f;
        PhotonNetwork.CreateRoom($"{Random.Range(int.MinValue, int.MaxValue)}", new Photon.Realtime.RoomOptions { MaxPlayers = 2 });

        while (this.currentTime < this.maxTime)
        {
            currentTime += addTime;
            if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {   // 현재 다른 플레이어가 방에 들어왔다면                                
                MyRoomClientIn();
                // TODO : 게임 시작 준비 (덱을 들고 씬을 이동하기)

            }
            yield return waitForSeconds;
        }
        
        if(this.isReadyToStart == true)
        {
            // 혹시모를 예외처리
            StopAllCoroutines();
        }

        PhotonNetwork.LeaveRoom();  // 현재방 나가기

        if (isReadyToStart == false)
        {   // 지정된 시간동안 다른 플레이어가 들어오지 않았다면
            StartCoroutine(RoomSerchAndJoin()); // 방찾으며 들어가기
        }
        else { /*PASS*/ }

    }       // RoomCreateAndWait()
    
    private void MyRoomClientIn()
    {
        StopAllCoroutines();
        isReadyToStart = true;        

    }       // MyRoomClientIn()

    private IEnumerator RoomSerchAndJoin()
    {   // 방을 찾으며 접속하는 함수
        this.currentTime = 0;

        while (this.currentTime < this.maxTime)
        {
            this.currentTime += addTime;
            // TODO : 생성된 방이 존재한다면 들어가기
            try
            {
                PhotonNetwork.JoinRandomRoom();
            }
            catch(System.Exception e)
            {
                /*! 방이 존재하지 않으면 예외가 던져짐 ! */
                // 의도된 예외기에 걱정 ㄴㄴ
            }
            finally { /*PASS*/ }            
            yield return waitForSeconds;
        }

        if(this.isReadyToStart == false)
        {
            StartCoroutine(RoomCreateAndWait());
        }

    }       // RoomSerchAndJoin()

    public override void OnJoinedRoom()
    {   // 방에 접속 성공 했을경우
        this.isReadyToStart = true;
        StopAllCoroutines();
    }       // OnJoinedRoom()


}       // LobbyPhoton Class End

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartSelectDeckCanvas : MonoBehaviour
{
    private Vector3 originPos = default;
    private Vector3 movePos = default;

    private GameObject[] printDeckObjs = null;
    private Image selectDeckPrintImage = null;

    private int selectDeckIndex = -1;
    public int SelectDeckIndex
    {
        get
        {
            return this.selectDeckIndex;
        }
        set
        {
            if (this.selectDeckIndex != value)
            {
                this.selectDeckIndex = value;

            }
            if (this.selectDeckIndex != -1)
            {
                // 참조된 덱의 영웅 이미지로 우측에 띄우기
                selectDeckPrintImage.gameObject.SetActive(true);
                selectDeckPrintImage.sprite =
                ResourceManager.Instance.ClassPullSprite[(int)LobbyManager.Instance.playerDeckRoot.decks.deckList
                [this.selectDeckIndex].deckClass - 1];

            }
        }
    }

    public event Action StartMatchingEvent;

    private void Awake()
    {
        originPos = this.transform.position;
        movePos = new Vector3(0f, 1.15f, 34f);
        printDeckObjs = new GameObject[this.transform.GetChild(1).childCount];
        selectDeckPrintImage = this.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>();

        int loopCount = this.transform.GetChild(1).childCount;
        for (int i = 0; i < loopCount; i++)
        {
            printDeckObjs[i] = this.transform.GetChild(1).GetChild(i).gameObject;
            this.transform.GetChild(1).GetChild(i).GetComponent<SelectDeckImage>().selectIndex = i;
        }
        PrintDeckListAllOff();

    }       // Awake()

    public void PrintDeckListAllOff()
    {   // 출력이되는 모든 덱 리스트 꺼주는 함수

        for (int i = 0; i < printDeckObjs.Length; i++)
        {
            printDeckObjs[i].gameObject.SetActive(false);
        }
    }       // PrintDeckListAllOff()

    public void PrintDeckList()
    {   // 플레이어가 가지고 있는 덱 수 만큼 출력시켜주는 함수
        int printCount = LobbyManager.Instance.playerDeckRoot.decks.deckList.Count;
        for (int i = 0; i < printCount - 1; i++)
        {
            printDeckObjs[i].gameObject.SetActive(true);
            printDeckObjs[i].transform.GetComponent<Image>().sprite =
                ResourceManager.Instance.ClassVerticalSprite[(int)LobbyManager.Instance.playerDeckRoot.decks.deckList[i].deckClass - 1];
        }

    }       // PrintDeckList()

    #region ButtonFunctions
    public void OpenCanvasButtonFunction()
    {   // LobbyCanvas의 게임시작 버튼의 함수
        LobbyManager.Instance.CanvasOpen(this.transform, movePos);
        PrintDeckList();
        this.transform.GetComponent<LobbyPhoton>().ConnectPhotonServer();
    }       // OpenCanvasButtonFunction()

    public void BackButtonFunction()
    {
        LobbyManager.Instance.CanvasClose(this.transform, originPos, 2.5f);
    }       // BackButtonFunction()

    public void InvekeMatchingStart()
    {
        // ! if : 포톤서버와 연결이 되었을때에만 매칭 시작되도록
        if (this.transform.GetComponent<LobbyPhoton>().isConnectedPhoton == true)
        {
            this.StartMatchingEvent?.Invoke();
        }
        else { /*PASS*/ }
        //  여기서 내 덱을 초기화 하도록
        GameManager.Instance.inGamePlayersDeck.MyDeckSetting(this.SelectDeckIndex);
    }       // InvekeMatchingStart()
    #endregion ButtonFunctions
}       // ClassEnd

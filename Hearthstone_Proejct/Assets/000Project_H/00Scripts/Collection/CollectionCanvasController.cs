using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CollectionState
{
    None = 0,
    Looking = 1,
    DeckBuild = 2
}
public class CollectionCanvasController : MonoBehaviour
{
    // On : 수집품이 켜졌을때 도착할 포지션 , Off : 수집품이 켜지지 않은 상태일때 포지션
    private Vector3 onPosition = default;
    private Vector3 offPosition = default;
    private bool isFirstOpen = true;
    private bool isOpen = false;

    private CollectionHeroIcon nowPageIcon = default;
    public CollectionHeroIcon NowPageIcon
    {
        get
        {
            return this.nowPageIcon;
        }
        set
        {
            if (this.nowPageIcon != value)
            {
                nowPageIcon = value;
                cardGroupRoot.CurrentIndex = 0;
                if (isOpen == true)
                {
                    pageRoot.isNextMove = true;
                    cardGroupRoot.OutPutCard((ClassCard)this.nowPageIcon);
                }
            }
        }
    }

    private CollectionState nowState = default;
    public CollectionState NowState
    {
        get { return this.nowState; }
        set
        {
            if (this.nowState != value)
            {
                this.nowState = value;
            }
            if (this.nowState == CollectionState.DeckBuild)
            {
                // 덱 빌드로 변경 되었을때 실행돼어야하는것들 실행
                this.transform.GetComponent<CollectionCanvasCardInteraction>().enabled = true;
            }
        }
    }

    public ClassCard selectedBuildClass = default;
    #region ClassRoot
    private RectTransform bookCover = default;
    public CollecetionCardGroup cardGroupRoot = null;
    public CollectionPage pageRoot = null;
    public CollectionDeckCardList deckCardListRoot = null;
    #endregion ClassRoot

    public event Action<bool> BackButtonClassImageSpinEvent;        // DeckListComponent가 체이닝

    private void Awake()
    {
        AwakeInIt();
    }       // Awake()

    private void Start()
    {
        LobbyManager.Instance.OpenCollectionEvent += CollectionOpen;
    }       // Start()


    // ---------------------------------------------------- CustomMethod ----------------------------------------------------------------

    private void AwakeInIt()
    {
        LobbyManager.Instance.collectionCanvasRoot = this;
        onPosition = this.transform.position;
        onPosition.x = 0f;
        offPosition = onPosition;
        offPosition.x = -35f;
        this.transform.position = offPosition;

        bookCover = this.gameObject.transform.GetChild(2).GetComponent<RectTransform>();

        NowState = CollectionState.Looking;

    }       // AwakeInIt()

    #region 컬렉션 오픈 오프 함수
    /// <summary>
    /// 수집품 오픈시 호출될 함수
    /// </summary>
    public void CollectionOpen()
    {
        isOpen = true;
        StartCoroutine(SlidingCanvas());
    }       // CanvasOpen()

    public void CollectionClose()
    {
        isOpen = false;
        StartCoroutine(CloseCanvase());
    }

    /// <summary>
    /// 캔버스 포지션을 카메라 쪽으로 서서히 다가오게해주는 함수
    /// </summary>    
    IEnumerator SlidingCanvas()
    {
        float currentTime = 0;
        float lerpTime = 2.5f;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Vector3 newPosition = Vector3.Lerp(transform.position, onPosition, t);
            // 새로운 위치 적용
            transform.position = newPosition;

            if (transform.position == onPosition)
            {
                break;
            }

            yield return null;
        }

        if (isFirstOpen == true)
        {
            cardGroupRoot.OutPutCard((ClassCard)nowPageIcon);
            StartCoroutine(OpenBookCover());    // 캔버스 도착시 책 커버 열기코루틴 호출
        }

    }       // OpenCanvas()

    IEnumerator OpenBookCover()
    {
        float currentTime = 0f;
        float lerpTime = 3f;

        Quaternion goalQuaternion = Quaternion.Euler(0f, 90f, 0f);
        Quaternion defaultQuaternion = Quaternion.Euler(0f, 0f, 0f);



        while (currentTime < lerpTime)
        {
            currentTime += Time.deltaTime;

            float t = currentTime / lerpTime;
            bookCover.transform.rotation = Quaternion.Slerp(defaultQuaternion, goalQuaternion, currentTime);
            yield return null;
        }


    }       // OpenBookCover()

    IEnumerator CloseCanvase()
    {
        float currentTime = 0;
        float lerpTime = 2.5f;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Vector3 newPosition = Vector3.Lerp(transform.position, offPosition, t);
            // 새로운 위치 적용
            transform.position = newPosition;

            if (transform.position == offPosition)
            {
                break;
            }

            yield return null;
        }
    }
    #endregion 컬렉션 오픈 오프 함수

    #region 덱 생성State 함수들


    #endregion 덱 생성State 함수들

    #region 버튼 함수
    public void BackButtonEvent()
    {       // 뒤로가기 버튼 누를경우 실행될 함수
        if (this.NowState == CollectionState.Looking)
        {
            CollectionClose();
        }
        else if (this.NowState == CollectionState.DeckBuild)
        {
            // 1. 현제 댁 만들고 있는 상황을 저장            
            // 2. 켄버스를 원래대로 (영웅 이미지 다시 돌리기) , 덱 리스트를 보여주는것
            // 3. State 를 Looking으로 변경

            int loopCount = deckCardListRoot.GetCurrentIndex();
            Deck newDeck = new Deck();
            CardID cardId = default;
            for (int i = 0; i < loopCount - 1; i++)
            {
                cardId = deckCardListRoot.cardList[i].GetComponent<DeckInCard>().datas.cardId;
                newDeck.AddCardInDeck(cardId);
            }
            newDeck.SetDeckClass(deckCardListRoot.selectClass);
            if (deckCardListRoot.isCreatDeck == true && deckCardListRoot.isFixDeck == false)
            {
                LobbyManager.Instance.playerDeckRoot.decksList.Add(newDeck);                
            }
            else if(deckCardListRoot.isCreatDeck == false && deckCardListRoot.isFixDeck == true)
            {
                LobbyManager.Instance.playerDeckRoot.decksList[deckCardListRoot.fixIndex] = newDeck;
            }

            LobbyManager.Instance.playerDeckRoot.SaveDecks();

            deckCardListRoot.ClearCurrentIndex();
            deckCardListRoot.SetActiveFlaseToChilds();

            BackButtonClassImageSpinEvent?.Invoke(false);

            this.NowState = CollectionState.Looking;

        }
    }       // BackButtonEvent()
    #endregion 버튼 함수
}       // CollectionCanvasController ClassEnd

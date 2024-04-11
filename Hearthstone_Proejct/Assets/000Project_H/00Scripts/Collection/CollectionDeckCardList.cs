using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.Rendering;
using UnityEngine;

public class CollectionDeckCardList : MonoBehaviour
{   // 덱 생성 수정 관련한 컴포넌트
    public GameObject[] cardList = null;

    public ClassCard selectClass = default;
    private int currentIndex = default;      // Initalize == 0 
    private DeckInCard deckInCardRoot = null;

    public bool isCreatDeck = default;
    public bool isFixDeck = default;        // 덱 저장할때 해당 bool값 2개를 확인해서 리스트를 수정할지 아니면 추가할지 결정될것
    public int fixIndex = default;          // 수정시 변경되어야할 PlayerDeckList의 인덱스 

    private RectTransform moveObj = null;
    private Vector3 onOutputV3 = default;       // ScrollView의 Transform을 조정시켜줄 Vector3
    private Vector3 offOutputV3 = default;      // ScrollView의 Transform을 조정시켜줄 Vector3

    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().deckCardListRoot = this;
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasCardInteraction>().deckCardListRoot = this;

        moveObj = this.transform.parent.parent.GetComponent<RectTransform>();
        onOutputV3 = moveObj.anchoredPosition3D;
        offOutputV3 = onOutputV3;
        offOutputV3.x = 80f;

        isFixDeck = false;
        isCreatDeck = false;

        cardList = new GameObject[this.transform.childCount];
        for (int i = 0; i < cardList.Length; i++)
        {
            cardList[i] = this.transform.GetChild(i).gameObject;
        }        
        SetActiveFlaseToChilds();
        CardListTransformSet(CollectionState.Looking);      // 이건 좀 좋지 않은 코드같음 하지만 현재기준 어쩔수 없이 하는 함수 2024.04.04

    }       // Awake()

    public void SetActiveFlaseToChilds()
    {
        for (int i = 0; i < cardList.Length; i++)
        {
            cardList[i].gameObject.SetActive(false);
        }
    }       // SetActiveFlaseToChilds()


    public void AddToCard(Card addCard_)
    {
        #region CardSetting
        cardList[currentIndex].SetActive(true);
        deckInCardRoot = cardList[currentIndex].GetComponent<DeckInCard>();
        #region LEGACY
        //deckInCardRoot.CardName = addCard_.cardName;
        //deckInCardRoot.CardCost = addCard_.cost;
        //deckInCardRoot.SetCardId(addCard_.cardId);
        //deckInCardRoot.cardImageRoot.sprite = addCard_.cardImage.sprite;
        #endregion LEGACY

        deckInCardRoot.Datas.cardId = addCard_.cardId;
        deckInCardRoot.Datas.cardCost = addCard_.cost;
        deckInCardRoot.Datas.cardName = addCard_.cardName;                
        deckInCardRoot.Datas.cardSprite = addCard_.GetCardSprite();


        #endregion CardSetting
        SortingObjectList();
        UpdateUis();

        #region SortCardList_LEGACY
        //DeckInCard cardIRoot = default;
        //DeckInCard cardJRoot = default;
        //for (int i = 0; i < currentIndex; i++)
        //{
        //    cardIRoot = cardList[i].GetComponent<DeckInCard>();
        //
        //    for (int j = i + 1; j <= currentIndex; j++)
        //    {
        //
        //        cardJRoot = cardList[j].GetComponent<DeckInCard>();
        //        if (cardIRoot.Datas.cardCost > cardJRoot.Datas.cardCost)
        //        {
        //            DeckInCardData tempRoot = cardIRoot.Datas;
        //            cardIRoot.Datas = cardJRoot.Datas;
        //            cardJRoot.Datas = tempRoot;
        //        }
        //    }
        //}
        #endregion SortCardList_LEGACY
        #region UpdataUILEGACY
        //for (int i = 0; i <= currentIndex; i++)
        //{
        //    cardList[i].GetComponent<DeckInCard>().UpdateUI();
        //}
        #endregion UpdataUILEGACY

        currentIndex++;

    }       // AddToCard()

    // 덱속 카드를 제거해주는 함수
    public void RemoveToCard(CardID removeCardId_)
    {
        #region LEGACY
        //// 2024.04.12기준 해당 함수에 문제가 있는거 같음 해결해야함
        //DeckInCard deckInCardRoot = null;
        //DeckInCard pullDeckInCardRoot1 = null;
        //DeckInCard pullDeckInCardRoot2 = null;
        //int nullIndex = default;
        //string exceptionStr = "";
        //
        //for (int i = 0; i < cardList.Length; i++)
        //{
        //    // 1. List에서 GetComponent가 가능하면 i번째의 컴포넌트를 가져온다.
        //    if (cardList[i].GetComponent<DeckInCard>())
        //    {
        //        deckInCardRoot = cardList[i].GetComponent<DeckInCard>();
        //    }
        //    else
        //    {
        //        //DE.Log($"RemoveToCard함수 : {i}번쨰루프중 참조가 안됨");
        //        continue;
        //    }
        //    // 2. 순회하며 제거할 카드와 같을때 들어간다.
        //    if (deckInCardRoot.Datas.cardId == removeCardId_)
        //    {
        //        currentIndex--;
        //        pullDeckInCardRoot1 = cardList[i].GetComponent<DeckInCard>();
        //        pullDeckInCardRoot1.ClearData();
        //        // TODO : target카드를 빼고 그 뒤부터 앞으로 땅기기 
        //        for (int j = i; j < cardList.Length - 1; j++)
        //        {
        //            pullDeckInCardRoot1 = cardList[i].GetComponent<DeckInCard>();
        //            pullDeckInCardRoot2 = cardList[j + 1].GetComponent<DeckInCard>();
        //            // 2 -> 1 로 데이터 변경
        //            pullDeckInCardRoot1.CopyToPaste(pullDeckInCardRoot2, ref pullDeckInCardRoot1.Datas.cardName, ref pullDeckInCardRoot2.Datas.cardName);
        //            if (pullDeckInCardRoot1.Datas.cardName == exceptionStr)
        //            {
        //                nullIndex = j;
        //            }
        //        }
        //        cardList[currentIndex].SetActive(false);
        //        break;
        //    }
        //}
        #endregion LEGACY
        // ! cardList == 출력해주는 Object가 들어가 있는 Object[] 개체

        DeckInCard outputCardRoot = null;
        //bool isRemove = false;
        for(int i = 0; i < currentIndex -1; i++)
        {
            outputCardRoot = cardList[i].GetComponent<DeckInCard>();
            if(outputCardRoot.Datas.cardId == removeCardId_)
            {
                DeckInCard swapRoot = cardList[currentIndex -1].GetComponent<DeckInCard>();
                DE.Log($"Swap 당하는 얘 이름 : {swapRoot.gameObject.name} , CurrentIndex : {currentIndex}");
                outputCardRoot.Datas.cardId = swapRoot.Datas.cardId;
                outputCardRoot.Datas.cardCost = swapRoot.Datas.cardCost;
                outputCardRoot.Datas.cardSprite = swapRoot.Datas.cardSprite;
                outputCardRoot.Datas.cardName = swapRoot.Datas.cardName;

                swapRoot.Datas.ClearDatas();
                //SortingObjectList();
                // TODO : list속 카드의 null값을 찾아서 null이 아닌 카드와 Swap?
                
                currentIndex--;
                // TODO : currentIndex 만큼 Loop하며 정렬
                SortingObjectList();
                for (int uiUpdateLoopCount = 0; uiUpdateLoopCount <= currentIndex; uiUpdateLoopCount++)
                {
                    //DE.Log($"몇번째 순회에서 참조를 못하지? : {i}\n i 번째의 게임오브젝트 이름 : {cardList[i].gameObject.name}");
                    cardList[uiUpdateLoopCount].GetComponent<DeckInCard>().UpdateUI();
                }

            }
        }        
        //SortingObjectList();
        //UpdateUis();
        
        //if(isRemove == true) { currentIndex--; }
        #region LEGACY
        //DeckInCard deckInCardRoot = null;
        //for (int i = 0; i < cardList.Length; i++)
        //{
        //    if (cardList[i].GetComponent<DeckInCard>())
        //    {
        //        deckInCardRoot = cardList[i].GetComponent<DeckInCard>();
        //        if (deckInCardRoot.Datas.cardId == removeCardId_)
        //        {
        //            deckInCardRoot.ClearData();
        //            currentIndex--;
        //            break;
        //        }
        //    }
        //}
        //
        //#region UpdataUI
        //for (int i = 0; i <= currentIndex; i++)
        //{
        //    cardList[i].GetComponent<DeckInCard>().UpdateUI();
        //}
        //#endregion UpdataUI        
        #endregion LEGACY


    }   // RemoveToCard()

    public void DeckOutPut(int targetDeckIndex_)
    {   // 플레이어가 선택한 덱의 인덱스를 참조해서 덱의 현재 존재하는 카드들을 출력해주는 함수
        // DeckListCompoent.DeckOnClick() 가 호출할거임
        PlayerDeckData playerDeckRoot = LobbyManager.Instance.playerDeckRoot.decks;
        int deckMaxCard = playerDeckRoot.deckList[targetDeckIndex_].count;
        for (int i = 0; i < deckMaxCard; i++)
        {
            if (playerDeckRoot.deckList[targetDeckIndex_].cardList[i] != CardID.StartPoint &&
                playerDeckRoot.deckList[targetDeckIndex_].cardList[i] != CardID.EndPoint)
            {
                AddToCard(CardManager.cards[playerDeckRoot.deckList[targetDeckIndex_].cardList[i]]);
            }
            else { /*PASS*/ }
        }

    }       // DeckOutPut()

    public void CardListTransformSet(CollectionState collectionState_)
    {
        //return;
        if (collectionState_ == CollectionState.Looking)
        {
            moveObj.anchoredPosition3D = offOutputV3;
        }
        else if (collectionState_ == CollectionState.DeckBuild)
        {
            moveObj.anchoredPosition3D = onOutputV3;
        }
    }       // CardListTransformSet()

    private void SortingObjectList()
    {
        DeckInCard cardIRoot = default;
        DeckInCard cardJRoot = default;
        for (int i = 0; i < currentIndex; i++)
        {
            cardIRoot = cardList[i].GetComponent<DeckInCard>();

            for (int j = i + 1; j <= currentIndex; j++)
            {

                cardJRoot = cardList[j].GetComponent<DeckInCard>();
                if (cardIRoot.Datas.cardCost > cardJRoot.Datas.cardCost)
                {
                    DeckInCardData tempRoot = cardIRoot.Datas;
                    cardIRoot.Datas = cardJRoot.Datas;
                    cardJRoot.Datas = tempRoot;
                }
            }
        }
    }       // SortingObjectList()

    private void UpdateUis()
    {
        for (int i = 0; i <= currentIndex; i++)
        {            
            //DE.Log($"몇번째 순회에서 참조를 못하지? : {i}\n i 번째의 게임오브젝트 이름 : {cardList[i].gameObject.name}");
            cardList[i].GetComponent<DeckInCard>().UpdateUI();
        }
    }       // UpdateUis()

    public int GetCurrentIndex()
    {
        return this.currentIndex;
    }
    public void ClearCurrentIndex()
    {
        this.currentIndex = 0;
    }



}       // ClassEnd

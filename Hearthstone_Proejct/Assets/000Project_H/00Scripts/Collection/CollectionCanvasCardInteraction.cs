using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionCanvasCardInteraction : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler, IPointerDownHandler
{       // DeckBuild State일떄 켜주어서 플레이어와 카드의 상호작용을 할 수 있게 해주는 클래스

    private int lastRefDeckIndex = -1;
    public int LastRefDeckIndex
    {
        get
        {
            return this.lastRefDeckIndex;
        }
        set
        {
            if (value >= 9 || -1 > value)
            {
                this.lastRefDeckIndex = -1;
            }
            else
            {
                this.lastRefDeckIndex = value;
            }
        }
    }


    public bool isDeckClick = false;       // 덱에 존재하는것을 클릭했는지 

    public LayerMask cardLayerMask = default;           // 컬렉션의 카드인지 판별할 레이어
    public LayerMask deckCardListLayerMask = default;   // 덱인지 판별한 레이어
    public LayerMask deckInCardLayerMask = default;     // 덱속 카드인지 판별할 레이어
    public LayerMask collectionBackGroundLayerMask = default; // 컬렉션의 배경인지 판별할 레이어
    public Card lastChoiceCard = null;      // TODO : CardId로 바꾸는게 좋을듯
    public CardID lastChoiceCardID = default;

    private float rayMaxDistance = default;
    private Ray ray;
    private RaycastHit hitInfo = default;

    private GameObject instanceCard = null;

    public CollectionDeckCardList deckCardListRoot = null;
    public ScrollView scrollViewRoot = null;

    private void Awake()
    {
        rayMaxDistance = 300f;
        cardLayerMask = 1 << 6;
        deckCardListLayerMask = 1 << 7;
        deckInCardLayerMask = 1 << 8;
        collectionBackGroundLayerMask = 1 << 9;
        this.enabled = false;
    }

    private void Start()
    {
        if (instanceCard == null)
        {   // ! 30오브젝트 생성이 30 ~ 100개정도로 인스턴스 예상이됨 이정도면 풀링 사용안하는것이 더 나을듯?
            Transform parentTargetTras = this.transform.GetChild(1).GetChild(2);    // ! target == PageObj
            instanceCard = Instantiate(CardManager.Instance.cardPrefab, Vector3.zero, Quaternion.identity, parentTargetTras);
            instanceCard.transform.localScale = new Vector3(25f, 25f, 25f);
            instanceCard.gameObject.SetActive(false);
        }
    }       // Start()

    private void Update()
    {       // 오픈된 덱의 스크롤을 위해 사용
        // TODO : 스크롤 Input이 들어올 경우 ScrollView의 currentView를 변경
        // 내리면 -20 올리면 +20      // 움직여야하는 개체  = Content

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {   // 위로 스크롤
            scrollViewRoot.CurrentView -= 20;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {   // 아래로 스크롤
            scrollViewRoot.CurrentView += 20;
        }
    }       // Update()
    #region Interface Methods

    #region LEGACY


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {   // 클릭시 호출

        // 마우스 스크린 좌표 얻기
        Vector3 mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));
        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, rayMaxDistance, cardLayerMask))
        {
            lastChoiceCard = hitInfo.collider.GetComponent<Card>();
        }
        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, rayMaxDistance, deckInCardLayerMask))
        {
            lastChoiceCardID = hitInfo.collider.GetComponent<DeckInCard>().Datas.cardId;
            isDeckClick = true;
        }

    }       // OnPointerDown()

    #endregion LEGACY

    void IDragHandler.OnDrag(PointerEventData eventData)
    {   // 드래그중 호출
        // TODO : 현제 개체가 따라다니는 중이라면 계속 따라다니도록
        if (instanceCard.gameObject.activeSelf == true)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                Camera.main.nearClipPlane));
            mouseWorldPosition.z = -1f;
            instanceCard.transform.position = mouseWorldPosition;
        }
        else { /*PASS*/ }

    }       // IDragHandler.OnDrag()

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {   // 클릭 땔때 호출
        // TODO : Prefab이 켜져있으며 땐곳이 덱 리스트라면 현재 카드추가
        //          아니라면 카드추가하지 않고 Prefab setActive = Flase        

        if (instanceCard.gameObject.activeSelf == true)
        {
            // 마우스 스크린 좌표 얻기
            Vector3 mouseScreenPosition = Input.mousePosition;
            // 마우스 스크린 좌표를 월드 좌표로 변환
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                Camera.main.nearClipPlane));

            //DE.DrawRay(mouseWorldPosition, Vector3.forward * 300f, Color.red, 5000f);            
            if (Physics.Raycast(mouseWorldPosition, Vector3.forward, rayMaxDistance, deckCardListLayerMask))
            {
                //DE.Log($"DeckCardListLayer 맞음");
                if (deckCardListRoot == null)
                {
                    deckCardListRoot = LobbyManager.Instance.collectionCanvasRoot.deckCardListRoot;
                }
                deckCardListRoot.AddToCard(lastChoiceCard);

            }

            if (isDeckClick == true && Physics.Raycast(mouseWorldPosition, Vector3.forward, rayMaxDistance, collectionBackGroundLayerMask))
            {
                #region LEGACY
                /*
                if(lastChoiceCardID == default)
                {
                    return;
                }
                //DE.Log($"lastRefDeckIndex : {lastRefDeckIndex}");
                DE.Log($"제거할 카드 ID : {(int)lastChoiceCardID}, 카드이름 : {lastChoiceCardID}");
                deckCardListRoot.RemoveToCard(lastChoiceCardID);
                LobbyManager.Instance.playerDeckRoot.decks.deckList[lastRefDeckIndex].RemoveCard(lastChoiceCardID);                
                 */
                #endregion LEGACY
                if (lastChoiceCardID == default)
                {
                    return;
                }
                // 1. 실제 덱을 수정한다. 
                LobbyManager.Instance.playerDeckRoot.deckClass.deckList[LastRefDeckIndex].RemoveCard(lastChoiceCardID);

                // 2.실제 덱을 토대로 출력을한다.
                // 새로 생성하는 덱이라면 저장하고 접근해야함
                if (deckCardListRoot.isCreatDeck == true)
                {       // 여기서 덱을 생성해서 추가하고 CreateMode -> EditMode로 변경해야할듯?
                    this.transform.GetComponent<CollectionCanvasController>().DeckCreateFromDeckFix();
                }
                else
                {       // TODO : (1)현재 참조된 덱의 내부를 바꿈(선택된 카드 제거) -> (2)참조된 덱을 새로 출력
                    deckCardListRoot.DeckOutPut(LastRefDeckIndex, true);
                }

            }


            Destroy(instanceCard.GetComponent<Card>());
            instanceCard.gameObject.SetActive(false);
        }
        lastChoiceCardID = default;
        lastChoiceCard = null;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {   // 드래그 하는 순간        
        // TODO : lastChoiceCard가 != null 이라면
        // Card Prefab이 보이게 되며 해당 개체가 마우스를 따라다님

        if (lastChoiceCard != null)
        {
            instanceCard.gameObject.SetActive(true);
            instanceCard.AddComponent(lastChoiceCard.GetType());
        }
        if (lastChoiceCardID != CardID.StartPoint)
        {
            instanceCard.gameObject.SetActive(true);
            instanceCard.AddComponent(CardManager.Instance.cards[lastChoiceCardID].GetType());
        }

    }       // OnBeginDrag()


    #endregion Interface Methods


}       // ClassEnd

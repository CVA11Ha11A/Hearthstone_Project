using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionCanvasCardInteraction : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler, IPointerDownHandler
{       // DeckBuild State일떄 켜주어서 플레이어와 카드의 상호작용을 할 수 있게 해주는 클래스
    
    public LayerMask cardLayerMask = default;
    public LayerMask deckCardListLayerMask = default;
    public Card lastChoiceCard = null;

    private float rayMaxDistance = default;
    private Ray ray = default;
    private RaycastHit hitInfo = default;

    private GameObject instanceCard = null;

    private CollectionDeckCardList deckCardListRoot = null;
    

    private void Awake()
    {
        rayMaxDistance = 300f;
        cardLayerMask = 1 << 6;        
        deckCardListLayerMask = 1 << 7;
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
    }
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
                GetDeckCardListRoot();      // 루트가 null이면 가져오는 함수
                deckCardListRoot.AddToCard(lastChoiceCard);
            }            

            Destroy(instanceCard.GetComponent<Card>());
            instanceCard.gameObject.SetActive(false);
        }
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

    }       // OnBeginDrag()


    #endregion Interface Methods

    private void GetDeckCardListRoot()
    {
        if(deckCardListRoot == null)
        {
            deckCardListRoot = LobbyManager.Instance.collectionCanvasRoot.deckCardListRoot;
        }
        else
        {
            return;
        }
    }       // GetDeckCardListRoot()

}       // ClassEnd

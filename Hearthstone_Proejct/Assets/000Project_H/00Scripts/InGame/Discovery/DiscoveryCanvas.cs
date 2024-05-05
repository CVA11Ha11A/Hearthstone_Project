using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscoveryCanvas : MonoBehaviour, IPointerDownHandler
{

    private GameObject[] discoveryObjs = null;
    private GameObject buttonObj = null;

    private bool isDisCoverying = false;        // true면 클릭시 Ray를 쏘아서 선택 가능하도록 할 것
    public bool IsDisCoverying
    {
        get
        {
            return isDisCoverying;
        }
        set
        {
            if (isDisCoverying != value)
            {
                isDisCoverying = value;
            }
        }
    }
    private bool isMultiplechoices = false;     // 다중 선택이 가능한 발견인지 확인할 bool값 false면 Click하는 순간 선택한 카드 return
    public bool IsMultiplechoices
    {
        get
        {
            return isMultiplechoices;
        }
        set
        {
            if (isMultiplechoices != value)
            {
                isMultiplechoices = value;
            }
        }
    }

    private int targetLayer = default;
    private Ray ray = default;
    private RaycastHit hitInfo = default;
    private CardID[] selectCardId = default;

    private bool isMulliganMode = default;
    private CardID[] mulliganCardIds = default;
    private Vector3 nonSelectScale = default;
    private Vector3 selectScale = default;

    private void Awake()
    {
        this.isDisCoverying = false;
        this.targetLayer = 1 << 10;
        this.nonSelectScale = new Vector3(12f, 12f, 12f);
        this.selectScale = new Vector3(15f, 15f, 15f);
        this.selectCardId = new CardID[3];       // 3개까지 발견 가능

        int loopCount = this.transform.GetChild(0).childCount;
        this.discoveryObjs = new GameObject[loopCount];
        for (int i = 0; i < loopCount; i++)
        {
            discoveryObjs[i] = this.transform.GetChild(0).GetChild(i).gameObject;
        }
        this.buttonObj = this.transform.GetChild(1).gameObject;

        OffDescoveryObjs();
        buttonObj.SetActive(false);
    }


    void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        #region 멀리건
        if (isMulliganMode == true)
        {           

            // 마우스 스크린 좌표 얻기
            Vector3 mouseScreenPosition = Input.mousePosition;
            // 마우스 스크린 좌표를 월드 좌표로 변환
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                Camera.main.nearClipPlane));
            if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, targetLayer))
            {
                if (hitInfo.transform.GetComponent<DiscoveryCard>() == true)
                {
                    hitInfo.transform.GetComponent<DiscoveryCard>().OnClick();                    
                }
                else
                {
                    DE.Log($"맞은 객체가 DiscovaryCard를 가지고 있지 않음");
                }
            }
        }

        #endregion 멀리건

        if (this.IsDisCoverying == true && isMulliganMode == false)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetLayer))
            {
                if (hitInfo.transform.GetComponent<DiscoveryCard>() == true)
                {
                    AddToDiscovaryCard(hitInfo.transform.GetComponent<Card>().cardId);
            
                }
                else
                {
                    DE.Log($"맞은 객체가 DiscovaryCard를 가지고 있지 않음");
                }
            }


        }

    }       // OnPointerDown()

    private void AddToDiscovaryCard(CardID selectCardid_)
    {
        for (int i = 0; i < selectCardId.Length; i++)
        {
            if (selectCardId[i] == CardID.StartPoint || selectCardId[i] == CardID.EndPoint)
            {
                selectCardId[i] = selectCardid_;
                break;
            }
        }

        if (this.IsMultiplechoices == true)
        {
            return;
        }
        else
        {
            ProvisionCard();
        }

    }       // AddToDiscovaryCard()

    // 발견한 플레이어에게 카드를 지급하는 함수
    private void ProvisionCard()
    {
        this.IsMultiplechoices = false;
        this.IsDisCoverying = false;
        // 카드 지급 함수 실행해야함
    }


    public void Mulligan(CardID mulligan1, CardID mulligan2, CardID mulligan3)
    {
        InGameManager.Instance.MasterClientMulliganWait();
        OnDescoveryObjs();
        buttonObj.SetActive(true);
        mulliganCardIds = new CardID[3];
        this.isMulliganMode = true;
        CardManager.Instance.InItCardComponent(discoveryObjs[0], mulligan1);
        CardManager.Instance.InItCardComponent(discoveryObjs[1], mulligan2);
        CardManager.Instance.InItCardComponent(discoveryObjs[2], mulligan3);

    }

    public void MulliganSelectButton()
    {   // 해당 켄버스의 버튼이 가지고 있을 함수
        CardID mulliganCardId0 = discoveryObjs[0].GetComponent<Card>().cardId;
        CardID mulliganCardId1 = discoveryObjs[1].GetComponent<Card>().cardId;
        CardID mulliganCardId2 = discoveryObjs[2].GetComponent<Card>().cardId;

        // 여기서 true인것에서 덱에서 랜덤하게 카드한장 고르고 반환 받아서 정해진것 드로우 시키고 셔플
        if (discoveryObjs[0].GetComponent<DiscoveryCard>().IsClick == true)
        {
            mulliganCardId0 = InGameManager.Instance.InGameMyDeckRoot.InGamePlayerDeck.cardList
                [Random.Range(3, InGameManager.Instance.InGameMyDeckRoot.InGamePlayerDeck.cardList.Length)];
        }
        if (discoveryObjs[1].GetComponent<DiscoveryCard>().IsClick == true)
        {
            mulliganCardId1 = InGameManager.Instance.InGameMyDeckRoot.InGamePlayerDeck.cardList
                [Random.Range(3, InGameManager.Instance.InGameMyDeckRoot.InGamePlayerDeck.cardList.Length)];
        }
        if (discoveryObjs[2].GetComponent<DiscoveryCard>().IsClick == true)
        {
            mulliganCardId2 = InGameManager.Instance.InGameMyDeckRoot.InGamePlayerDeck.cardList
                [Random.Range(3, InGameManager.Instance.InGameMyDeckRoot.InGamePlayerDeck.cardList.Length)];
        }

        
        InGameManager.Instance.InGameMyDeckRoot.DrawCard(mulliganCardId0);
        InGameManager.Instance.InGameMyDeckRoot.DrawCard(mulliganCardId1);
        InGameManager.Instance.InGameMyDeckRoot.DrawCard(mulliganCardId2);

        // 셔플
        InGameManager.Instance.ShuffleCards(InGameManager.Instance.InGameMyDeckRoot, ETarGet.My);

        // 초기화 
        for (int i = 0; i < 3; i++)
        {
            discoveryObjs[i].GetComponent<DiscoveryCard>().IsClick = false;
            discoveryObjs[i].transform.localScale = nonSelectScale;
            Destroy(discoveryObjs[i].GetComponent<Card>());
        }
        mulliganCardIds = null;
        isMulliganMode = false;

        OffDescoveryObjs();
        buttonObj.gameObject.SetActive(false);
        InGameManager.Instance.CompleateMulligan();
    }       // MulliganSelectButton()


    public void OnDescoveryObjs()
    {
        for (int i = 0; i < discoveryObjs.Length; i++)
        {
            discoveryObjs[i].gameObject.SetActive(true);
        }
    }       // OnDescoveryObjs()
    public void OffDescoveryObjs()
    {
        for (int i = 0; i < discoveryObjs.Length; i++)
        {
            discoveryObjs[i].gameObject.SetActive(false);
        }
    }       // OffDescoveryObjs()

}       // ClassEnd

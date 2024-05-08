using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{   // 상시로 레이를 쏘며 상호작용할 오브젝트

    private LayerMask targetObjLayer = default;
    private LayerMask cardLayer = default;
    public bool isRayCast = false;
    public bool isDraging = false;

    private RaycastHit hitInfo = default;
    private Vector3 mouseScreenPosition = default;
    private Vector3 mouseWorldPosition = default;
    private MouseInteractionObj lastHitObj = null;

    private Vector3 highlightCardScale = default;           // standard에 접근한다면 15f 바로 접근한다면 1.5f
    private Vector3 highlightCardPosition = default;        // Y = 80 Fix
    private float highlightCardYPos = default;

    private Vector3 lastCardPosition = default;
    private Vector3 lastCardScale = default;
    private Quaternion lastCardRoation = default;
    public Card lastCardRoot = null;

    private void Awake()
    {
        InGameManager.Instance.mouseRoot = this;
        this.isRayCast = false;
        this.isDraging = false;
        this.targetObjLayer = 1 << 11;
        this.cardLayer = 1 << 6;
        this.highlightCardYPos = 65f;
        this.highlightCardScale = new Vector3(1.5f, 1.5f, 1.5f);


    }



    void Update()
    {
        if (this.isRayCast == false)
        {
            return;
        }

        // 마우스 스크린 좌표 얻기
        mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));

        // ------------------------------------------------------- 상호작용 Obj관련 ----------------------------------------------------------------
        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, targetObjLayer))
        {
            if (hitInfo.transform.GetComponent<MouseInteractionObj>() == true)
            {
                lastHitObj = hitInfo.transform.GetComponent<MouseInteractionObj>();
                lastHitObj.ObjFunction();
            }
        }
        else
        {
            if (lastHitObj != null)
            {
                lastHitObj.EndObjFunction();
                lastHitObj = null;
            }
        }

        // ----------------------------------------------------------- Card관련 --------------------------------------------


        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, cardLayer))
        {
            if (lastCardRoot != null)
            {
                if (lastCardRoot.Equals(hitInfo.transform.GetComponent<Card>()))
                {   // 이미 부각시킨 카드라면
                    return;
                }
                else if(isDraging == true)
                {
                    return;
                }
                else
                {
                    LastCardPositionRollBack();
                }
            }
            //DE.Log("Ray가 CardLayer를 검출하였음");
            if (hitInfo.transform.parent.parent.name == "MyHand")
            {
                //DE.Log($"HitInfo의 parent.parent가 MyHand임");
                // 해당 카드 를 부각 시켜야함
                // 이전 데이터 저장
                lastCardRoation = hitInfo.transform.rotation;
                lastCardPosition = hitInfo.transform.position;
                lastCardScale = hitInfo.transform.localScale;

                highlightCardPosition = new Vector3(hitInfo.transform.localPosition.x, highlightCardYPos, -200f);
                hitInfo.transform.localPosition = highlightCardPosition;
                hitInfo.transform.localScale = highlightCardScale;
                hitInfo.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                lastCardRoot = hitInfo.transform.GetComponent<Card>();

            }
            else
            {
                //DE.Log($"HitInfo의 parent.parent가 MyHand라는 이름을 가진 개체가 아님");
            }

        }
        else
        {
            //DE.Log($"MOuse : IsDraging -0> {isDraging}");
            if (lastCardRoot != null && isDraging == false)
            {
                LastCardPositionRollBack();
            }
        }

    }       // Update()


    public void LastCardPositionRollBack(bool isTransfromSet_ = true)
    {
        DE.Log($"언제호출이되는거지?");
        if (isTransfromSet_ == true)
        {
            lastCardRoot.transform.position = lastCardPosition;
            lastCardRoot.transform.rotation = lastCardRoation;
            lastCardRoot.transform.localScale = lastCardScale;
        }
        lastCardRoot = null;
    }       // LastCardPositionRollBack()

}       // ClassEnd

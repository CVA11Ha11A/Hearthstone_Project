using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollectionCanvasCardInteraction : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler , IPointerDownHandler
{       // DeckBuild State일떄 켜주어서 플레이어와 카드의 상호작용을 할 수 있게 해주는 클래스

    RaycastHit hitObj = default;
    public int targetLayer = default;
    public LayerMask layerMask = default;
    public Card lastChoiceCard = null;

    private void Awake()
    {
        targetLayer = LayerMask.GetMask("Card");
        layerMask = LayerMask.GetMask("Card");
        this.enabled = false;
    }
    void Start()
    {
        Gizmos.color = Color.red;
    }

    #region Interface Methods

    #region LEGACY


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {   // 클릭시 호출        
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
      Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Physics.Raycast(point, Vector3.forward, out hitObj, 300f, layerMask))
        {            
            lastChoiceCard = hitObj.collider.GetComponent<Card>();
        }
        

    }       // OnPointerDown()

    #endregion LEGACY

    void IDragHandler.OnDrag(PointerEventData eventData)
    {   // 드래그중 호출

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {   // 클릭 땔때 호출

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {   // 드래그 하는 순간        

    }       // OnBeginDrag()


    #endregion Interface Methods



}       // ClassEnd

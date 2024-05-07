using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{   // 상시로 레이를 쏘며 상호작용할 오브젝트

    private LayerMask targetLayer = default;
    public bool isRayCast = false;

    private RaycastHit hitInfo = default;
    private Vector3 mouseScreenPosition = default;
    private Vector3 mouseWorldPosition = default;
    private MouseInteractionObj lastHitObj = null;
    private void Awake()
    {
        InGameManager.Instance.mouseRoot = this;
        this.isRayCast = false;
        this.targetLayer = 1 << 11;

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

        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, targetLayer))
        {
            if(hitInfo.transform.GetComponent<MouseInteractionObj>() == true)
            {
                lastHitObj = hitInfo.transform.GetComponent<MouseInteractionObj>();
                lastHitObj.ObjFunction();
            }
        }
        else
        {
            if(lastHitObj != null)
            {
                lastHitObj.EndObjFunction();
                lastHitObj = null;
            }
        }


    }       // Update()


}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test004 : MonoBehaviour
{

    private RaycastHit hitInfo;
    private LayerMask targetMask = default;
    // Start is called before the first frame update
    void Start()
    {
        this.targetMask = 1 << 13;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 스크린 좌표 얻기
        Vector3 mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));

        if(Physics.Raycast(mouseWorldPosition,Vector3.forward, out hitInfo,Mathf.Infinity,targetMask))
        {
            if (hitInfo.collider.GetComponent<IDamageable>() != null)
            {
                hitInfo.collider.GetComponent<IDamageable>().IAttacked(1);
            }
        }   
    }       //  Update()
}

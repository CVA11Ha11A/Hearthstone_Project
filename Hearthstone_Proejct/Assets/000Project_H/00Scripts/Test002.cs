using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Test002 : MonoBehaviour
{
    private Vector3 mouseScreenPosition;
    private Vector3 mouseWorldPosition;
    private RaycastHit hitInfo;
    private int CardLayer;

    private void Awake()
    {
        CardLayer = 1 << 6;
        
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        

        // 마우스 스크린 좌표 얻기
        mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));
        
       

        // ----------------------------------------------------------- Card관련 --------------------------------------------


        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, CardLayer))
        {
            
            DE.Log("Ray가 CardLayer를 검출하였음");
            
            

        }
        
    }


}

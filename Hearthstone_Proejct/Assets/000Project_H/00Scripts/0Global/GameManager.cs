using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GameManager : MonoBehaviour
{       // 싱글턴

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                GameObject manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    public InGamePlayersDeck inGamePlayersDeck = null;

    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Application.targetFrameRate = 120;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != null)
        {
            Destroy(this.gameObject);
        }
        
        

    } // Awake


    public GameObject GetTopParent(Transform target_)
    {
        while (target_.parent != null)
        {
            target_ = target_.parent;
        }

        return target_.gameObject;
    }

    public RaycastHit RayCastMousePos(LayerMask targetLayer_)
    {   // 레이를 쏘아서 타겟을 리턴해주는 함수
        RaycastHit hitInfo;
        // 마우스 스크린 좌표 얻기
        Vector3 mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));

        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, targetLayer_))
        {
            return hitInfo;
        }
        else
        {
            return default;
        }
    }       // RayCastMousePos()

    public Vector3 GetMouseWorldPos()
    {   // 현재 위치의 마우스 포지션을 리턴해주는 함수
        Vector3 mouseScreenPosition = Input.mousePosition;        
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));

        return mouseWorldPosition;
    }

}       // GameManager Class

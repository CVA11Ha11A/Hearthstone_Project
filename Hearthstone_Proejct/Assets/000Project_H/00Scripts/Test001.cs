using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using TMPro;
using UnityEngine.UI;



[System.Serializable]
public class Test001 : MonoBehaviour
{
    public GameObject testObj;
    public GameObject hitObj;
    LayerMask layerMask;
    RaycastHit hitInfo;
    private void Awake()
    {
        layerMask = LayerMask.GetMask("Card");

    }

    private void Start()
    {
       
    }

    private void Update()
    { 
        // 마우스 스크린 좌표 얻기
       Vector3 mouseScreenPosition = UnityEngine.Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));
        if (Physics.Raycast(mouseWorldPosition,Vector3.forward,out hitInfo,Mathf.Infinity,layerMask))
        {
            hitObj = hitInfo.transform.gameObject;
            if(hitObj.Equals(testObj))
            {
                DE.Log("같은 개체");
            }
            
        }
        else
        {
            hitObj = null;
        }
    }




}








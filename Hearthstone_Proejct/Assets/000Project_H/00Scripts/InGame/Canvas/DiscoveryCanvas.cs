using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryCanvas : MonoBehaviour
{

    private GameObject[] discoveryObjs = null;

    private bool isDisCoverying = false;        // true면 클릭시 Ray를 쏘아서 선택 가능하도록 할 것
    private bool isMultiplechoices = false;     // 다중 선택이 가능한 발견인지 확인할 bool값 false면 Click하는 순간 선택한 카드 return
    private void Awake()
    {
        int loopCount = this.transform.childCount;
        discoveryObjs = new GameObject[loopCount];
        for(int i =0; i < loopCount; i++)
        {
            discoveryObjs[i] = this.transform.GetChild(i).gameObject;
        }
    }


    private void Update()
    {
        if(isDisCoverying == true)
        {
            // 찾기
            //if(Physics.Raycast())     // 마우스 포인트를 어떻게 받더라
        }
        else { /*PASS*/ }
    }

    void Start()
    {
        
    }

    
    
}

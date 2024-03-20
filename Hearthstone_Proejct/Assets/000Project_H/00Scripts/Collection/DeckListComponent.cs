using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListComponent : MonoBehaviour
{       // 컬렉션 창에서 플레이어의 덱 리스트들을 관리해줄 컴포넌트
    GameObject[] decks = default;
    private void Awake()
    {
        GetDecksObj();
    }



    void Start()
    {
        LobbyManager.Instance.newDeckCanvasRoot.startDeckBuildButtonEvent += DeckBuildStateDeckCreateButtonSet;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GetDecksObj()
    {       // Calling To Awake Time
        int childCount = this.gameObject.transform.childCount;
        decks = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            decks[i] = this.transform.GetChild(i).gameObject;
        }
    }       // GetDecksObj()

    /// <summary>
    /// 덱의 갯수가 변동될때마다 실행해야됨
    /// </summary>
    private void CreateDeckButtonControll()
    {
        for (int i = 0; i < decks.Length - 1; i++)
        {
            if (decks[i].gameObject.activeSelf == false)
            {
                decks[decks.Length].gameObject.SetActive(true);
                return;
            }
        }

        decks[decks.Length].gameObject.SetActive(false);
    }       // CreateDeckButtonControll()

    public void DeckBuildStateDeckCreateButtonSet(ClassCard selectClass_ , bool isDeckBuildMode_)
    {   // 덱 생성 모드가 됬을 경우 버튼의 움직임
        GameObject buttonParent = decks[^1];
        GameObject classImageObj = decks[^1].transform.GetChild(0).GetChild(0).gameObject;
        GameObject createDeckButtonObj = decks[^1].transform.GetChild(0).GetChild(1).gameObject;

        UnityEngine.UI.Image classImage = classImageObj.GetComponent<UnityEngine.UI.Image>();
        classImage.sprite = CardManager.Instance.classSprites[(int)selectClass_ - 1];

        StartCoroutine(SpinCreateDeckButton(buttonParent,classImageObj,createDeckButtonObj,isDeckBuildMode_));

    }       // DeckBuildStateDeckCreateButtonSet()

    private IEnumerator SpinCreateDeckButton(GameObject buttonParent_, GameObject classImageObj_ , 
        GameObject createDeckButtonObj_ ,bool isDeckBuildMode_)
    {   // isDeckBuildMode_ 해당 변수에 따라서 어떤 오브젝트가 SetAsLastSibling될지 결정
        float currentTime = 0f;
        float spinTime = 3f;

        Quaternion arrivalQuaternion = default;
        Quaternion setSiblingQuaterion = Quaternion.Euler(-90f,0f,0f);
        bool isSetSibling = false;

        if(isDeckBuildMode_ == true)
        {
            arrivalQuaternion = Quaternion.Euler(-180f, 0f, 0f);
        }
        else
        {
            arrivalQuaternion = Quaternion.Euler(0f, 0f, 0f);
        }

        while (currentTime < spinTime)
        {
            currentTime += Time.deltaTime;
            //decks            

            // 보간 비율 계산
            float t = currentTime/ spinTime;

            DE.Log($"LocalRoatation : {buttonParent_.transform.localRotation}\nSetSiblingQuaternion : {setSiblingQuaterion}");
            // Lerp 함수 사용하여 새로운 위치 계산
            Quaternion newQuaternion = Quaternion.Slerp(buttonParent_.transform.localRotation, arrivalQuaternion, t);            
            // 새로운 위치 적용
            buttonParent_.transform.localRotation = newQuaternion;

            if (buttonParent_.transform.localRotation.w <= setSiblingQuaterion.w &&isSetSibling == false &&
                isDeckBuildMode_ == true)
            {                
                isSetSibling = true;
                classImageObj_.transform.SetAsLastSibling();    
            }
            else if(buttonParent_.transform.localRotation.w >= setSiblingQuaterion.w && isSetSibling == false &&
                isDeckBuildMode_ == false)
            {             
                isSetSibling = true;
                createDeckButtonObj_.transform.SetAsLastSibling();
            }
                        
            yield return null;
        }
    }

    private void OnDestroy()
    {
        LobbyManager.Instance.newDeckCanvasRoot.startDeckBuildButtonEvent -= DeckBuildStateDeckCreateButtonSet;
    }
}       // DeckListComponent ClassEnd

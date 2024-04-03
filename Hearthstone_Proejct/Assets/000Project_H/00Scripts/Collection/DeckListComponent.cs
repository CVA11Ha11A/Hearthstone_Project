using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListComponent : MonoBehaviour
{       // 컬렉션 창에서 플레이어의 덱 리스트들을 관리해줄 컴포넌트
    GameObject[] decks = default;
    private void Awake()
    {
        // GetDecksObjs() 였던것
        int childCount = this.gameObject.transform.childCount;
        decks = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            decks[i] = this.transform.GetChild(i).gameObject;
        }

    }       // Awake()

    void Start()
    {
        LobbyManager.Instance.newDeckCanvasRoot.startDeckBuildButtonEvent += DeckBuildStateDeckCreateButtonSet;
        GameManager.Instance.GetTopParent(this.transform).transform.GetComponent<CollectionCanvasController>().
            BackButtonClassImageSpinEvent += LookingStateDeckCreateButtonSet;
    }       // Start()

    #region Deck내부 동작 관련

    public void UpdateOutputDeckList()
    {       // 이함수는 덱이 존재할시 1대1 매핑을 위한 함수
        int loopCount = this.transform.childCount - 1;
        for (int i = 0; i < loopCount; i++)
        {
            if (decks[i].transform.childCount == 1)
            {   // 댁 내부에 자식 Count가 0일경우     ! 1 인 이유는 내부에 UI를 모아둔 Object가 1개 존재하기 떄문
                for (int createObjCount = 0; createObjCount < 30; createObjCount++)
                {
                    GameObject tempObj = new GameObject("Card");
                    tempObj.transform.SetParent(decks[i].transform);
                }
            }

            for(int j = 1; j < 31; j++)
            {
                if(decks[i].transform.GetChild(j).GetComponent<Card>() == true)
                {
                    Destroy(decks[i].transform.GetChild(j).GetComponent<Card>());
                }
                else
                {
                    CardManager.Instance.InItCardComponent(decks[i].transform.GetChild(j).gameObject,
                        LobbyManager.Instance.playerDeckRoot.decks.deckList[i].cardList[j]);                    
                }
            }
        }


    }       // UpdateOutputDeckList()
    public void UpdateOutputDeckList(int targetIndex)
    {       // 이함수는 덱이 존재할시 1대1 매핑을 위한 함수        

        if (decks[targetIndex].transform.childCount == 1)
        {   // 댁 내부에 자식 Count가 0일경우     ! 1 인 이유는 내부에 UI를 모아둔 Object가 1개 존재하기 떄문
            for (int createObjCount = 0; createObjCount < 30; createObjCount++)
            {
                GameObject tempObj = new GameObject("Card");
                tempObj.transform.SetParent(decks[targetIndex].transform);
            }
        }

    }       // UpdateOutputDeckList()

    #endregion Deck내부 동작 관련

    /// <summary>
    /// 덱의 갯수가 변동될때마다 실행해야됨
    /// </summary>
    private void CreateDeckButtonControll()
    {        // ? 04.03 이게 뭐지?
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


    #region 버튼 움직이는 함수들
    public void DeckBuildStateDeckCreateButtonSet(ClassCard selectClass_, bool isDeckBuildMode_)
    {   // 덱 생성 모드가 됬을 경우 버튼의 움직임
        GameObject buttonParent = decks[^1];
        GameObject classImageObj = decks[^1].transform.GetChild(0).GetChild(0).gameObject;
        GameObject createDeckButtonObj = decks[^1].transform.GetChild(0).GetChild(1).gameObject;

        UnityEngine.UI.Image classImage = classImageObj.GetComponent<UnityEngine.UI.Image>();
        classImage.sprite = CardManager.Instance.classSprites[(int)selectClass_ - 1];

        StartCoroutine(SpinCreateDeckButton(buttonParent, classImageObj, createDeckButtonObj, isDeckBuildMode_));

    }       // DeckBuildStateDeckCreateButtonSet()

    public void LookingStateDeckCreateButtonSet(bool isLookingMode_)
    {
        GameObject buttonParent = decks[^1];
        GameObject classImageObj = decks[^1].transform.GetChild(0).GetChild(1).gameObject;
        GameObject createDeckButtonObj = decks[^1].transform.GetChild(0).GetChild(0).gameObject;
        StartCoroutine(SpinCreateDeckButton(buttonParent, classImageObj, createDeckButtonObj, isLookingMode_));
    }       // LookingStateDeckCreateButtonSet()


    private IEnumerator SpinCreateDeckButton(GameObject buttonParent_, GameObject classImageObj_,
        GameObject createDeckButtonObj_, bool isDeckBuildMode_)
    {   // isDeckBuildMode_ 해당 변수에 따라서 어떤 오브젝트가 SetAsLastSibling될지 결정
        float currentTime = 0f;
        float spinTime = 3f;

        Quaternion arrivalQuaternion = default;
        Quaternion setSiblingQuaterion = Quaternion.Euler(-90f, 0f, 0f);
        bool isSetSibling = false;

        if (isDeckBuildMode_ == true)
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
            float t = currentTime / spinTime;

            //DE.Log($"LocalRoatation : {buttonParent_.transform.localRotation}\nSetSiblingQuaternion : {setSiblingQuaterion}");
            // Lerp 함수 사용하여 새로운 위치 계산
            Quaternion newQuaternion = Quaternion.Slerp(buttonParent_.transform.localRotation, arrivalQuaternion, t);
            // 새로운 위치 적용
            buttonParent_.transform.localRotation = newQuaternion;

            if (isDeckBuildMode_ == true &&
                buttonParent_.transform.localRotation.w <= setSiblingQuaterion.w && isSetSibling == false)
            {
                //DE.Log("이미지가 LastSibling으로");
                isSetSibling = true;
                classImageObj_.transform.SetAsLastSibling();
            }
            else if (isDeckBuildMode_ == false &&
                buttonParent_.transform.localRotation.w <= setSiblingQuaterion.w && isSetSibling == false)
            {
                //DE.Log("버튼이 LastSibling으로");
                isSetSibling = true;
                createDeckButtonObj_.transform.SetAsLastSibling();
            }

            yield return null;
        }
        // TODO : 덱에 넣었을때 정렬에 이상이 없을 경우 직업의 이미지가 돌아가고 올라가는것 까지 추가
    }

    #endregion 버튼 움직이는 함수들

    private void OnDestroy()
    {
#if DEVELOP_TIME
        return;
#endif
        LobbyManager.Instance.newDeckCanvasRoot.startDeckBuildButtonEvent -= DeckBuildStateDeckCreateButtonSet;
        GameManager.Instance.GetTopParent(this.transform).transform.GetComponent<CollectionCanvasController>().
            BackButtonClassImageSpinEvent -= LookingStateDeckCreateButtonSet;
    }
}       // DeckListComponent ClassEnd

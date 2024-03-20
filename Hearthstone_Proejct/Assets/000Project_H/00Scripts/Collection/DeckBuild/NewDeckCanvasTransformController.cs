using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeckCanvasTransformController : MonoBehaviour
{

    private Vector3 onPos = default;
    private Vector3 defaultPos = default;
    private float canvasMoveTime = default;

    public event Action BackButtonEvent;
    public event Action<ClassCard, bool> startDeckBuildButtonEvent; // 선택버튼 누를경우 발생할 이벤트 

    private void Awake()
    {
        LobbyManager.Instance.newDeckCanvasRoot = this;
        defaultPos = this.transform.position;
        onPos = this.transform.position;
        onPos.x = 0f;
        canvasMoveTime = 2.5f;
    }

    public void OutputCanvas()
    {
        LobbyManager.Instance.CanvasOpen(this.transform, onPos, canvasMoveTime);
    }

    public void BackButtonOnClickMethod()
    {   // 뒤로가기 버튼이 눌릴경우 실행될 기능

        LobbyManager.Instance.CanvasClose(this.transform, defaultPos, canvasMoveTime);
        BackButtonEvent?.Invoke();
        this.transform.GetChild(2).GetComponent<DeckBuildSelectingClass>().lastChoiceClass = ClassCard.None;
    }       // BackButtonOnClickMethod()

    public void SelectButtonOnClickMethod()
    {   // 영웅 선택 버튼을 누를경우        
        if (this.transform.GetChild(2).GetComponent<DeckBuildSelectingClass>().lastChoiceClass != ClassCard.None)
        {
            LobbyManager.Instance.CanvasClose(this.transform, defaultPos, canvasMoveTime);
            LobbyManager.Instance.collectionCanvasRoot.NowState = CollectionState.DeckBuild;
            startDeckBuildButtonEvent?.Invoke(this.transform.GetChild(2).GetComponent<DeckBuildSelectingClass>().lastChoiceClass, true);
            this.transform.GetChild(2).GetComponent<DeckBuildSelectingClass>().lastChoiceClass = ClassCard.None;
        }
        else { /*PASS*/ }
    }       // SelectButtonOnClickMethod()

}       // ClassEnd

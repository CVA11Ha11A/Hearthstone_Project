using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckBuildSelectingClass : MonoBehaviour
{
    private Transform calssImageObjRoot = null;
    public ClassCard lastChoiceClass = default;

    private void Awake()
    {
        calssImageObjRoot = this.transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        this.transform.parent.GetChild(1).GetChild(1).GetComponent<DeckBuildSeclectClassInstance>().ClickToClassEvent += SelectClass;
        GameManager.Instance.GetTopParent(this.transform).GetComponent<NewDeckCanvasTransformController>().BackButtonEvent += SetInitialState;
    }

    public void SelectClass(ClassCard eventParam_)
    {       
        lastChoiceClass = eventParam_;
        calssImageObjRoot.gameObject.SetActive(true);
        calssImageObjRoot.GetComponent<UnityEngine.UI.Image>().sprite = this.transform.parent.GetChild(1).GetChild(1)
            .GetComponent<DeckBuildSeclectClassInstance>().classSprites[(int)eventParam_ - 1];
    }       // SelectClass()

    private void OnDestroy()
    {        
        this.transform.parent.GetChild(1).GetChild(1).GetComponent<DeckBuildSeclectClassInstance>().ClickToClassEvent -= SelectClass;
        GameManager.Instance.GetTopParent(this.transform).GetComponent<NewDeckCanvasTransformController>().BackButtonEvent -= SetInitialState;
    }

    public void SetInitialState()
    {   // 뒤로가기 누를경우 초기상태로 돌려놓기 위한 함수
        calssImageObjRoot.gameObject.SetActive(false);
    }
}       // ClassEnd

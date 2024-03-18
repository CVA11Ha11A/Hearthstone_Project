using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeckBuildSelectingClass : MonoBehaviour
{
    private Transform calssImageObjRoot = null;

    private void Awake()
    {
        calssImageObjRoot = this.transform.GetChild(0).GetChild(0).GetComponent<Transform>();
        this.transform.parent.GetChild(1).GetChild(1).GetComponent<DeckBuildSeclectClassInstance>().ClickToClassEvent += SelectClass;
    }

    public void SelectClass(ClassCard eventParam_)
    {       
        calssImageObjRoot.gameObject.SetActive(true);
        calssImageObjRoot.GetComponent<UnityEngine.UI.Image>().sprite = this.transform.parent.GetChild(1).GetChild(1)
            .GetComponent<DeckBuildSeclectClassInstance>().classSprites[(int)eventParam_ - 1];
    }       // SelectClass()

    private void OnDestroy()
    {
        this.transform.parent.GetChild(1).GetChild(1).GetComponent<DeckBuildSeclectClassInstance>().ClickToClassEvent -= SelectClass;
    }

}       // ClassEnd

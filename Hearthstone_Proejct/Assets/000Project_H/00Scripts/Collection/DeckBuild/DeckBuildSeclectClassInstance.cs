using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuildSeclectClassInstance : MonoBehaviour
{
    public Sprite[] classSprites;
    public string[] classNames = null;

    public GameObject selectClassPrefab = null;

    public event Action<ClassCard> ClickToClassEvent;

    private void Awake()
    {
        AwakeInIt();
        InstanceSelectClassPrefab();

    }       // Awake()

    private void InstanceSelectClassPrefab()
    {
        int instanceCount = Enum.GetValues(typeof(ClassCard)).Length;       
        
        for (int i = 1; i < instanceCount -1; i++)
        {
            GameObject instance = Instantiate(selectClassPrefab, this.transform);
            SelectHeroPrefab prefabClass = instance.GetComponent<SelectHeroPrefab>();
            prefabClass.prefabClickEvent += ClassImageCallBackCompleate;
            prefabClass.ThisClass = (ClassCard)i;
        }

    }       // InstanceSelectClass()


    private void AwakeInIt()
    {
        classNames = new string[Enum.GetValues(typeof(ClassCard)).Length - 1];
        classNames[0] = "사제";
        classNames[1] = "마법사";

        selectClassPrefab = Resources.Load<GameObject>("SelectHeroPrefab");
    }

    private void ClassImageCallBackCompleate(ClassCard callbackParam_)
    {       // SelectHeroPrefab에서 버튼이 눌려 Click이 되는 순간에 이벤트 발생으로 호출될 함수임
            // DeckBuildSelectingClass의 함수가 결국 실행될 것임

        ClickToClassEvent?.Invoke(callbackParam_);

    }       // ClassImageCallBackCompleate()


    private void OnDestroy()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {   // 이벤트 해지 
            if(this.transform.GetChild(i).GetComponent<SelectHeroPrefab>())
            {
                this.transform.GetChild(i).GetComponent<SelectHeroPrefab>().prefabClickEvent -= ClassImageCallBackCompleate;
            }
            else { /*PASS*/ }
        }
    }       // OnDestroy()

}       // ClassEnd

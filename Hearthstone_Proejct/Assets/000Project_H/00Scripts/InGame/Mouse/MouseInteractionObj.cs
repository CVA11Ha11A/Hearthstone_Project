using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseInteractionObj : MonoBehaviour
{
    public bool isInteraction = default;    

    protected virtual void Awake()
    {        
        this.isInteraction = false;
        this.gameObject.layer = LayerMask.NameToLayer("MouseInteraction");
    }
    
    public virtual void ObjFunction()
    {
        // 오브젝트의 기능을 실행하는 함수
    }       // ObjFunction()

    public virtual void EndObjFunction()
    {
        // 오브젝트의 기능을 종료하는 함수
    }       // EndObjFunction()

}       // ClassEnd

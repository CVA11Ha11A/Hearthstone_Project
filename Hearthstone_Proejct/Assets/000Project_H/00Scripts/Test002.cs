using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Test002 : MouseInteractionObj
{
    private void Awake()
    {
        
        
    }
    private void Start()
    {
       
    }

    public override void ObjFunction()
    {
        DE.Log("자식 클래스의 함수가 호출됨");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test002 : MonoBehaviour
{
    public GameObject test001Obj = null;
    Test001 test001 = null;

    private void Start()
    {
        test001 = test001Obj.GetComponent<Test001>();
        test001.data.tempStr = "TEST002가 수정했음";
    }
}

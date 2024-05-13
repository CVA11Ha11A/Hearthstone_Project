using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Test002 : MonoBehaviour
{


    private void Awake()
    {
    
        
    }
    private void Start()
    {
       
    }

    public void IAttacked(int damage_)
    {
        DE.Log($"Test002 인터페이스 함수 호출됨");
    }

}

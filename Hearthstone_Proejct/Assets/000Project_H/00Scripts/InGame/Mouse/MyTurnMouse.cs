using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTurnMouse : MonoBehaviour
{   // 나의 턴에만 작동될 상호작용 마우스

    private int targetLayer = default;

    private void Awake()
    {
        this.targetLayer = 1 << 6;      // Card
    }
    void Start()
    {
        
    }




}       // ClassEnd

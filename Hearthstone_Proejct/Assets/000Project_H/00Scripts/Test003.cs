using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Test003 : MonoBehaviour
{
    private void Awake()
    {
        int[] tArr = new int[6];
        DE.Log($"Num : {(int)RClassVerticalSprite.EndPoint}");
        tArr[6] = 0;        
    }
}

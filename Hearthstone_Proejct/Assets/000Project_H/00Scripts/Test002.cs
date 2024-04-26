using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Test002 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log($"씬 로드는 되나? : Awake");
    }
    private void Start()
    {
        Debug.Log($"씬 로드는 되나? : Start");
    }
}

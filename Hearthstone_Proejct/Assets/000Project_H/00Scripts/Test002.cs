using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Test002 : MonoBehaviour
{
    private void Awake()
    {
        StringBuilder sb = new StringBuilder();
        sb.Clear();
        sb.Append("대충Path_").Append("대충영웅번호09_").Append(EEmoteClip.Oops);
        DE.Log($"어떻게 나올까? : {sb.ToString()}");
        
    }
    private void Start()
    {
        Debug.Log($"씬 로드는 되나? : Start");
    }
}

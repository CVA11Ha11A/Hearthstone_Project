using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPage : MonoBehaviour
{
    public const int MIN_PAGE = 1;
    public int nowPage = default;
    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().pageRoot = this;
        nowPage = MIN_PAGE;
    }
    void Start()
    {
        
    }

    
}       // CollectionPage ClassEnd

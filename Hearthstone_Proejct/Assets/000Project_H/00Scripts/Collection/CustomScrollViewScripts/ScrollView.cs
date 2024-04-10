using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScrollView : MonoBehaviour
{    
    public ScrollViewContent scrollViewContent = null;  // AwakeInIt    
    private readonly int maxCurrentViewSize = -290;      // oneBlockSize * maxCardCount + 1 (1 = 공백으로 마지막 알려주는 것)    
    private int currentView = default;                  // default : 0
    public int CurrentView
    {
        get
        {
            return this.currentView;
        }  
        set
        {
            // 1. CurrentView의 변경과 예외처리
            if(value < 0)
            {
                this.currentView = 0;
            }
            else
            {
                this.currentView = value;
            }
            if(this.currentView < maxCurrentViewSize)
            {
                this.currentView = maxCurrentViewSize;
            }
            // 2. 이동될 포지션 값할당과 변경
            if(scrollViewContent != null)
            {
                this.scrollViewContent.changedContentPos = new Vector3(this.scrollViewContent.contentRect.anchoredPosition3D.x,
                    this.currentView, this.scrollViewContent.contentRect.anchoredPosition3D.z);
                this.scrollViewContent.contentRect.anchoredPosition3D = this.scrollViewContent.changedContentPos;
            }
            
        }
    }
    // 카드 1개의 크기 20
    // 한줄에 담을수 있는 크기 340
    // 현재 켜져 있는카드 수에 따라서 +20 이 되며 340을 넘으면 스크롤 가능
    // 스크롤시 ViewPoint의 top 이 -20 씩 되면 올라감 
    // CollectionState가 변경될때마다 움직인거 초기화 되면됨

    private void Awake()
    {        
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasCardInteraction>().scrollViewRoot = this;
        scrollViewContent = new ScrollViewContent();
        scrollViewContent.contentRect = this.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();    
        

    }

    void Start()
    {
        
    }

}       // ClassEnd

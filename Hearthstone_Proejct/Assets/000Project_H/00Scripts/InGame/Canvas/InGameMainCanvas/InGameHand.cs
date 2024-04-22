using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHand : MonoBehaviour
{       // 핸드에 들어온 카드 옵젝트들을 관리할 것임
    
    public List<GameObject> handCard = null;
    private int maxHandCount = 10;
    public int MaxHandCount
    {
        get
        {
            return this.maxHandCount;
        }        
    }
    private void Awake()
    {
        handCard = new List<GameObject>(12);
        maxHandCount = 10;
    }



    public void SetterMaxHandCount(int newMaxHandCount_)
    {   // 최대 가드 소지 갯수를 늘려주는 함수
        this.maxHandCount = newMaxHandCount_;
    }



}       // ClassEnd

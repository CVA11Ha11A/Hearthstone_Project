using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveryCard : MonoBehaviour
{   // 카드를 발견할때 사용될 컴포넌트 내가 클릭 당했는지 체크할것

    private bool isClick = false;

    public bool IsClick
    {
        get
        {
            return this.isClick;
        }
        set
        {
            if(this.IsClick != value)
            {
                this.isClick = value;
            }
        }
    }

    private void Awake()
    {
        isClick = false;
    }


}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (this.isClick == true)
            {
                ScaleSetter(onClickScale);
            }
            else
            {
                ScaleSetter(nonClickScale);
            }

        }
    }

    private Vector3 nonClickScale = default;
    private Vector3 onClickScale = default;

    private void Awake()
    {
        isClick = false;
        nonClickScale = new Vector3(15f, 15f, 15f);
        onClickScale = new Vector3(12f, 12f, 12f);
        this.transform.GetChild(0).localPosition = nonClickScale;
    }

    private void ScaleSetter(Vector3 scale_)
    {
        this.transform.GetChild(0).transform.localScale = scale_;
    }
    public void OnClick() 
    {
        if(this.IsClick == true)
        {
            this.IsClick = false;
        }
        else
        {
            this.IsClick = true;
        }
    }

}       // ClassEnd

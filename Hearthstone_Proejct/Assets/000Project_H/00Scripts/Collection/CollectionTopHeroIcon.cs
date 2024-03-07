using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTopHeroIcon : MonoBehaviour
{
    private bool isClick = false;
    public bool IsClick
    {
        get
        {
            return isClick;
        }
        set
        {
            if(isClick != value)
            {
                isClick = value;
            }
            if(isClick == true)
            {
                StartCoroutine(SelectOnIcon());
            }
            else
            {
                StartCoroutine(SelectOffIcon());
            }
        }
    }    

    public CollectionHeroIcon iconType = default;

    private Vector3 adjustVector = default;
    private void Awake()
    {
        isClick = false;
        adjustVector = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void IsOnClick()
    {
        this.transform.parent.GetComponent<SelectCardTheme>().CardThemeCheck(this);
    }       // IsOnClick()

    #region 아이콘 확대, 축소함수
    IEnumerator SelectOnIcon()
    {
        Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);        
        while (this.gameObject.transform.localScale != targetScale || targetScale.x > this.gameObject.transform.localScale.x)
        {
            this.gameObject.transform.localScale += adjustVector;
            if(this.gameObject.transform.localScale.x > targetScale.x)
            {
                this.gameObject.transform.localScale = targetScale;
            }
            yield return null;
        }
    }       // SelectOnIcon()

    IEnumerator SelectOffIcon()
    {
        Vector3 targetScale = Vector3.one;        
        while (this.gameObject.transform.localScale != targetScale || targetScale.x < this.gameObject.transform.localScale.x)
        {
            this.gameObject.transform.localScale -= adjustVector;
            if (this.gameObject.transform.localScale.x < targetScale.x)
            {
                this.gameObject.transform.localScale = targetScale;
            }
            yield return null;
        }
    }       // SelectOffIcon()
    #endregion 아이콘 확대, 축소함수
}       // CollectionTopHeroIcon ClassEnd

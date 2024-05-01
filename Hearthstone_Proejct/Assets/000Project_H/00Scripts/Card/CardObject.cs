using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{       // 인게임에서 오브젝트가 이상하게 보이는 이슈로 만들어진 컴포넌트임


    private void Awake()
    {
        this.transform.GetComponent<Canvas>().sortingLayerName = "Card";

    }
    private void Start()
    {
        if(this.transform.parent != null)
        {
            if (this.transform.parent.CompareTag("InGameDeck"))
            {
                this.transform.rotation = Quaternion.Euler(0f, -90f, 90f);                
            }
        }
    }


}       // ClassEnd

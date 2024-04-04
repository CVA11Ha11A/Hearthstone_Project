using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckListChild : MonoBehaviour
{    
    private void Awake()
    {
        this.transform.GetComponent<Button>().onClick.AddListener(OnClickDeck);   
    }

    private void OnClickDeck()
    {   // 컬렉션에서 제작된 덱이 눌렸을경우 실행될 함수 (버튼이 참조함)
        this.transform.parent.GetComponent<DeckListComponent>().DeckOnClick(this);
    }       // SelectDeck()

}       // ClassEnd

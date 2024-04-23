using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDeck : MonoBehaviour
{       // 플레이어의 덱을 관리 (저장 해둔 덱 X)
        // 덱을 새로 인스턴스 해야함
    Deck deck = null;

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    public void DeckInit()
    {       // 
        deck = new Deck();
    }
    
}       // ClassEnd

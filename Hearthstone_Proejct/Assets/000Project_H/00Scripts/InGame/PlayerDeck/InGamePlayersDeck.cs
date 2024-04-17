using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayersDeck : MonoBehaviour
{
    private Deck myDeck = null;
    public Deck MyDeck
    {
        get
        {
            return this.myDeck;
        }
        set
        {
            if(this.myDeck != value)
            {
                this.myDeck = value;
            }
        }
    }

    private Deck enemyDeck = null;
    public Deck EnemyDeck
    {
        get
        {
            return this.enemyDeck;
        }
        set
        {
            if(this.enemyDeck != value)
            {
                enemyDeck = value;
            }
        }
    }

    public bool isMyDeckInIt = false;       // bool 값 2개는 현재 적과 나의 덱을 포톤으로 받은것으로 초기화 시켰는지 확인할 변수
    public bool isEnemyDeckInIt = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        GameManager.Instance.inGamePlayersDeck = this;
    }

    public void ClearDecks()
    {   // 게임이 끝난후 새로운 매칭을 할때에는 새로 제작해야함
        this.myDeck = null;
        this.enemyDeck = null;
    }

}   // ClassEnd

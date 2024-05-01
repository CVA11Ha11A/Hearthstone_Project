using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameDecks : MonoBehaviour
{   // IngameDeck의 Root를 관리해줄 컴포넌트
    // 여기서 Root는 플레이어 자신 기준의 My와 Enemy를 가지고 있을것임
    private InGameDeck myInGameDeck = null;
    private InGameDeck enemyInGameDeck = null;
    public InGameDeck MyDeck
    {
        get { return this.myInGameDeck; }
    }
    public InGameDeck EnemyDeck
    {
        get
        {
            return this.enemyInGameDeck;
        }
    }

    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<InGameMainCanvas>().decksRoot = this;
    }

    public void MyDeckSetter(InGameDeck root_)
    {
        this.myInGameDeck = root_;
    }
    public void EnemyDeckSetter(InGameDeck root_)
    {
        this.enemyInGameDeck = root_;
    }
}       // ClassEnd

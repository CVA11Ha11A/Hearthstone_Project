using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHands : MonoBehaviour
{
    private InGameHand myHand = null;
    private InGameHand enemyHand = null;
    public InGameHand MyHand
    {
        get
        {
            return this.myHand;
        }
    }
    public InGameHand EnemyHand
    {
        get
        {
            return this.enemyHand;
        }
    }
    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<InGameMainCanvas>().handRoot = this;
    }
    public void SetterMyHand(InGameHand root_)
    {
        this.myHand = root_; 
    }
    public void SetterEnemyHand(InGameHand root_)
    {
        this.enemyHand = root_;
    }

}       // ClassEnd

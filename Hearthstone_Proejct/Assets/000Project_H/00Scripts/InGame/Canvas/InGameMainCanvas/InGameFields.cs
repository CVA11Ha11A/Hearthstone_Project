using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameFields : MonoBehaviour
{
    private InGameField myField = null;
    public InGameField MyField
    {
        get
        {
            return this.myField;
        }
    }
    private InGameField enemyField = null;
    public InGameField EnemyField
    {
        get
        {
            return this.enemyField;
        }
    }

    void Start()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<InGameMainCanvas>().fieldRoot = this;
    }

    public void MyFieldSetter(InGameField root_)
    {
        this.myField = root_;
    }
    public void EnemyFieldSetter(InGameField root_)
    {
        this.enemyField = root_;
    }


}       // ClassEnd

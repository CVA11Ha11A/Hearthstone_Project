using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameField : MonoBehaviour
{

    private int nowMinionCount = default;
    public int NowMinionCount
    {
        get
        {
            this.nowMinionCount = this.transform.childCount;
            return this.nowMinionCount;
        }
        set
        {
            if (this.nowMinionCount != value)
            {
                this.nowMinionCount = value;
            }
        }
    }

    public const int MAX_MINON_COUNT = 6;


    private void Awake()
    {
        if (this.transform.name == "MyField")
        {
            this.transform.parent.GetComponent<InGameFields>().MyFieldSetter(this);
        }
        else if (this.transform.name == "EnemyField")
        {
            this.transform.parent.GetComponent<InGameFields>().EnemyFieldSetter(this);
        }
    }

    void Start()
    {

    }


}       // ClassEnd

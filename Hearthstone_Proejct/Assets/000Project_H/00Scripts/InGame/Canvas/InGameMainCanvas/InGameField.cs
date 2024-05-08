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

    private GameObject recentFieldObjRoot = null;
    public GameObject RecentFieldObjRoot
    {
        get
        {
            return this.recentFieldObjRoot;
        }
    }

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

    public void SpawnMinion()
    {   // 하수인 소환전 필드의 자리를 잡아두는 함수
        GameObject fieldObj = new GameObject("FieldObj");
        fieldObj.transform.parent = this.transform;
        fieldObj.AddComponent<RectTransform>();
        fieldObj.transform.localPosition = Vector3.zero;
        recentFieldObjRoot = fieldObj;
    }       // SpawnMinion()


}       // ClassEnd

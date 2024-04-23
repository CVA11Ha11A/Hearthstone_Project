using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class InGamePlayersCost : MonoBehaviour
{       // 인게임에서 플레이어들의 코스트를 관리할 컴포넌트

    private int maxCost = default;      // 게임의 최대 코스트 (Setter를 이용해서 변화 가능)
    public int MaxCost
    {
        get
        {
            return this.maxCost;
        }
    }
    private int nowCost = default;      // 현재 코스트
    public int NowCost
    {
        get
        {
            return this.nowCost;
        }
        set
        {
            this.nowCost = value;
            CostTextUpdate();
        }
    }

    public int nowMaxCost = default;        // 현재 코스트 최대치

    private GameObject[] costObjs = null;       // 코스트 의 오브젝트들
    private TextMeshProUGUI costText = null;

    private Color32 costOnColor = default;      // 사용되지 않은 코스트의 색
    private Color32 costOffColor = default;     // 사용된 코스트의 색

    public StringBuilder sb = null;
    public InGamePlayersCost()
    {
        maxCost = 10;
    }

    private void Awake()
    {
        int costObjsLength = this.transform.GetChild(0).childCount;
        costObjs = new GameObject[costObjsLength];
        for(int i = 0; i < costObjsLength; i++)
        {
            costObjs[i] = this.transform.GetChild(0).GetChild(i).gameObject;
        }

        costText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        costOnColor = new Color32(255, 255, 255, 255);
        costOffColor = new Color32(150, 150, 150, 255);
        sb = new StringBuilder();

    }


    public void MaxCostSetter(int cost_)
    {        
        this.maxCost = cost_;
    }

    public void CostTextUpdate()
    {   // 코스트 텍스트를 업데이트 해줌 NowCost/NowMaxCost
        sb.Clear();
        sb.Append(NowCost);
        sb.Append("/");
        sb.Append(nowMaxCost);
    }       // CostTextUpdate()



}       // ClassEnd

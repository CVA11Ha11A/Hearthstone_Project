using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            CostColorSetOffColor();
            CostTextUpdate();
            
            // 코스트 이미지 RGB 145로 변경해야함
            
        }
    }

    private int nowMaxCost = default;        // 현재 코스트 최대치
    public int NowMaxCost
    {
        get
        {
            return this.nowMaxCost;
        }
        set
        {
            if (this.nowCost != value)
            {
                this.nowMaxCost = value;             

            }

        }
    }

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
        costOnColor = new Color32(255, 255, 255, 255);
        costOffColor = new Color32(150, 150, 150, 255);
        sb = new StringBuilder();
        this.nowCost = 0;
        this.nowMaxCost = 0;

        if (this.transform.name == "MyCost")
        {
            this.transform.parent.GetComponent<InGamePlayersCosts>().MyCostSetter(this);
        }
        else if (this.transform.name == "EnemyCost")
        {
            this.transform.parent.GetComponent<InGamePlayersCosts>().EnemyCostSetter(this);
        }
        int costObjsLength = this.transform.GetChild(0).childCount;
        costObjs = new GameObject[costObjsLength];
        for (int i = 0; i < costObjsLength; i++)
        {
            costObjs[i] = this.transform.GetChild(0).GetChild(i).gameObject;
            costObjs[i].SetActive(false);
        }

        costText = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();


    }

    private void Start()
    {
        CostTextUpdate();
    }

    public void MaxCostSetter(int cost_)
    {
        this.maxCost = cost_;
    }

    private void CostColorSetOffColor()
    {
        if(NowMaxCost > NowCost)
        {
            for(int i = NowMaxCost; i > NowCost; i--)
            {   // for : 역순으로 소비된 코스트 만큼 Color조정 
                costObjs[i].GetComponent<Image>().color = costOffColor;
            }
        }
        else { /*PASS*/ }
    }       // CostColorSet()

    public void CostTextUpdate()
    {   // 코스트 텍스트를 업데이트 해줌 NowCost/NowMaxCost
        sb.Clear();
        sb.Append(NowCost);
        sb.Append("/");
        sb.Append(NowMaxCost);
        costText.text = sb.ToString();
    }       // CostTextUpdate()

    public void TurnStartCostSetting()
    {   // 턴시작시 코스트 변동사항
        this.NowMaxCost++;
        NowCost = this.NowMaxCost;
        for(int i = 0; i < NowMaxCost; i++)
        {   // 코스트
            if (costObjs[i].gameObject.activeSelf == false)
            {
                costObjs[i].SetActive(true);
                costObjs[i].GetComponent<Image>().color = costOnColor;
            }
        }

    }       // TurnStartCostSetting()

}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectionHeroIcon
{
    Defulat = 0,
    Prist = 1,
    Common = 2
}
public class SelectCardTheme : MonoBehaviour
{
    public List<CollectionTopHeroIcon> heroIconList = default;

    private CollectionCanvasController collectionCanvasController = default;

    private CollectionHeroIcon nowPage = default;

    private void Awake()
    {
        heroIconList = new List<CollectionTopHeroIcon>();
        AwakeInIt();

    }       // Awake()

    void Start()
    {
        StartInIt();        
    }

    private void AwakeInIt()
    {
        CollectionTopHeroIcon refRoot = null;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            heroIconList.Add(this.transform.GetChild(i).GetComponent<CollectionTopHeroIcon>());
            // 03.04기준 IconNum을 어떻게 가독성을 챙길지 발상을 못하겠음 Enum으로 해도 어려움이 존재
            // 03.08 명시적 캐스팅을 이용해서 int 정수를 해당 enum타입으로 변경한다고 명시후 0 = default로 넣어놓았기 때문에 +1 해줌       
            refRoot = this.transform.GetChild(i).GetComponent<CollectionTopHeroIcon>();
            refRoot.iconType = (CollectionHeroIcon)i + 1;
            refRoot.cardClass = (ClassCard)i + 1;            
        }
        heroIconList[0].IsClick = true;
        nowPage = heroIconList[0].iconType;

    }       // AwakeInIt()

    private void StartInIt()
    {
        collectionCanvasController = GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>();
        collectionCanvasController.NowPageIcon = nowPage;
    }

    public void CardThemeCheck(CollectionTopHeroIcon clickComponent_)
    {
        for (int i = 0; i < heroIconList.Count; i++)
        {
            if (heroIconList[i] == clickComponent_)
            {
                heroIconList[i].IsClick = true;
                nowPage = heroIconList[i].iconType;
                collectionCanvasController.NowPageIcon = nowPage;
            }
            else
            {
                heroIconList[i].IsClick = false;
            }
        }
    }       // CardThemeCheck()



}       // SelectCardTheme ClassEnd

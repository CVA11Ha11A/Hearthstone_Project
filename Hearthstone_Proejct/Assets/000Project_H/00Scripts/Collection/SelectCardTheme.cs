using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCardTheme : MonoBehaviour
{
    public List<CollectionTopHeroIcon> heroIconList;

    private void Awake()
    {
        heroIconList = new List<CollectionTopHeroIcon>();
        InItIconList();
    }       // Awake()

    void Start()
    {
        
    }

    private void InItIconList()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            heroIconList.Add(this.transform.GetChild(i).GetComponent<CollectionTopHeroIcon>());
            // 03.04기준 IconNum을 어떻게 가독성을 챙길지 발상을 못하겠음 Enum으로 해도 어려움이 존재
            // 0 = 사제, 1 = 공용
            this.transform.GetChild(i).GetComponent<CollectionTopHeroIcon>().iconNumber = i;
        }
        heroIconList[0].IsClick = true;
    }       // InItIconList()
    public void CardThemeCheck(CollectionTopHeroIcon clickComponent_)
    {
        for(int i = 0; i < heroIconList.Count; i++)
        {
            if (heroIconList[i] == clickComponent_)
            {
                heroIconList[i].IsClick = true;
            }
            else
            {
                heroIconList[i].IsClick = false;
            }
        }
    }       // CardThemeCheck()

}       // SelectCardTheme ClassEnd

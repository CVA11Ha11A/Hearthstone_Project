using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERootIndex
{   // InGame 배열 용
    My = 0,
    Enemy = 1
}
public class InGameMainCanvas : MonoBehaviour
{
    private HeroImage[] heroImageRoots = null;      // ChildNum 0.6
    private InGameHand[] ingameHandRoots = null;    // ChildNum 0.5

    private void Awake()
    {
        GetRoots();
    }

    private void GetRoots()
    {
        // 영웅 이미지
        int heroImageChildCount = this.transform.GetChild(0).GetChild(6).childCount;
        heroImageRoots = new HeroImage[heroImageChildCount];
        for (int i = 0; i < heroImageChildCount; i++)
        {
            heroImageRoots[i] = this.transform.GetChild(0).GetChild(6).GetChild(i).GetComponent<HeroImage>();
        }
        // 핸드
        int handChildCount = this.transform.GetChild(0).GetChild(5).childCount;
        ingameHandRoots = new InGameHand[handChildCount];
        for(int i = 0; i < handChildCount; i++)
        {
            ingameHandRoots[i] = this.transform.GetChild(0).GetChild(5).GetChild(i).GetComponent<InGameHand>();
        }


    }       // GetRoot()

}       // ClassEnd
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public enum ERootIndex
{   // InGame 배열 용
    My = 0,
    Enemy = 1
}
public class InGameMainCanvas : MonoBehaviour
{
    public HeroImages heroImagesRoot = null;      // ChildNum 0.6
    public InGameDecks decksRoot = null;    
    public InGameHands handRoot = null;
    public InGamePlayersCost[] ingameCostRoots = null; // 0.2.0.0

    private void Awake()
    {
        //PhotonNetwork.Instantiate("InGameManager", Vector3.zero, Quaternion.identity);
        InGameManager.Instance.mainCanvsRoot = this;
        AudioManager.Instance.SceneMoveBGMPlay();
        GetRoots();
    }

    private void GetRoots()
    {
        // 영웅 이미지        
        heroImagesRoot = this.transform.GetChild(0).GetChild(6).GetComponent<HeroImages>();             
      
        // 코스트
        int costChildCount = this.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).childCount;
        ingameCostRoots = new InGamePlayersCost[costChildCount];
        for(int i =0; i < costChildCount; i++)
        {
            ingameCostRoots[i] = this.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).GetComponent<InGamePlayersCost>();
        }


    }       // GetRoot()

}       // ClassEnd

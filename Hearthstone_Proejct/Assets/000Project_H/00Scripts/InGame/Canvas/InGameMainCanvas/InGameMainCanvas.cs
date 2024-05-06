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
    public TurnUis turnUIRoot = null;
    public InGamePlayersCosts costRoot = null;

    private void Awake()
    {
        //PhotonNetwork.Instantiate("InGameManager", Vector3.zero, Quaternion.identity);
        InGameManager.Instance.mainCanvasRoot = this;
        AudioManager.Instance.SceneMoveBGMPlay();
        GetRoots();
    }

    private void GetRoots()
    {
        // 영웅 이미지        
        heroImagesRoot = this.transform.GetChild(0).GetChild(6).GetComponent<HeroImages>();

        // 턴 UI
        turnUIRoot = this.transform.GetChild(0).GetChild(8).GetComponent<TurnUis>();

        // 코스트
        costRoot = this.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetComponent<InGamePlayersCosts>();

    }       // GetRoot()

}       // ClassEnd

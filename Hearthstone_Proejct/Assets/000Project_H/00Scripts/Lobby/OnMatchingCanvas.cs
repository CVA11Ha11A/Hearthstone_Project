using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMatchingCanvas : MonoBehaviour
{
    private Button stopMatchingButton = null;


    private void OnEnable()
    {
        stopMatchingButton = this.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        stopMatchingButton.enabled = true;
    }


    private void Awake()
    {
        
    }

    void Start()
    {
        OffImages();

    }

    

    public void OffImages()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OnImages()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void StopMatchingButton()
    {   // 매칭 잡는 중이라면 매칭을 멈추어야함 
                
        // 1 포톤 매칭 종료 시켜야함
        GameManager.Instance.GetTopParent(this.transform).GetComponent<LobbyPhoton>().StopAllCoroutines();
        GameManager.Instance.GetTopParent(this.transform).GetComponent<LobbyPhoton>().ConnectPhotonServer();

        // 2 매칭 에니메이션 꺼야함
        this.transform.GetChild(0).GetComponent<MacthingStartScrollController>().StopAllCoroutines();
        this.transform.GetChild(0).GetComponent<MacthingStartScrollController>().IsScrollring = false;
        OffImages();

        // 3 매칭 사운드 강제 종료 시켜야함 (종료시키면 다시 메인 음악 자동재생되면 정상)
        AudioManager.Instance.PlayStopBGM(ESoundBGM.MatchingTheme1, ESoundBGM.MatchingTheme2);
        

    }       // StopMatchingButton()

    public void StopMatchingButtonEnable()
    {       // 버튼의 기능 활성화
        stopMatchingButton.enabled = true;
    }
    public void StopMatchingButtonDisable()
    {       // 버튼의 기능 비활성화 (매칭 되었을때 외부 호출)
        stopMatchingButton.enabled = false;
    }

    

}       // ClassEnd

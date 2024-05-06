using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUis : MonoBehaviour
{
    private GameObject uiObj = null;        // 이미지 컴포넌트를 가지고 있는 Ui 오브젝트
    private Coroutine coroutine = null;    

    private Vector3 durationScale = default;

    private void Awake()
    {
        uiObj = this.transform.GetChild(0).gameObject;
        uiObj.SetActive(false);
    }


    void Start()
    {        
        durationScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public void YourTurnAnime()
    {   // 외부 호출용 함수
        coroutine = StartCoroutine(CYourTurnAnime());
    }

    public IEnumerator CYourTurnAnime()
    {   // 코루틴 yeild return을 위한 public
        float durationTime = 1f;
        float elapsedTime = 0f;

        uiObj.SetActive(true);
        // 턴 사운드 플레이
        AudioManager.Instance.PlaySFM(false, AudioManager.Instance.SFMClips[(int)ESoundSFM.TurnStart]);

        yield return new WaitForSeconds(0.5f);

        while (elapsedTime < durationTime)
        {
            uiObj.transform.localScale = Vector3.Lerp(uiObj.transform.localScale, durationScale, elapsedTime);

            if(uiObj.transform.localScale.x < 0.1f)
            {
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        uiObj.transform.localScale = Vector3.one;
        uiObj.gameObject.SetActive(false);
        // 턴에대한 기능 실행
        // 이건 포톤으로 동기화 시켜주어야하기에 호출 해야할듯


    }       // CYourTurnAnime()


}   // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{
    private GameObject[] uiObjs = null;
    private Vector3 targetScale = default;

    private bool isNextSceneMove = false;           // 씬 로드 중인가?
    private bool isReadyToNextSceneMove = false;    // 씬을 넘어갈 준비가 되었는가?

    private void Awake()
    {
        this.transform.parent.GetComponent<FrontGroundCanvas>().gameEndUiRoot = this;
    }

    private void Start()
    {
        int uiObjsLength = this.transform.childCount;
        this.uiObjs = new GameObject[uiObjsLength];

#if UNITY_EDITOR
        this.targetScale = new Vector3(0.8f, 0.8f, 0.8f);
#else
        this.targetScale = new Vector3(1.5f, 1.5f, 1.5f);
#endif
        this.isNextSceneMove = false;
        this.isReadyToNextSceneMove = false;


        uiObjs[0] = this.transform.GetChild(0).gameObject;
        uiObjs[1] = this.transform.GetChild(1).gameObject;

        uiObjs[0].SetActive(false);
        uiObjs[1].SetActive(false);

        
    }

    private void Update()
    {
        if (this.isReadyToNextSceneMove == true && this.isNextSceneMove == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                this.isNextSceneMove = true;
                InGameManager.Instance.GameEnd();
            }
        }
    }

    public void OutPutResult(bool isVictory_)
    {
        
        StartCoroutine(COutPutAnime(isVictory_));
    }

    private IEnumerator COutPutAnime(bool isVictory_)
    {
        GameObject uiTarget = null;


        if(isVictory_ == true)
        {
            AudioManager.Instance.PlaySFM(false, AudioManager.Instance.SFMClips[(int)ESoundSFM.Victory]);
            uiTarget = uiObjs[1];
        }
        else
        {
            AudioManager.Instance.PlaySFM(false, AudioManager.Instance.SFMClips[(int)ESoundSFM.Defeat]);
            uiTarget = uiObjs[0];
        }
        yield return new WaitForSeconds(5.5f);
        if (isVictory_ == true)
        {
            AudioManager.Instance.PlaySFM(false, AudioManager.Instance.SFMClips[(int)ESoundSFM.Victory_screen_start]);
        }
        else
        {
            AudioManager.Instance.PlaySFM(false, AudioManager.Instance.SFMClips[(int)ESoundSFM.Defeat_screen_start]);
        }

        uiTarget.SetActive(true);
        uiTarget.transform.localScale = Vector3.zero;

        float currentTime = 0f;
        float durationTime = 2f;
        float t = 0f;

        while(currentTime < durationTime)
        {
            currentTime += Time.deltaTime;
            t = currentTime / durationTime;
            uiTarget.transform.localScale = Vector3.Lerp(uiTarget.transform.localScale,targetScale,t);
            yield return null;
        }
        isReadyToNextSceneMove = true;
    }       // COutPutAnime()

}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class MacthingStartScrollController : MonoBehaviour
{       // ImageObjParent가 가지고 있는 컴포넌트
    private enum AllowDirection
    {
        Left = 0,
        Right = 1
    }


    private MatchingScroll_Image[] scrollImage = null;
    public GameObject scrollObj = null;
    private GameObject[] allowObjs = null;
    private float startYPos = default;
    private float endYPos = default;
    public Vector3 gameStartAnimeV3 = default;

    private float animeSpeed = default;

    private bool isScrollring = default;
    public bool IsScrollring
    {
        get
        {
            return this.isScrollring;
        }
        set
        {
            if (this.isScrollring != value)
            {
                this.isScrollring = value;

                if (this.isScrollring == true)
                {
                    StartCoroutine(MoveScroll());
                }
            }


        }
    }

    private Vector3 minusPos = default;

    private Quaternion maxUp = default;     // 화살의 최대 위쪽 방향
    private Quaternion maxDown = default;   // 화살의 최대 아랫쪽 방향



    private void Awake()
    {
        this.scrollObj = this.transform.GetChild(1).GetChild(0).gameObject;
        this.allowObjs = new GameObject[this.transform.GetChild(3).transform.childCount];
        this.allowObjs[(int)AllowDirection.Left] = this.transform.GetChild(3).GetChild(0).gameObject;
        this.allowObjs[(int)AllowDirection.Right] = this.transform.GetChild(3).GetChild(1).gameObject;

        this.maxUp = Quaternion.Euler(0, 0, 1);
        this.maxDown = Quaternion.Euler(0, 0, -1);

        this.animeSpeed = 13f;
        this.isScrollring = false;
        this.startYPos = 6.5f;
        this.endYPos = -3.5f;
        this.minusPos = new Vector3(0f, 2f, 0f);

        scrollImage = new MatchingScroll_Image[3];
        for(int i = 0; i < scrollImage.Length; i++)
        {
            //1.0.i
            scrollImage[i] = this.transform.GetChild(1).GetChild(0).GetChild(i).GetComponent<MatchingScroll_Image>();
        }

    }
    

    public void StartMatchingAnimation()
    {
        IsScrollring = true;
        StartCoroutine(UpAllow());
    }

    public void GameStartAnime()
    {       // 게임씬 넘어가기 직전에 애니메이션
        StopAllCoroutines();
        StartCoroutine(CGameStartAnime());
    }

    #region 애니메이션 관련
    private IEnumerator MoveScroll()
    {

        while (isScrollring == true)
        {            
            if (scrollObj.transform.position.y <= endYPos)
            {                
                scrollObj.transform.position = new Vector3(scrollObj.transform.position.x, startYPos, scrollObj.transform.position.z);
            }
            scrollObj.transform.position -= minusPos * Time.deltaTime * animeSpeed;
            yield return null;
        }

    }       // MoveScroll()

    private IEnumerator UpAllow()
    {
        float currentTime = 0;
        float lerpTime = 1f;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime * animeSpeed;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Quaternion newLeftDirection = Quaternion.Lerp(this.allowObjs[(int)AllowDirection.Left].transform.rotation, maxUp, t);
            Quaternion newRightDirection = Quaternion.Lerp(this.allowObjs[(int)AllowDirection.Right].transform.rotation, maxDown, t);
            // 새로운 위치 적용
            this.allowObjs[(int)AllowDirection.Left].transform.rotation = newLeftDirection;
            this.allowObjs[(int)AllowDirection.Right].transform.rotation = newRightDirection;

            yield return null;
        }
        StartCoroutine(DownAllow());
    }       // UpAllow()

    private IEnumerator DownAllow()
    {
        float currentTime = 0;
        float lerpTime = 0.2f;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Quaternion newLeftDirection = Quaternion.Lerp(this.allowObjs[(int)AllowDirection.Left].transform.rotation, maxDown, t);
            Quaternion newRightDirection = Quaternion.Lerp(this.allowObjs[(int)AllowDirection.Right].transform.rotation, maxUp, t);
            // 새로운 위치 적용
            this.allowObjs[(int)AllowDirection.Left].transform.rotation = newLeftDirection;
            this.allowObjs[(int)AllowDirection.Right].transform.rotation = newRightDirection;

            yield return null;
        }
        StartCoroutine(UpAllow());
    }       // DonwAllow()

    private IEnumerator CGameStartAnime()
    {
        // 여기서 이미지 바꾸고 텍스트 켜야함
        for(int i =0; i < scrollImage.Length; i++)
        {
            scrollImage[i].EndMatchingSetting();
        }

        float currentTime = 0;
        float lerpTime = 5f;
        Vector3 goalV3 = Vector3.zero;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            goalV3 = Vector3.Lerp(scrollObj.transform.position, gameStartAnimeV3, t);
            // 새로운 위치 적용
            scrollObj.transform.position = goalV3;            
            yield return null;
        }

        GameManager.Instance.GetTopParent(this.transform).GetComponent<LobbyPhoton>().InGameScene();
            
    }

    #endregion 애니메이션 관련

}   // ClassEnd

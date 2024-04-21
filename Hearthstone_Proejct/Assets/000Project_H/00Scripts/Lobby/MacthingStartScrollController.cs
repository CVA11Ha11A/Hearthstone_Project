using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;


public class MacthingStartScrollController : MonoBehaviour
{       // ImageObjParent가 가지고 있는 컴포넌트
    private enum AllowDirection
    {
        Left = 0,
        Right = 1
    }

    private GameObject scrollObj = null;
    private GameObject[] allowObjs = null;
    private float startYPos = default;
    private float endYPos = default;

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

    }
    

    public void StartMatchingAnimation()
    {
        IsScrollring = true;
        StartCoroutine(UpAllow());
    }

    public void StopAllCoroutine()
    {       // 외부에서 코루틴 멈추라고 명령할 수 있도록 제작
        StopAllCoroutines();
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
    #endregion 애니메이션 관련

}   // ClassEnd

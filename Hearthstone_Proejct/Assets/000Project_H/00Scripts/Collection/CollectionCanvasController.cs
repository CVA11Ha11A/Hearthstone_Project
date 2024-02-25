using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionCanvasController : MonoBehaviour
{
    // On : 수집품이 켜졌을때 도착할 포지션 , Off : 수집품이 켜지지 않은 상태일때 포지션
    private Vector3 onPosition = default;
    private Vector3 offPosition = default;

    RectTransform bookCover = default;

    private void Awake()
    {
        AwakeInIt();
    }       // Awake()

    private void Start()
    {
        LobbyManager.Instance.OpenCollectionEvent += CollectionOpen;
    }       // Start()


    // ---------------------------------------------------- CustomMethod ----------------------------------------------------------------

    private void AwakeInIt()
    {
        onPosition = this.transform.position;
        onPosition.x = 0f;
        offPosition = onPosition;
        offPosition.x = -35f;
        this.transform.position = offPosition;

        bookCover = this.gameObject.transform.GetChild(2).GetComponent<RectTransform>();

    }       // AwakeInIt()

    /// <summary>
    /// 수집품 오픈시 호출될 함수
    /// </summary>
    public void CollectionOpen()
    {
        StartCoroutine(SlidingCanvas());
    }       // CollectionOpen()


    /// <summary>
    /// 캔버스 포지션을 카메라 쪽으로 서서히 다가오게해주는 함수
    /// </summary>
    /// <returns></returns>
    IEnumerator SlidingCanvas()
    {
        float currentTime = 0;
        float lerpTime = 2.5f;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Vector3 newPosition = Vector3.Lerp(transform.position, onPosition, t);
            // 새로운 위치 적용
            transform.position = newPosition;

            if(transform.position == onPosition)
            {
                break;
            }

            yield return null;
        }

        StartCoroutine(OpenBookCover());    // 캔버스 도착시 책 커버 열기코루틴 호출

    }       // SlidingCanvas()

    IEnumerator OpenBookCover()
    {
        float currentTime = 0f;
        float lerpTime = 3f;

        Quaternion goalQuaternion = Quaternion.Euler(0f, 90f, 0f);
        Quaternion defaultQuaternion = Quaternion.Euler(0f, 0f, 0f);



        while (currentTime < lerpTime)
        {
            currentTime += Time.deltaTime;

            float t = currentTime / lerpTime;
            bookCover.transform.rotation = Quaternion.Slerp(defaultQuaternion, goalQuaternion, currentTime);            
            yield return null;
        }


    }       // OpenBookCover()

}       // CollectionCanvasController ClassEnd
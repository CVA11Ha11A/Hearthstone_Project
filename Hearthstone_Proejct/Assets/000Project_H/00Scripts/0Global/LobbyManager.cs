using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    /// <summary>
    /// 0 : 일반 하수인 1 : 주문
    /// </summary>
    public Image[] cardOutlines;
    /// <summary>
    /// 0 : 레어 1 : 에픽 2 : 전설
    /// </summary>
    public Material[] minionOutLines;

    #region LobbySceneCanvasRoots
    public LobbyCanvasController mainCanvasRoot = null;
    public CollectionCanvasController collectionCanvasRoot = null;
    public NewDeckCanvasTransformController newDeckCanvasRoot = null;
    #endregion LobbySceneCanvasRoots

    private static LobbyManager instance = default;
    public static LobbyManager Instance 
    {
        get 
        {
            if(instance == null || instance == default)
            {
                GameObject lobbyManager = new GameObject("LobbyManager");
                lobbyManager.AddComponent<LobbyManager>();                
            }
            return instance; 
        } 
    }
    

    // Collection의 열리는 기능(함수)이 구독할 이벤트
    public event Action OpenCollectionEvent;


    private void Awake()
    {        
        if(instance == null || instance == default)
        {
            instance = this;
        }
        else { /*PASS*/ }
    }       // Awake()



    public void OpenCollection()
    {        
        OpenCollectionEvent?.Invoke();
    }       // OpenCollection()


    #region 켄버스 오픈 오프 함수
    /// <summary>
    /// 이동할 개체, 목표 포지션 , 이동에 걸릴 시간 을 매개로 받으며 그에 맞게 이동
    /// </summary>
    public void CanvasOpen(Transform targetPos_,Vector3 arrivalPos_, float time)
    {        
        StartCoroutine(OpenCanvas(targetPos_,arrivalPos_,time));
    }       // CanvasOpen()

    public void CanvasClose(Transform targetPos_, Vector3 arrivalPos_, float time)
    {
     
        StartCoroutine(CloseCanvase(targetPos_, arrivalPos_, time));
    }

    private IEnumerator OpenCanvas(Transform targetPos_, Vector3 arrivalPos_, float time)
    {
        float currentTime = 0;
        float lerpTime = time;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Vector3 newPosition = Vector3.Lerp(targetPos_.position, arrivalPos_, t);
            // 새로운 위치 적용
            targetPos_.position = newPosition;

            if (targetPos_.position == arrivalPos_)
            {
                break;
            }

            yield return null;
        }    
    }       // OpenCanvas()

    private IEnumerator CloseCanvase(Transform targetPos_, Vector3 arrivalPos_, float time_)
    {
        float currentTime = 0;
        float lerpTime = time_;
        while (currentTime < lerpTime)
        {
            // 현재 시간 업데이트
            currentTime += Time.deltaTime;

            // 보간 비율 계산
            float t = currentTime / lerpTime;

            // Lerp 함수 사용하여 새로운 위치 계산
            Vector3 newPosition = Vector3.Lerp(targetPos_.position, arrivalPos_, t);
            // 새로운 위치 적용
            targetPos_.position = newPosition;

            if (targetPos_.position == arrivalPos_)
            {
                break;
            }

            yield return null;
        }
    }
    #endregion 컬렉션 오픈 오프 함수

    private void OnDestroy()
    {
        newDeckCanvasRoot = null;
        mainCanvasRoot = null;
        collectionCanvasRoot = null;
    }

}       // LobbyManager ClassEnd

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListComponent : MonoBehaviour
{       // 컬렉션 창에서 플레이어의 덱 리스트들을 관리해줄 컴포넌트
    GameObject[] decks = default;
    private void Awake()
    {
        GetDecksObj();
    }

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetDecksObj()
    {       // Calling To Awake Time
        int childCount = this.gameObject.transform.childCount;
        decks = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            decks[i] = this.transform.GetChild(i).gameObject;
        }
    }       // GetDecksObj()

    /// <summary>
    /// 덱의 갯수가 변동될때마다 실행해야됨
    /// </summary>
    private void CreateDeckButtonControll()
    {        
        for (int i = 0;i < decks.Length -1;i++)
        {
            if(decks[i].gameObject.activeSelf == false)
            {
                decks[decks.Length].gameObject.SetActive(true);
                return;
            }
        }

        decks[decks.Length].gameObject.SetActive(false);
    }       // CreateDeckButtonControll()

    public void CreateButtonOnClick()
    {
        // 덱 생성 버튼 클릭시 실행해야하는 것들
    }       // CreateButtonOnClick()



}       // DeckListComponent

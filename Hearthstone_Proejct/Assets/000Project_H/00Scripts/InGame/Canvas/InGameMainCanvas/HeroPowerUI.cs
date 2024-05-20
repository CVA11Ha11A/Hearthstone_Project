using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPowerUI : MonoBehaviour
{
    private SpriteRenderer heroPowerImage = null;   // 시작시 자신의 영웅의 맞는 이미지로 체인지 
    private GameObject costObj = null;              // 영능 사용후 돌아갈때 절반 돌아갔을때 꺼져야함
    private GameObject backGroundObj = null;        // 뒷면의 스프라이트의 Z축을 조정해서 앞으로 놔주어야함 (SortingLayer가 Front와 동일하기 떄문)

    private void Awake()
    {
        this.heroPowerImage = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        this.costObj = this.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
        backGroundObj = this.transform.GetChild(0).GetChild(1).gameObject;
    }

    private void Start()
    {
        // 덱을 참조해서 현재 Class를 가져오고 이미지 설정 // 자신이 누구인지 알아야함 적, 아군
        if(this.transform.CompareTag("My"))
        {
            SetHeroPowerSprite(InGameManager.Instance.InGameMyDeckRoot.DeckClass);
        }
        else if(this.transform.CompareTag("Enemy"))
        {           
            SetHeroPowerSprite(InGameManager.Instance.InGameEnemyDeckRoot.DeckClass);
        }
        else { DE.Log($"해당 개체의 Tag가 잘못됨"); }

    }

    private void SetHeroPowerSprite(ClassCard heroNum_)
    {
        this.heroPowerImage.sprite = ResourceManager.Instance.HeroPowerSprites[(int)heroNum_];
    }       // SetHeroPowerSprite()

}

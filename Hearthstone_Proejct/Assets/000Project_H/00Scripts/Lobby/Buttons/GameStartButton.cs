using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartButton : MonoBehaviour
{
    private void Awake()
    {
        this.transform.GetComponent<Button>().onClick.AddListener(GameStartButtonOnClick);
    }

    private void GameStartButtonOnClick()
    {
        // 게임찾는 이미지가 나오며 포톤에서 게임을 찾는 동작을 해야함
        GameManager.Instance.GetTopParent(this.transform).GetComponent<GameStartSelectDeckCanvas>().InvekeMatchingStart();
    }       // GameStartButtonOnClick()



}       // ClassEnd

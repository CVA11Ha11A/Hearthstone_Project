using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using System;
using System.Xml.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;


[System.Serializable]
public class Test001 : MonoBehaviour
{
    public GameObject[] gameObjects = null;


    private void Awake()
    {
        int lengths = this.transform.childCount;
        gameObjects = new GameObject[lengths-1];
        for (int i = 0; i < lengths-1; i++)
        {
            gameObjects[i] = this.transform.GetChild(i).gameObject;
        }


    }

    private void Start()
    {
        TS();
    }

    private void TS()
    {
        // 나눌떄 홀수는 -1 / .5는 버림
        // 홀수일때는 /2 -> +1 이 senter가 되면됨
        // 짝수는 그냥 /2 한 값이 center

        // 이동 가능한 거리 X -4.5 ~ +4.5
        // 한 카드의 가로 길이를 알아야 할 수도 있을듯
        bool allAngleChanger = false; // 모든 카드들을 기울일것인지 
        float afterSenterAngle = -60f / gameObjects.Length;
        float previousSenterAngle = 60f / gameObjects.Length; ;
        float angle = default;
        int senterIndex = -1;

        // 조건에서 Lenth대신 CurrentHandCount 가 들어가야함        // 현재 손패 갯수 기준으로 잡아야함
        DE.Log($"Length : {gameObjects.Length}\n MOd : {gameObjects.Length % 2}");
        if (gameObjects.Length % 2 == 0)
        {

            senterIndex = (gameObjects.Length / 2) - 1;
            allAngleChanger = true;
        }
        else
        {
            senterIndex = (gameObjects.Length / 2);
            allAngleChanger = false;
        }


        int nowCardCount = gameObjects.Length;
        for (int i = 0; i < senterIndex; i++)
        {
            angle = previousSenterAngle * nowCardCount;
            nowCardCount--;
            gameObjects[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (allAngleChanger == true)
        {
            angle = previousSenterAngle * nowCardCount;
            gameObjects[senterIndex].transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            gameObjects[senterIndex].transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        for (int i = senterIndex + 1; i < gameObjects.Length; i++)
        {
            angle = afterSenterAngle * i;
            gameObjects[i].transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }


}








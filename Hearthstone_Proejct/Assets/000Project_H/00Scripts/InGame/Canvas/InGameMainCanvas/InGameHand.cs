using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHand : MonoBehaviour
{       // 핸드에 들어온 카드 옵젝트들을 관리할 것임

    public List<GameObject> handCard = null;
    private int maxHandCount = 10;
    private float cardMaxYPos = 40f;        // 카드의 최대 Y 높이
    private float cardOverlapXPosDis = 40f;        // 카드가 곂칠 경우 X position 간격
    private float cardOneSizeXPos = 100f;
    private float senterX = default;        // X는 Index에 따라 Dis를 이용해서 계산
    private float senterY = default;        // Y는 카드 갯수에 따라서 변동         // Z 는 Index * -10 으로

    private bool isEnemyHand = false;
    private bool isMyHand = false;



    public int MaxHandCount
    {
        get
        {
            return this.maxHandCount;
        }
    }
    private int nowHandCount = default;
    public int NowHandCount
    {
        get
        {
            return this.nowHandCount;
        }
        set
        {
            this.nowHandCount = value;


            CardHandSorting();
            DisplayHandCards();
        }
    }


    private void Awake()
    {
        this.handCard = new List<GameObject>(12);
        this.maxHandCount = 10;
        this.cardMaxYPos = 40f;
        this.senterX = 0f;
        this.senterY = 0f;
        this.cardOneSizeXPos = 100f;

        if (this.gameObject.name == "MyHand")
        {
            this.transform.parent.GetComponent<InGameHands>().SetterMyHand(this);
            isMyHand = true;
        }
        else if (this.gameObject.name == "EnemyHand")
        {
            this.transform.parent.GetComponent<InGameHands>().SetterEnemyHand(this);
            isEnemyHand = true;
        }

    }



    public void SetterMaxHandCount(int newMaxHandCount_)
    {   // 최대 가드 소지 갯수를 늘려주는 함수
        this.maxHandCount = newMaxHandCount_;
    }

    public void DisplayHandCards()
    {   // 핸드의 X Y 축을 조정 하는 기능
        //DE.Log($"핸드 카드 포지션 조정 함수 진입");
        int nowHandCount = handCard.Count;
        int senterIndex = nowHandCount / 2;

        if (this.isEnemyHand == true)
        {
            for (int i = 0; i < handCard.Count; i++)
            {
                handCard[i].transform.rotation = Quaternion.Euler(180f, 0, 0);
            }
        }

        if (senterIndex == 0 && handCard.Count == 1)
        {   // 카드가 한개일 경우         
            handCard[0].transform.localPosition = Vector3.zero;
            return;
        }
        else if (handCard.Count == 0)
        {   // 카드가 존재하지 않을경우 return
            return;
        }
        else if (handCard.Count == 2)
        {
            handCard[0].transform.localPosition = new Vector3(senterX - cardOneSizeXPos, senterY, 0f);
            handCard[1].transform.localPosition = new Vector3(senterX, senterY, 1 * -10f);
            return;
        }
        else if (handCard.Count == 3)
        {
            handCard[0].transform.localPosition = new Vector3(senterX - cardOneSizeXPos, senterY, 0f);
            handCard[1].transform.localPosition = new Vector3(senterX, senterY, 1 * -10f);
            handCard[2].transform.localPosition = new Vector3(senterX + cardOneSizeXPos, senterY, 2 * -10f);
            return;
        }
        else
        {       // 카드가 4개 이상일 경우
            if (senterIndex >= 4)
            {
                handCard[senterIndex].transform.localPosition = new Vector3(0f, cardMaxYPos, -10 * senterIndex);
            }
            else if (senterIndex >= 3)
            {
                handCard[senterIndex].transform.localPosition = new Vector3(0f, cardMaxYPos - 10f, -10 * senterIndex);
            }
            else if (senterIndex >= 2)
            {
                handCard[senterIndex].transform.localPosition = new Vector3(0f, cardMaxYPos - 20f, -10 * senterIndex);
            }
        }

        int leftCardCoopCount = 1;
        // 중앙기준 좌측
        for (int i = senterIndex - 1; 0 <= i; i--)
        {
            handCard[i].transform.localPosition =
                new Vector3(leftCardCoopCount * -cardOverlapXPosDis, handCard[i + 1].transform.localPosition.y - 10, -10 * i);
            leftCardCoopCount++;
        }       // for : 중앙기준 좌측

        int rightCardLoopCount = 1;
        for (int i = senterIndex + 1; i < handCard.Count; i++)
        {
            handCard[i].transform.localPosition =
                new Vector3(rightCardLoopCount * cardOverlapXPosDis, handCard[i - 1].transform.localPosition.y - 10, -10 * i);
            rightCardLoopCount++;
        }


    }       // DisplayHandCards()
    public void CardHandSorting()
    {
        //DE.Log($"핸드 부채모양 함수 진입");
        // 나눌떄 홀수는 -1 / .5는 버림
        // 홀수일때는 /2 -> +1 이 senter가 되면됨
        // 짝수는 그냥 /2 한 값이 center

        // 이동 가능한 거리 X -4.5 ~ +4.5
        // 한 카드의 가로 길이를 알아야 할 수도 있을듯

        // 해당함수도 조건을 따지지 말고 호출하고 여기서 3보다 작으면 다 000으로 설정하는 기능을 추가
        if (handCard.Count <= 3)
        {
            foreach (GameObject objRoot in this.handCard)
            {
                if(objRoot == null)
                {
                    continue;
                }
                objRoot.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            return;
        }
        else { /*PASS*/ }

        bool allAngleChanger = false; // 모든 카드들을 기울일것인지 
        float afterSenterAngle = -20f / handCard.Count;
        float previousSenterAngle = 20f / handCard.Count;
        float angle = default;
        int senterIndex = -1;
        float xAngle = 0f;
        if (isEnemyHand == true)
        {
            xAngle = 180f;
        }

        // 조건에서 Lenth대신 CurrentHandCount 가 들어가야함        // 현재 손패 갯수 기준으로 잡아야함

        if (handCard.Count % 2 == 0)
        {

            senterIndex = (handCard.Count / 2) - 1;
            allAngleChanger = true;
        }
        else
        {
            senterIndex = (handCard.Count / 2);
            allAngleChanger = false;
        }


        int nowCardCount = handCard.Count;
        for (int i = 0; i < senterIndex; i++)
        {
            angle = previousSenterAngle * nowCardCount;
            nowCardCount--;
            handCard[i].transform.rotation = Quaternion.Euler(xAngle, 0, angle);
        }

        if (allAngleChanger == true)
        {
            angle = previousSenterAngle * nowCardCount;
            handCard[senterIndex].transform.rotation = Quaternion.Euler(xAngle, 0, angle);
        }
        else
        {
            handCard[senterIndex].transform.rotation = Quaternion.Euler(xAngle, 0, 0);

        }

        for (int i = senterIndex + 1; i < handCard.Count; i++)
        {
            angle = afterSenterAngle * i;
            handCard[i].transform.rotation = Quaternion.Euler(xAngle, 0, angle);
        }

    }



    public void AddCardInHand(GameObject targetObj_)
    {
        targetObj_.transform.SetParent(this.transform.GetChild(0).transform);
        handCard.Add(targetObj_);
        NowHandCount++;
    }

    public void RemoveCardInHand(GameObject targetObj_)
    {   // 카드를 지우거나 카드를 내거나 핸드가 소모되었을때 호출되야하는 함수


        for (int i = 0; i < handCard.Count; i++)
        {
            if (handCard[i].Equals(targetObj_))
            {
                //DE.Log($"카드 낸 이후 카드 제거 성공");
                handCard.RemoveAt(i);
                NowHandCount--;
                break;
            }

        }
        //handCard.Remove(targetObj_);
        //NowHandCount--;

    }


    private void DisPlayHandCardLEGACY()
    {
        #region LEGACY TRY1
        //#region 기울지 않을 경우
        //if (NowHandCount == 0)
        //{   // 손이 원형 모양으로 되지 않아도 될때
        //    return;
        //}
        //else if (NowHandCount == 1)
        //{
        //    handCard[0].transform.position = this.senterCardPos;
        //    return;
        //}
        //else if (NowHandCount == 2)
        //{
        //    handCard[0].transform.position = new Vector3(-100f, 0f, 0f);
        //    handCard[1].transform.position = this.senterCardPos;
        //    return;
        //}
        //else if (NowHandCount == 3)
        //{
        //    handCard[0].transform.position = new Vector3(-100f, 0f, 0f);
        //    handCard[1].transform.position = this.senterCardPos;
        //    handCard[2].transform.position = new Vector3(100f, 0f, 0f);
        //    return;
        //}
        //#endregion 기울지 않을 경우

        //#region 기울경우

        //여기부터는 카드가 4장 이상일 경우 y축 z도 조절 해야함
        //홀수 짝수 구별해야함
        //bool isEven = NowHandCount % 2 == 0;        // 여기서 현재 핸드 수가 짝수인지 구별 짝수면 true
        //int senterIndex = NowHandCount / 2;
        //Vector3 senterV3 = default;

        //if (NowHandCount >= 9)
        //{
        //    senterV3 = new Vector3(0f, cardMaxYPos, -senterIndex * 10f);
        //}
        //else if (NowHandCount >= 7)
        //{
        //    senterV3 = new Vector3(0f, 30f, -senterIndex * 10f);
        //}
        //else if (NowHandCount >= 5)
        //{
        //    senterV3 = new Vector3(0f, 20f, -senterIndex * 10f);
        //}
        //else
        //{
        //    senterV3 = new Vector3(0f, 10f, -senterIndex * 10f);
        //}

        //if (isEven == true)
        //{       // 짝수 
        //    int loopCount = 1;
        //    3가지 루트
        //    int cardSizeSetter = 1;     // 카드 size *연산에 사용될 것
        //    handCard[senterIndex].transform.position =
        //    for (int i = senterIndex - 1; i > 0; i--)
        //    {
        //        {
        //            예외처리
        //        if (i - 1 < 0)
        //            {
        //                break;
        //            }
        //            else if (i >= handCard.Count)
        //            {
        //                break;
        //            }
        //        }
        //        예외처리

        //        if (handCard.Count == i || 2 * loopCount >= handCard.Count)
        //        {
        //            DE.LogError($"잘못된 접근\n조건1 : {handCard.Count == i}\n조건2 : {2 * loopCount >= handCard.Count}");
        //        }
        //        handCard[i].transform.position = new Vector3(cardSizeSetter * -cardOverlapXPosDis, senterV3.y - (10 * loopCount), i * -10);
        //        handCard[2 * loopCount].transform.position =
        //            new Vector3(cardSizeSetter * cardOverlapXPosDis, senterV3.y - (10 * loopCount), i * -10);
        //    }
        //}
        //else
        //{       // 홀수
        //    for (int i = senterIndex + 1; i > 0; i--)
        //    {
        //        {
        //            예외처리
        //        if (i - 1 < 0)
        //            {
        //                break;
        //            }
        //            else if (i >= handCard.Count)
        //            {
        //                break;
        //            }
        //        }
        //        예외처리


        //       handCard[i].transform.position =
        //    }
        //}

        //짝수 홀수 두개 차이가 존재하나 ?
        //1 senter를 지정후 for문을 순회하며
        //senter를 WkrtndlfEo + 1해주면 - 1 인덱스에 접근할 가능성이 없지 않을까? 해당 그렇다면 해당NullReferens만 예방하면 됨



        //#endregion 기울경우

        #endregion LEGACY TRY1
    }
}       // ClassEnd

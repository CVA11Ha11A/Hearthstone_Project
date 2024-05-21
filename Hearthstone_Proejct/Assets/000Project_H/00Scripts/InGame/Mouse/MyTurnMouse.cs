using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyTurnMouse : MonoBehaviour
{   // 나의 턴에만 작동될 상호작용 마우스

    private Mouse mouseRoot = null;
    private int targetLayer = default;

    // 기능 관련 bool 변수
    private bool isDragToReady = false;
    private bool isCardScaleSet = false;
    private bool isMinion = false;
    private bool isSpell = false;


    private Vector3 mouseScreenPosition = default;
    private Vector3 mouseWorldPosition = default;

    // 핸드 카드 변수
    public GameObject targetCard = null;
    private GameObject scaleSetObjTarget = null;    // 스케일 조정중 targetCard가 Null이 될경우를 위한 Root
    private Vector3 setScale = default;

    // 미니언 드래그 관련 변수
    private GameObject selectMinion = null;     // 선택된 대상
    private GameObject targetObj = null;        // 공격할 타겟
    private LayerMask attackTargetLayer = default;  // 공격가능한 타겟이 될 레이어
    private LayerMask heroPowerLayer = default;
    private RaycastHit hitInfo = default;

    private void Awake()
    {
        this.targetLayer = 1 << 6;      // Card
        this.isDragToReady = false;
        this.isCardScaleSet = false;
        this.isMinion = false;
        this.isSpell = false;
        this.setScale = Vector3.one;
        this.mouseRoot = this.transform.GetComponent<Mouse>();
        this.attackTargetLayer = 1 << 12 | 1 << 13;     // Minion | Hero
        this.heroPowerLayer = 1 << 14;

        this.enabled = false;
    }
    void Start()
    {

    }

    private void Update()
    {
#if DEVELOP_TEST
        // 마우스 포지션을 얻어옴
        mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));
        if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo))
        {
            //DE.Log($"테스트 레이 맞은 개체 : {hitInfo.collider.name}");
        }
#endif

        if (Input.GetMouseButtonDown(0))
        {   // 클릭 순간
            //DE.Log($"클릭하는 순간 heroPower == Null? : {mouseRoot.myHeroPowerRoot == null}");
            if (mouseRoot.lastCardRoot != null)
            {
                this.isDragToReady = true;
                this.mouseRoot.isDraging = true;
                //DE.Log("클릭해서 드래그를 true로 바꿈");
                this.targetCard = mouseRoot.lastCardRoot.gameObject;
                this.scaleSetObjTarget = this.targetCard;
            }

            else if (mouseRoot.lastMinionRoot != null)
            {
                this.isDragToReady = true;
                mouseRoot.isDraging = true;
                this.selectMinion = mouseRoot.lastMinionRoot.gameObject;
                AudioManager.Instance.PlaySFM(false, selectMinion.GetComponent<Minion>().GetAttackClip());
            }

            else if (mouseRoot.myHeroPowerRoot != null)
            {
                this.isDragToReady = true;
                mouseRoot.isDraging = true;
            }
        }

        if (this.isDragToReady == true && this.targetCard != null)
        {   // 드래그
            if (isCardScaleSet == false)
            {
                StartCoroutine(CTargetCardSclaeSet());
            }
            mouseScreenPosition = Input.mousePosition;
            // 마우스 스크린 좌표를 월드 좌표로 변환
            float distance = Mathf.Abs(targetCard.transform.position.z - Camera.main.transform.position.z);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                distance));

            targetCard.transform.position = mouseWorldPosition;
        }

        else if (this.isDragToReady == true && this.selectMinion != null)
        {
            // 마우스 포지션을 얻어옴
            mouseScreenPosition = Input.mousePosition;
            // 마우스 스크린 좌표를 월드 좌표로 변환
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                Camera.main.nearClipPlane));

            // 포물선 그리기 목표는 마우스 현재 위치
            InGameManager.Instance.frontCanvas.drawRoot.DrawParabola(selectMinion.transform.position, mouseWorldPosition);
        }

        else if (this.isDragToReady == true && mouseRoot.myHeroPowerRoot != null)
        {
            if (mouseRoot.myHeroPowerRoot.isNeedTarget == true)
            {
                // 마우스 포지션을 얻어옴
                mouseScreenPosition = Input.mousePosition;
                // 마우스 스크린 좌표를 월드 좌표로 변환
                mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                    Camera.main.nearClipPlane));
                
                // 포물선 그리기 목표는 마우스 현재 위치
                InGameManager.Instance.frontCanvas.drawRoot.DrawParabola(mouseRoot.myHeroPowerRoot.transform.position, mouseWorldPosition);
            }
            else { /*PASS*/ }
        }


        if (Input.GetMouseButtonUp(0))
        {   // 마우스 땔경우

            // 마우스 포지션을 얻어옴
            mouseScreenPosition = Input.mousePosition;
            // 마우스 스크린 좌표를 월드 좌표로 변환
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
                Camera.main.nearClipPlane));

            if (this.targetCard != null)
            {   // 현재 핸드의 카드를 드래그 중이라면

                // 여기서 레이를 쏘고 필드의 타겟인지 체크
                if (targetCard.transform.position.y > -2f)
                {   // 필드쪽으로 갔다는 뜻
                    // 1. 내코스트가 카드를 사용할 만큼의 코스트가 되는지 확인
                    if(CheckIsThrowCard() == true)
                    {
                        // Pass
                    }
                    else
                    {
                        mouseRoot.LastCardPositionRollBack();
                    }
                }
                else
                {
                    mouseRoot.LastCardPositionRollBack();
                }
            }

            // 레이를 쏘아서 공격가능한 Layer라면 공격을 실행해야함

            if (this.selectMinion != null)
            {   // 현재 공격시도를 하는 미니언이 존재할 경우
                if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, attackTargetLayer))
                {
                    //DE.Log($"현재 레이에 맞은 목표 타겟 : {hitInfo.collider.name}");
                    // 미니언이라면 card를 가져와서 데미지 계산 | 영웅이라면 영웅 공격
                    if (hitInfo.collider.GetComponent<IDamageable>() != null)
                    {
                        // 공격가능한지 조건 체크 함수 실행 : 아군공격하는것인지? , 도발하수인 존재하는지?등 체크
                        if (InGameManager.Instance.mainCanvasRoot.fieldRoot.EnemyField.IsTargetAtteckAble(hitInfo.transform))
                        {
                            // Attack실행
                            StartCoroutine(this.selectMinion.GetComponent<Minion>().CIAttackAnime(hitInfo.transform));

                        }
                    }
                }
            }

            if (mouseRoot.myHeroPowerRoot != null)
            {
                if (mouseRoot.myHeroPowerRoot.isNeedTarget == true)
                {   // 타겟이 필요하면 레이를 쏴야함
                    if (Physics.Raycast(mouseWorldPosition, Vector3.forward, out hitInfo, Mathf.Infinity, attackTargetLayer))
                    {
                        mouseRoot.myHeroPowerRoot.TargetHeroPowerEmpect(hitInfo.transform);
                    }
                }
                else
                {
                    mouseRoot.myHeroPowerRoot.NonTargetHeroPowerEmpect();
                }
            }

            InGameManager.Instance.frontCanvas.drawRoot.EndParabola();  // 그려진 LineRenderer를 숨기기
            this.isDragToReady = false;
            this.isCardScaleSet = false;
            mouseRoot.isDraging = false;
            mouseRoot.lastMinionRoot = null;
            mouseRoot.myHeroPowerRoot = null;
            this.targetCard = null;
            this.selectMinion = null;

        }       // if : MouseGetUp(0)

    }       // Update()


    IEnumerator CTargetCardSclaeSet()
    {
        this.isCardScaleSet = true;
        float currentTime = default;
        float targetTime = 1f;
        float t = default;
        Vector3 originScale = scaleSetObjTarget.transform.localScale;
        while (targetTime > currentTime)
        {
            if (targetCard != null)
            {
                t = targetTime / currentTime;
                scaleSetObjTarget.transform.localScale = Vector3.Lerp(originScale, setScale, t);
                currentTime += Time.deltaTime;
                yield return null;
            }
            else
            {   // targetCard == null
                scaleSetObjTarget.transform.localScale = Vector3.one;
                break;
            }

        }

    }       // CTargetCardSclaeSet()

    public bool CheckIsThrowCard()
    {        
        // 코스트 조건 확인
        if (targetCard.GetComponent<Card>().cost <= InGameManager.Instance.mainCanvasRoot.costRoot.MyCost.NowCost)
        {
            //PASS
        }
        else
        {
            return false;
        }

        // 어떤 카드인지 체크
        if (targetCard.GetComponent<Card>() is Minion)
        {
            isMinion = true;
            isSpell = false;
        }
        else
        {
            isSpell = true;
            isMinion = false;
        }

        // 3 미니언이라면 필드의 수가 가득 찼는지 체크
        if (isMinion == true)
        {
            if (InGameManager.Instance.mainCanvasRoot.fieldRoot.MyField.NowMinionCount >= InGameField.MAX_MINON_COUNT)
            {
                return false;
            }
            else
            {
                // 여기서 target카드가 핸드의 몇번째 카드인지 알아내고 쏴주어야 할거같음
                int cardHandIndex = default;
                for (int i = 0; i < InGameManager.Instance.mainCanvasRoot.handRoot.MyHand.transform.GetChild(0).childCount; i++)
                {   // 핸드의 몇번째 존재하는 카드를 내려고 시도하는지 찾는 for문
                    if (targetCard.Equals(InGameManager.Instance.mainCanvasRoot.handRoot.MyHand.transform.GetChild(0).GetChild(i).gameObject))
                    {   // 현재 들고있는 카드와 같은 개체라면 들어올 if문
                        cardHandIndex = i;
                        break;
                    }
                }


                targetCard.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                InGameManager.Instance.mainCanvasRoot.handRoot.MyHand.RemoveCardInHand(targetCard);
                InGameManager.Instance.mainCanvasRoot.fieldRoot.MyField.SpawnMinion();
                targetCard.GetComponent<Card>().MinionFieldSpawn(InGameManager.Instance.mainCanvasRoot.fieldRoot.MyField.RecentFieldObjRoot);
                InGameManager.Instance.mainCanvasRoot.costRoot.MyCost.NowCost -= targetCard.GetComponent<Card>().cost;
                InGameManager.Instance.CostSync(InGameManager.Instance.mainCanvasRoot.costRoot.MyCost.NowCost);
                InGameManager.Instance.ThrowMinionSync(cardHandIndex);
            }


            return true;
        }
        return false;    // temp 
    }       // CheckIsThrowCard()


}       // ClassEnd

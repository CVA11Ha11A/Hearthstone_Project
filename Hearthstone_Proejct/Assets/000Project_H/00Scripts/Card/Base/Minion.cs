using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Minion : Card
{
    protected M_Ability ability = default;
    protected int damageDefault = default;
    protected int heathDefault = default;
    protected bool isSpawnEmpect = false;
    public int damage = default;
    public int heath = default;


    public Minion()
    {
        SetCardType(CardType.Minion);
        clips = new AudioClip[Enum.GetValues(typeof(M_Clip)).Length];
    }


    protected override void Awake()
    {
        base.Awake();
        GetCardComponents();

    }       // Awake()

    protected override void Start()
    {
        base.Start();
        GetAudioClip();
        UpdateUI();
        NameTextFix();
    }       // Start()


    /// <summary>
    /// 이것도 AddComponent된 이후에 호출 당해야함
    /// </summary>
    protected override void UpdateUI()
    {
        base.UpdateUI();
        cardTexts[(int)C_Text.Hp].text = heath.ToString();
        cardTexts[(int)C_Text.Damage].text = damage.ToString();
    }

    protected void StatSetting(int cost_, int damage_, int hp_)
    {
        this.damageDefault = damage_;
        this.damage = this.damageDefault;
        this.heathDefault = hp_;
        this.heath = heathDefault;
        this.cost = cost_;
    }       // StatSetting()

    protected override void GetAudioClip()
    {   // 오디오 소스를 가져오는 함수        
        string defaultAudioPath = "Audios/";
        string defaultName = "MinionDefault";
        sb.Clear().Append(this.cardNameEn + "_Play");
        if (clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            sb.Clear().Append(defaultName).Append("_Play");
            clips[(int)M_Clip.Play] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }

        sb.Clear().Append(this.cardNameEn + "_Attack");
        if (clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            sb.Clear().Append(defaultName).Append("_Attack");
            clips[(int)M_Clip.Attack] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }

        sb.Clear().Append(this.cardNameEn + "_Death");
        if (clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb))
        {
            clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }
        else
        {
            sb.Clear().Append(defaultName).Append("_Death");
            clips[(int)M_Clip.Death] = Resources.Load<AudioClip>(defaultAudioPath + sb);
        }


        sb.Clear();
        if (this.cardRank == CardRank.M_Legendry)
        {
            sb.Append(this.cardNameEn + "_Stinger");
            if (clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb))
            {
                clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb);
            }
            else
            {
                sb.Clear().Append(defaultName).Append("_Stinger");
                clips[(int)M_Clip.Stinger] = Resources.Load<AudioClip>(defaultAudioPath + sb);
            }
        }
        else { /*PASS*/ }
    }       // GetAudioClip()

    protected override void GetCardComponents()
    {
        base.GetCardComponents();
        cardTexts[(int)C_Text.Hp].gameObject.SetActive(true);
        cardTexts[(int)C_Text.Damage].gameObject.SetActive(true);
    }       // GetCardComponents()

    public override void NameTextFix()
    {
        Vector3 v3 = this.cardTexts[(int)C_Text.Name].transform.localPosition;
        v3.z = -0.1f;
        this.cardTexts[(int)C_Text.Name].transform.localPosition = v3;
        this.cardTexts[(int)C_Text.Name].transform.localRotation = Quaternion.Euler(0f, 0f, 6f);
    }

    public override void MinionFieldSpawn(GameObject spawnParentObj_)
    {
        this.gameObject.layer = LayerMask.NameToLayer("Minion");
        // 하수인이 소환될 경우 실행되어야 하는것
        // 1. 필드에 이동
        if (isSpawnEmpect == false)
        {
            StartCoroutine(CSpawnMove(spawnParentObj_));
        }
        else
        {
            // TODO : 소환 모션 실행 함수 호출해야함
        }

    }       // MinionSpawn()

    IEnumerator CSpawnMove(GameObject spawnParentObj_)
    {
        

        // 포지션 파트
        float durationTime = 2f;
        float currentTime = default;
        float t = default;
        Vector3 moveV3 = default;

        while (durationTime > currentTime)
        {
            currentTime += Time.deltaTime;
            t = currentTime / durationTime;

            if (Vector3.Distance(this.transform.position, spawnParentObj_.transform.position) < 3f)
            {
                break;      // 연산이 좀 무거울듯 좋은 방법이 생각나면 수정할 것
            }
            moveV3 = Vector3.Lerp(this.transform.position, spawnParentObj_.transform.position, t);
            this.transform.position = moveV3;
            //DE.Log($"t : {currentTime}");
            yield return null;
        }
        this.transform.parent = spawnParentObj_.transform;
        this.transform.localPosition = Vector3.zero;

        this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        // 2. 카드 -> 필드 미니언으로 변경
        ingameUiObj.SetActive(true);    // UI 켜기        
        textCanvas.textObjRoots[0].transform.position = ingameUiObj.transform.GetChild(0).transform.position;
        textCanvas.textObjRoots[1].transform.position = ingameUiObj.transform.GetChild(1).transform.position;
        Vector3 v3 = new Vector3(textCanvas.textObjRoots[1].transform.position.x,
            textCanvas.textObjRoots[0].transform.position.y, textCanvas.textObjRoots[1].transform.position.z);
        textCanvas.textObjRoots[1].transform.position = v3;

        textCanvas.GetComponent<CardTextCanvas>().SetMinionFieldTextPos();  // 불필요한 Text끄기

        // 마테리얼 변경
        this.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material =
            CardManager.Instance.cardOutLineMaterials[(int)C_Material.M_InGameLine];

        // 카드 대사 출력
        AudioManager.Instance.PlaySFM(false, clips[(int)M_Clip.Play]);
        if (this.cardRank == CardRank.M_Legendry)
        {
            AudioManager.Instance.PlaySFM(false, clips[(int)M_Clip.Stinger]);
        }

        // 

    }       // CSpawnMove()


}       // Minion ClassEnd

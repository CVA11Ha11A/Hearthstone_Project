using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EEmoteClip
{
    Start = 0,
    Hello = 1,
    WOW = 2,
    Thanks = 3,
    Oops = 4,
    Concede = 5,
    Death = 6
}
public class HeroImage : MonoBehaviour, IDamageable
{
    private int maxHeroHp = 30;
    private int heroHp = 30;
    public int HeroHp
    {
        get
        {
            return this.heroHp;
        }
        set
        {
            heroHp = value;
            if (heroHp > maxHeroHp)
            {
                heroHp = maxHeroHp;
            }
            else if (heroHp <= 0)
            {
                // TODO : 영웅 사망
                HeroDeath();
            }
            HeroHPTextUpdate();

        }
    }


    public bool isSettingCompleate = default;
    public Image heroImage = null;
    public TextMeshProUGUI hpText = null;

    private bool isMine = false;
    private bool isEnemy = false;
    private GameObject hpImageObject = null;
    private AudioClip[] emoteClip = null;
    public AudioClip[] EmoteClip
    {
        get
        {
            return this.emoteClip;
        }
    }
    private StringBuilder sb = null;

    private void Awake()
    {
        isSettingCompleate = false;
        sb = new StringBuilder();
        emoteClip = new AudioClip[7];
        this.maxHeroHp = 30;
        this.heroHp = 30;


        heroImage = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = this.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        hpImageObject = this.transform.GetChild(1).gameObject;

        if (this.transform.name == "MyImage(Frame)")
        {
            isMine = true;
            isEnemy = false;
            this.transform.parent.GetComponent<HeroImages>().MyHeroImageRootSetter(this);
        }
        else if (this.transform.name == "EnemyImage(Frame)")
        {
            isMine = false;
            isEnemy = true;
            this.transform.parent.GetComponent<HeroImages>().EnemyHeroImageRootSetter(this);
        }

        hpImageObject.SetActive(false);
    }

    public void HPImageOn()
    {   // 처음에 영웅 인트로 들어간뒤에 켜져야함
        hpImageObject.SetActive(true);

    }

    public void HeroSetting()
    {

        string defaultPath = "ClassEmoteClips/VO_HERO_";
        string heroNum = string.Empty;
        if (this.isMine == true)
        {
            heroNum = ResourceManager.Instance.GetHeroNum(GameManager.Instance.inGamePlayersDeck.MyDeck.deckClass);
        }
        else
        {
            heroNum = ResourceManager.Instance.GetHeroNum(GameManager.Instance.inGamePlayersDeck.EnemyDeck.deckClass);
        }
        int conversIndex = 0;

        for (int i = 0; i < emoteClip.Length; i++)
        {
            sb.Clear();
            sb.Append(defaultPath).Append(heroNum).Append("_").Append((EEmoteClip)conversIndex);
            emoteClip[i] = Resources.Load<AudioClip>(sb.ToString());
            conversIndex++;
        }

        isSettingCompleate = true;
    }       // HeroSetting()

    public void IAttacked(int damage_)
    {

        this.HeroHp -= damage_;

    }

    public void HeroHPTextUpdate()
    {
        if (sb == null)
        {
            sb = new StringBuilder();
        }
        sb.Clear();
        sb.Append($"{HeroHp}");
        hpText.text = sb.ToString();
    }       // HeroHPTextUpdate()

    public void HeroDeath()
    {
        // 1. 영웅 비명 지르기
        StartCoroutine(CHeroDeathShake());
        StartCoroutine(CHeroImageColorSet());
    }
    private IEnumerator CHeroDeathShake()
    {
        // 1 흔들리기
        float shakeDuration = 0.5f; // 흔들림 지속 시간
        float shakeMagnitude = 0.1f; // 흔들림 강도
        float currentTime = 0f;

        Vector3 OriginV3 = this.transform.localPosition;

        while (currentTime < shakeDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

            this.transform.localPosition = new Vector3(x, y, OriginV3.z);

            currentTime += Time.deltaTime;

            yield return null;
        }       // while(ShakeMove)        
    }       // CHeroDeathShake()
    private IEnumerator CHeroImageColorSet()
    {
        AudioManager.Instance.PlaySFM(false, this.EmoteClip[(int)EEmoteClip.Death]);

        Color32 deathColor = new Color32(145, 145, 145, 255);
        Image heroImageRoot = heroImage;
        float currentTime = 0f;
        float setTime = 1f;

        while (currentTime < setTime)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / setTime;
            heroImageRoot.color = Color32.Lerp(heroImageRoot.color, deathColor, t);
            yield return null;
        }        
    }


    public IEnumerator CIAttackAnime(Transform targetTrans_, bool isRPC = false)
    {   // 영웅의 공격은 아직 구현 X 
        yield return null;
    }
}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrontGroundCanvas : MonoBehaviour
{       // 맨앞 이미지이며 해당 켄버스로 페이드인 페이드 아웃을 표현

    private Image fadeImage = null;
    private float maxTime = 5f;

    public DrawLine drawRoot = null;
    public GameEndUI gameEndUiRoot = null;

    private void Awake()
    {        
        fadeImage = this.transform.GetChild(0).GetComponent<Image>();
        this.fadeImage.color = Color.black;
    }
    private void Start()
    {
        InGameManager.Instance.frontCanvas = this;
        //FadeIn();
    }

    public void FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(CFadeIn());
    }

    public void FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        StartCoroutine(CFadeOut());
    }

    private IEnumerator CFadeIn()
    {
        float currentTime = 0f;
        Color32 goalColor = new Color32(0, 0, 0, 0);
        Color32 tempColor = default;
        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime / maxTime;
            tempColor = Color32.Lerp(fadeImage.color, goalColor, currentTime);
            fadeImage.color = tempColor;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator CFadeOut()
    {
        float currentTime = 0f;
        Color32 goalColor = new Color32(0, 0, 0, 255);
        Color32 tempColor = default;
        while (currentTime < maxTime)
        {
            currentTime += Time.deltaTime / maxTime;
            tempColor = Color32.Lerp(fadeImage.color, goalColor, currentTime);
            fadeImage.color = tempColor;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);

    }

}       // ClassEnd

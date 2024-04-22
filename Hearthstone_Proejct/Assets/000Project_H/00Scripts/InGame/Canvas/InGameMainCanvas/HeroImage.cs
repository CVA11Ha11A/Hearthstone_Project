using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroImage : MonoBehaviour
{
    public Image heroImage = null;
    public TextMeshProUGUI hpText = null;
    private GameObject hpImageObject = null;

    private void Awake()
    {
        heroImage = this.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = this.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        hpImageObject = this.transform.GetChild(1).gameObject;

        hpImageObject.SetActive(false);
    }

    public void HPImageOn()
    {   // 처음에 영웅 인트로 들어간뒤에 켜져야함
        hpImageObject.SetActive(true);
    }



}       // ClassEnd

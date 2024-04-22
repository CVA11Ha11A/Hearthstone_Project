using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchingScroll_Image : MonoBehaviour
{
    public Sprite[] scrollSprites = null;

    private TextMeshProUGUI scrollText = null;
    private void Awake()
    {
        scrollText = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scrollText.gameObject.SetActive(false);
    }

    public void EndMatchingSetting()
    {
        this.transform.GetComponent<Image>().sprite = scrollSprites[0];
        scrollText.gameObject.SetActive(true);

    }

}

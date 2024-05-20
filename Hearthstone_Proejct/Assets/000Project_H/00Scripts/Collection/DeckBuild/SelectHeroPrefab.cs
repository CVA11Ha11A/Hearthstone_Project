using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectHeroPrefab : MonoBehaviour
{
    private Image classImage = null;
    private TextMeshProUGUI classNameText = null;
    private ClassCard thisClass = default;
    public ClassCard ThisClass
    {
        get
        {
            return thisClass;
        }
        set
        {
            if (thisClass != value)
            {
                thisClass = value;                
            }
            if(thisClass != ClassCard.None && thisClass != ClassCard.Common)
            {
                ImageUpdate();
            }
        }
    }

    public event Action<ClassCard> prefabClickEvent;

    private void Awake()
    {
        classImage = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        classNameText = this.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        Button thisButton = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
        thisButton.onClick.AddListener(IsOnClick);
    }
    

    private void ImageUpdate()
    {

        classImage.sprite = this.transform.parent.GetComponent<DeckBuildSeclectClassInstance>().classSprites[(int)ThisClass -1];
        classNameTextUpdate();
    }
    private void classNameTextUpdate()
    {
        classNameText.text = this.transform.parent.GetComponent<DeckBuildSeclectClassInstance>().classNames[(int)ThisClass -1];
    }

    public void IsOnClick()
    {
        prefabClickEvent?.Invoke(this.ThisClass);
    }

}       // ClassEnd

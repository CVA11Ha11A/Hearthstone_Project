using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDeckImage : MonoBehaviour
{
    public int selectIndex = -1;

    private void Awake()
    {
        this.transform.GetComponent<Button>().onClick.AddListener(this.ButtonOnClickEvent);
    }
    public void ButtonOnClickEvent()
    {
        this.transform.parent.parent.GetComponent<GameStartSelectDeckCanvas>().SelectDeckIndex = this.selectIndex;
    }
}       // SelectDeckImageClassEnd

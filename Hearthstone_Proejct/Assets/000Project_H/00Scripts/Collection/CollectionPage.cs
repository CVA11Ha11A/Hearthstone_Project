using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CollectionPage : MonoBehaviour
{
    public const int MIN_PAGE = 1;
    private int nowPage = default;
    public int NowPage
    {
        get
        {
            return nowPage;
        }
        set
        {
            if (nowPage != value)
            {
                nowPage = value;
            }
            if (nowPage < MIN_PAGE)
            {
                nowPage = MIN_PAGE;
            }
            UpdatePageText();
        }
    }
    private TextMeshProUGUI pageTextRoot = null;
    private StringBuilder sb = null;
    private const string defaultText = "페이지 : ";

    private CollectionCanvasController canvasControllerRoot = null;

    public bool isNextMove = true;    

    private void Awake()
    {
        sb = new StringBuilder();
        canvasControllerRoot = GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>();
        canvasControllerRoot.pageRoot = this;

        pageTextRoot = this.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>();

    }
    void Start()
    {
        nowPage = MIN_PAGE;
        UpdatePageText();
    }

    private void UpdatePageText()
    {
        sb.Clear();
        sb.Append(defaultText).Append(NowPage);
        pageTextRoot.text = sb.ToString();
    }       // UpdatePageText()

    public void NextMoveToPage()
    {
        if (isNextMove == true)
        {
            NowPage++;
            canvasControllerRoot.cardGroupRoot.OutPutCard((ClassCard)canvasControllerRoot.NowPageIcon);
        }
        else { /*PASS*/ }

    }

    public void PreviousMoveToPage()
    {
        if(NowPage != MIN_PAGE) 
        {
            NowPage--;
            canvasControllerRoot.cardGroupRoot.CurrentIndex -= canvasControllerRoot.cardGroupRoot.transform.childCount + 1;
            canvasControllerRoot.cardGroupRoot.OutPutCard((ClassCard)canvasControllerRoot.NowPageIcon);
            isNextMove = true;
        }
    }


}       // CollectionPage ClassEnd

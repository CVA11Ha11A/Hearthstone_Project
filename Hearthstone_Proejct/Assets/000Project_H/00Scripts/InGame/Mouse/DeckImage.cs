using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckImage : MouseInteractionObj
{
    private Image deckCountImage = null;
    private TextMeshProUGUI deckCountText = null;
    private StringBuilder sb = null;
    private InGameDeck playerDeckRoot = null;

    protected override void Awake()
    {
        base.Awake();
        sb = new StringBuilder();
        deckCountImage = this.transform.GetChild(0).GetComponent<Image>();
        deckCountText = this.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        playerDeckRoot = this.transform.parent.GetComponent<InGameDeck>();

        EndObjFunction();
    }


    public override void ObjFunction()
    {
        if (deckCountImage.gameObject.activeSelf == false)
        {
            deckCountImage.gameObject.SetActive(true);
            sb.Clear();
            sb.Append("내 덱에 카드가 ");
            sb.Append($"{playerDeckRoot.InGamePlayerDeck.count.ToString()}");
            sb.Append("장 있습니다.");
            deckCountText.text = sb.ToString();
        }
        else { /*PASS*/ }

    }       // ObjFunction()

    public override void EndObjFunction()
    {
        deckCountImage.gameObject.SetActive(false);
    }       // 

}       // ClassEnd

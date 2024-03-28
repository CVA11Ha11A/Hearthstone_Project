using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionDeckCardList : MonoBehaviour
{
    GameObject[] cardList = null;
    private int currentIndex = default;      // Initalize == 0 

    private DeckInCard deckInCardRoot = null;

    private void Awake()
    {
        GameManager.Instance.GetTopParent(this.transform).GetComponent<CollectionCanvasController>().deckCardListRoot = this;

        cardList = new GameObject[this.transform.childCount];
        for (int i = 0; i < cardList.Length; i++) 
        {
            cardList[i] = this.transform.GetChild(i).gameObject;        
        }
        SetActiveFlaseToChilds();

    }       // Awake()

    private void SetActiveFlaseToChilds()
    {
        for (int i = 0; i < cardList.Length; i++) 
        {
            cardList[i].gameObject.SetActive(false);
        }
    }       // SetActiveFlaseToChilds()


    public void AddToCard(Card addCard_)
    {
        cardList[currentIndex].SetActive(true);

        deckInCardRoot = cardList[currentIndex].GetComponent<DeckInCard>();
        deckInCardRoot.CardName = addCard_.cardName;
        deckInCardRoot.CardCost = addCard_.cost;
        deckInCardRoot.SetCardId(addCard_.cardId);
        deckInCardRoot.cardImageRoot.sprite = addCard_.cardImage.sprite;

        currentIndex++;
    }       // AddToCard()

}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDeckData
{
    public List<Deck> deckList = null;

    private readonly int deckMaxCount = default;

    public PlayerDeckData()
    {
        if(deckList == null)
        {
            deckMaxCount = 9;
            deckList = new List<Deck>(deckMaxCount);            
        }
    }

 

}       // ClassEnd

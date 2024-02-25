using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CardID
{
    Norgannon = 1
}

public enum ClassCard
{
    Common = 0,
    Prist = 1,
    Mage = 2

}

/// <summary>
/// Play : 소환대사 Attack : 공격 Death : 죽음, Stinger : 소환음악
/// </summary>
public enum M_Clip
{
    Play = 0,
    Attack,
    Death,
    Stinger
}
public enum M_Text
{
    Name = 0,
    Empect,
    Cost,
    Hp,
    Damage
}


public class CardManager : MonoBehaviour
{
    public static Dictionary<int, Card> cards = new Dictionary<int, Card>();

    private static CardManager instance = null;
    public static CardManager Instance
    {
        get
        {
            if(instance == null || instance == default)
            {
                GameObject managerObj = new GameObject("CardManager");
                managerObj.AddComponent<CardManager>();                
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        
    }

}       // CardManager ClassEnd


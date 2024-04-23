using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnEndButton : MonoBehaviour
{
    private Button turnEndButton = null;
    private Color myTurnColor = default;
    private Color enemyTurnColor = default;
    private void Awake()
    {
        turnEndButton = this.transform.GetComponent<Button>();
        Color32 myturnColor32 = new Color32(255, 180, 0, 255);
        myTurnColor = myturnColor32;
        Color32 enemyTurnColor32 = new Color32(85, 85, 85, 255);
        enemyTurnColor = enemyTurnColor32;

        turnEndButton.onClick.AddListener(TurnEndButtonMethod);

    }

    private void Start()
    {
        
    }

    private void TurnEndButtonMethod()
    {
       // TODO : 제작해야함
    }
}       // ClassEnd

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

    public void TurnEndButtonMethod()
    {                
        if(InGameManager.Instance.TurnSystem == InGameManager.Instance.gameSycleRoot.NowTurn)
        {
            InGameManager.Instance.TurnEndSync();
        }
        else
        {
            DE.Log("자신의 턴이 아닌데 클릭되서 Pass");
        }
     
        

    }
}       // ClassEnd

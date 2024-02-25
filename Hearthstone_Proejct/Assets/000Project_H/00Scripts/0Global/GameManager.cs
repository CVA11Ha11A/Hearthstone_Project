using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{       // 싱글턴

    private GameManager instance;
    public GameManager Instance
    {
        get 
        { 
            if (instance == null || instance == default)
            {
                GameObject manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();
            }
            return instance; 
        }
    }
    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 120;
    }



}       // GameManager Class

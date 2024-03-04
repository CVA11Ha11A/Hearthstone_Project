using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{       // 싱글턴

    private static GameManager instance;
    public static GameManager Instance
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


    public GameObject GetTopParent(Transform target_)
    {        
        while(target_.parent != null)
        {            
            target_ = target_.parent;
        }

        return target_.gameObject;
    }
    


}       // GameManager Class

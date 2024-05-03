using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardStandardSet : MonoBehaviour
{        
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "LobbyScene")
        {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if(this.gameObject.layer == LayerMask.NameToLayer("Discovery"))
        {
            this.transform.localScale = new Vector3(15f, 15f, 15f);
        }
        else if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            this.transform.localScale = new Vector3(10f, 10f, 10f);
        }


    }

}

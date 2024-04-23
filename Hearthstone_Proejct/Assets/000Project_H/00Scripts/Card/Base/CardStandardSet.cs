using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardStandardSet : MonoBehaviour
{
    private Vector3 fowardRotation = default;
    private Vector3 deckInRotation = default;
    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "LobbyScene")
        {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if(SceneManager.GetActiveScene().name == "InGameScene")
        {
            this.transform.localScale = new Vector3(10f, 10f, 10f);
        }

        fowardRotation = new Vector3(-90f, 0f, 0f);
        deckInRotation = new Vector3(-180f, 0f, -90f);

    }

}

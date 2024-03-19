using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvasController : MonoBehaviour
{    
    private Vector3 originV3 = default;
    private Vector3 moveV3 = default;

    private void Awake()
    {        
        LobbyManager.Instance.mainCanvasRoot = this;
        originV3 = this.transform.position;
        moveV3 = new Vector3(300f, 300f, -300f);
        //this.transform.position = moveV3;
    }


}       // LobbyCanvasController Class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class ClearScene : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("InGameScene");
        }

    }


}

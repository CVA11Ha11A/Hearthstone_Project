using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDeckCanvasTransformController : MonoBehaviour
{

    private Vector3 onPos = default;
    private Vector3 defaultPos = default;
    private float openTime = default;

    private void Awake()
    {
        defaultPos = this.transform.position;
        onPos = this.transform.position;
        onPos.x = 0f;
        openTime = 2.5f;
    }

    public void OutputCanvas()
    {
        LobbyManager.Instance.CanvasOpen(this.transform, onPos, openTime);
    }



}       // ClassEnd

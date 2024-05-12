using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Transform startTras = null;
    public Transform endTras = null;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = this.transform.GetComponent<LineRenderer>();        
    }



    public void DrawParabola(Vector3 startPos_, Vector3 endPos_)
    {

        Vector3 pos = default;
        Vector3 center = (startPos_ + endPos_) * 0.5f;
        center.z -= 3f;
        startPos_ = startPos_ - center;
        endPos_ = endPos_ - center;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            pos = Vector3.Slerp(startPos_, endPos_ , i / (float)(lineRenderer.positionCount - 1));

            lineRenderer.SetPosition(i, pos);
        }
    }       // DrawParabola()

}       // ClassEnd

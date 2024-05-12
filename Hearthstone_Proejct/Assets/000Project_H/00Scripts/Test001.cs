using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;




public class Test001 : MonoBehaviour
{
    public GameObject startTras = null;
    public GameObject endTras = null;

    private LineRenderer lineRenderer = null;

    private void Awake()
    {
        lineRenderer = this.transform.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // 마우스 스크린 좌표 얻기
        Vector3 mouseScreenPosition = Input.mousePosition;
        // 마우스 스크린 좌표를 월드 좌표로 변환
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y,
            Camera.main.nearClipPlane));

        DrawParabola(startTras.transform.position, mouseWorldPosition);
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
            pos = Vector3.Slerp(startPos_, endPos_, i / (float)(lineRenderer.positionCount - 1));

            lineRenderer.SetPosition(i, pos);
        }
    }       // DrawParabola()



}








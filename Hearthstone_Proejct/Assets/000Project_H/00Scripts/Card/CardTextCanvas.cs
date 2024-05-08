using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CardTextCanvas : MonoBehaviour
{
    public GameObject[] textObjRoots = null;
    private Vector3[] originCardTextV3 = null;    

    private void Awake()
    {
        textObjRoots = new GameObject[2];
        for (int i = 0; i < textObjRoots.Length; i++)
        {
            textObjRoots[i] = this.transform.GetChild(3 + i).gameObject;
            //DE.Log($"textObjRoots[i] : {textObjRoots[i].name}");
        }

        originCardTextV3 = new Vector3[textObjRoots.Length];
        for (int i = 0; i < originCardTextV3.Length; i++)
        {
            originCardTextV3[i] = textObjRoots[i].transform.localPosition;
        }        
    }

    public void SetMinionFieldTextPos()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void SetMinionCardTextPos()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(2).gameObject.SetActive(true);
        textObjRoots[0].transform.localPosition = originCardTextV3[0];
        textObjRoots[1].transform.localPosition = originCardTextV3[1];
    }

}       // ClassEnd

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum testEnum
{
    Test001 = 001,
    Test002
}
public class Test003 : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.AddComponent<Test002>();
        StartCoroutine(TESTC());

        Array array = Enum.GetValues(typeof(CardID));

        for (int i = 0; i < array.Length; i++)
        {
            //Debug.Log($"{array.GetValue(i)}\nInt : {(int)array.GetValue(i)}");

        }
    }

    IEnumerator TESTC()
    {
        yield return new WaitForSeconds(3);
        MonoBehaviour desTarget = this.gameObject.GetComponent<Test001>();
        Destroy(desTarget);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DE
{
    public static void Log(string log_)
    {
#if DEVELOP_TIME
        Debug.Log($"{log_}");
#endif
    }
}

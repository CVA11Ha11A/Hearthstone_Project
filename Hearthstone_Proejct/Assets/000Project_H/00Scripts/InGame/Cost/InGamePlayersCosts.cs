using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePlayersCosts : MonoBehaviour
{
    private InGamePlayersCost myCost = null;
    private InGamePlayersCost enemyCost = null;
    public InGamePlayersCost MyCost
    {
        get
        {
            return this.myCost;
        }
    }
    public InGamePlayersCost EnemyCost
    {
        get
        {
            return this.enemyCost;
        }
    }


    public void MyCostSetter(InGamePlayersCost root_)
    {
        this.myCost = root_;
    }
    public void EnemyCostSetter(InGamePlayersCost root_)
    {
        this.enemyCost = root_;
    }

}       // ClassEnd

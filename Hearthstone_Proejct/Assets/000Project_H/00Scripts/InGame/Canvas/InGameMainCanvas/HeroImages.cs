using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroImages : MonoBehaviour
{       // Root를 관리할 컴포넌트

    private HeroImage myHeroImage = null;
    private HeroImage enemyHeroImage = null;

    public HeroImage MyHeroImage
    {
        get
        {
            return this.myHeroImage;
        }
    }
    public HeroImage EnemyHeroImage
    {
        get
        {
            return this.enemyHeroImage;
        }
    }

    public void MyHeroImageRootSetter(HeroImage root_)
    {
        this.myHeroImage = root_;
    }

    public void EnemyHeroImageRootSetter(HeroImage root_)
    {
        this.enemyHeroImage = root_;
    }


}

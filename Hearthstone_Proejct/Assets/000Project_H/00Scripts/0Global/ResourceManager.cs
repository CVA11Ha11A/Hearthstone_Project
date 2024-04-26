using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#region Enums
public enum RClassPullSprite
{

}
public enum RClassVerticalSprite
{
    Anduin = 0,
    Jeina = 1,
    Galosy = 2,
    Guldan = 3,
    SRal = 4,
    Useao = 5,
    Vallila = 6,
    EndPoint
}
#endregion Enums
public class ResourceManager : MonoBehaviour
{       // 리소스들은 여기에서 캐싱되어서 활용될것    2024.04.16
    private static ResourceManager instance = null;
    public static ResourceManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ResourceManger");
                obj.AddComponent<ResourceManager>(); 
            }
            return instance;
        }
    }

    private StringBuilder sb = null;

    private Sprite[] classVerticalSprite = null;
    public Sprite[] ClassVerticalSprite
    {
        get
        {
            return classVerticalSprite;
        }
    }

    private Sprite[] classPullSprite = null;
    public Sprite[] ClassPullSprite
    {
        get
        {
            return this.classPullSprite;
        }
    }

    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            ManagerInIt();
        }
        else if(instance != null)
        {
            Destroy(this.gameObject);
        }

    }       // Awake()

    private void ManagerInIt()
    {
        sb = new StringBuilder();
        ClassVerticalSpriteLoad();
        ClassPullSpriteLoad();
    }

    private void ClassVerticalSpriteLoad()
    {        
        classVerticalSprite = new Sprite[(int)RClassVerticalSprite.EndPoint];
        // TODO : 직업이 늘어날경우 추가해야함 일단 사제, 마법사만 가져옴
        classVerticalSprite[0] = Resources.Load<Sprite>("ClassVerticalSprites/Anduin");
        classVerticalSprite[1] = Resources.Load<Sprite>("ClassVerticalSprites/Jeina");

    }       // ClassVerticalSpriteLoad()

    private void ClassPullSpriteLoad()
    {
        classPullSprite = new Sprite[(int)RClassVerticalSprite.EndPoint];
        // TODO : 직업이 늘어날경우 추가해야함 일단 사제, 마법사만 가져옴
        classPullSprite[0] = Resources.Load<Sprite>("ClassSprites/Anduin");
        classPullSprite[1] = Resources.Load<Sprite>("ClassSprites/Jeina");
    }       // ClassPullSpriteLoad()

}       // ClassEnd

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.IO;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Text;
using TMPro;
using UnityEngine.UI;



[System.Serializable]
public class Test001 : MonoBehaviour
{
        
    public AudioClip[] emoteClip = null;
    public AudioClip[] EmoteClip
    {
        get
        {
            return this.emoteClip;
        }
    }
    private StringBuilder sb = null;

    private void Awake()
    {
       
        sb = new StringBuilder();
        emoteClip = new AudioClip[7];
        HeroSetting();



    }
    
    public void HeroSetting()
    {

        string defaultPath = "ClassEmoteClips/VO_HERO_";
        string heroNum = "09";

        int conversIndex = 0;

        for (int i = 0; i < emoteClip.Length; i++)
        {
            sb.Clear();
            sb.Append(defaultPath).Append(heroNum).Append("_").Append((EEmoteClip)conversIndex);
            DE.Log(sb.ToString());
            emoteClip[i] = Resources.Load<AudioClip>(sb.ToString());
            conversIndex++;
        }
        
    }       // HeroSetting()



}








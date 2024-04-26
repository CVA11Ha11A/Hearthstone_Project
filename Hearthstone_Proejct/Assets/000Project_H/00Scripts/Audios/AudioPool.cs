using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPool : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioSource AudioSource
    {
        get
        {
            return this.audioSource;
        }        
    }
    public bool isKeeping = false;      // 잠시 멈추는 것인지?
    public bool isKeepers = false;      // 내가 멈춘개체가 존재하는지?

    private void Awake()
    {
        this.isKeepers = false;
        this.isKeeping = false;

        this.audioSource = this.transform.GetComponent<AudioSource>();
        this.audioSource.spatialBlend = 0f;
    }

    private void OnDisable()
    {
        if (this.AudioSource.clip != null)
        {
            this.AudioSource.clip.UnloadAudioData();
        }
        else
        {
            this.AudioSource.clip = null;
        }

        if(this.isKeepers == true)
        {
            AudioManager.Instance.KeepSoundPlay();
            this.isKeepers = false;
        }
    }       // OnDisable()


    private void FixedUpdate()
    {
        if (isKeeping == true)
        {
            AudioSource.Pause();        // 멈추기
            AudioManager.Instance.keepingAudioList.Add(this);
        }
        else if (AudioSource.isPlaying == false)
        {
            this.gameObject.SetActive(false);
        }
    }       // FixedUpdate()

    public void PlayAudio(bool isLoop_, AudioClip playClip_, AudioMixerGroup audioGroup_)
    {
        this.AudioSource.loop = isLoop_;
        this.AudioSource.outputAudioMixerGroup = audioGroup_;
        this.AudioSource.clip = playClip_;
        this.AudioSource.Play();
    }       // PlayAudio()

    public void KeepAudioPlay()
    {
        this.isKeeping = false;
        this.AudioSource.UnPause();        
    }

    public AudioMixerGroup GetNowMixerGroup()
    {
        return this.AudioSource.outputAudioMixerGroup;
    }

    public void AudioPlayStop()
    {   // 더이상 오디오를 플레이 할 필요가 없을때 플레이
        this.AudioSource.Stop();
    }


}       // C AudioPool

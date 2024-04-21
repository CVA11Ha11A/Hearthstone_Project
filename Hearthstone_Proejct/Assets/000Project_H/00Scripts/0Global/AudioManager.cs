using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum ESoundBGM
{
    LobbyTheme = 0,
    MatchingTheme1 = 1,
    MatchingTheme2 = 2

}

public enum ESoundSFM
{

}
public enum EAudioMixerGroup
{
    Master = 0,
    BGM = 1,
    SFM = 2
}
public class AudioManager : MonoBehaviour
{   
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            if (instance != null)
            {
                instance = value;
            }
        }
    }

    private List<GameObject> audioObjList = null;       // 오디오를 플레이 시킬 수 있는 개체들의 리스트
    public List<AudioPool> keepingAudioList = null;     // 잠시멈추고 플레이 시킬 개체들의 리스트    

    private AudioClip[] bgmClips = null;    // 리소스
    private AudioClip[] sfmClips = null;    // 리소스 
    public AudioMixerGroup[] mixerGroup = null; // 오디오 그룹들

    private int currentChildCount = default;    // 현재 자식개체의 수
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);

        audioObjList = new List<GameObject>(50);
        for (int i = 0; i < this.transform.childCount; i++)
        {       // 시작시 자식 오브젝트를 리스트에 추가
            this.audioObjList.Add(this.transform.GetChild(i).gameObject);
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        bgmClips = new AudioClip[50];    // 임시 50공간할당 추후 수정 예정
        sfmClips = new AudioClip[50];    // 임시 50공간할당 추후 수정 예정

        AudioResourceLoad();
    }       // Awake()
    void Start()
    {
        PlayBGM(isLoop_: true, ESoundBGM.LobbyTheme);
    }



    private void AudioResourceLoad()
    {
        // 오디오믹서 관련
        string audioMixerPath = "AudioMixer/";
        AudioMixer audioMixer = Resources.Load<AudioMixer>(audioMixerPath + "MainAudioMixer");
        // 로그 찍었을때 해당 오디오 믹서의 그룹을 인스펙터에서 나열되어있는 순서대로 배열에 인덱스에 맞게 넣는것을 확인함
        mixerGroup = audioMixer.FindMatchingGroups("");     

        // Bgm 관련
        string bgmPath = "BGMClips/";
        bgmClips[(int)ESoundBGM.LobbyTheme] = Resources.Load<AudioClip>(bgmPath + "LobbyTheme");
        bgmClips[(int)ESoundBGM.MatchingTheme1] = Resources.Load<AudioClip>(bgmPath + "MatchingTheme1");
        bgmClips[(int)ESoundBGM.MatchingTheme2] = Resources.Load<AudioClip>(bgmPath + "MatchingTheme2");

        // Sfm 관련
    }       // AudioResourceLoad()


    private void CreatePullObj()
    {       // 대충 오브젝트풀 할떄 게임 오브젝트 인스턴스 하고 자기 자식으로 묶고 오디오소스 컴포넌트 추가해주는 함수
        currentChildCount = this.transform.childCount + 1;
        GameObject obj = new GameObject("Sound" + currentChildCount);

        obj.transform.SetParent(this.transform);
        obj.AddComponent<AudioSource>().playOnAwake = false;
        obj.AddComponent<AudioPool>();

        audioObjList.Add(obj);
    }       // CreatePullObj()



    public void PlayBGM(bool isLoop_, ESoundBGM playBGM_)
    {        
        bool isKeeper = false;      // 이번 플레이에서 미룬 오디오가 존재하는지
        int playObjIndex = -1;
        AudioPool tempRoot = null;  // 조건 검사에 사용될 Root

        //DE.Log($"Count : {this.audioObjList.Count}");
        // 플레이할 오브젝트지정
        for (int i = 0; i < this.audioObjList.Count; i++)
        {
            if (audioObjList[i].gameObject.activeSelf == false)
            {
                playObjIndex = i;                
                break;
            }
            else
            {
                // BGM은 한개만 존재해야함
                tempRoot = audioObjList[i].gameObject.GetComponent<AudioPool>();
                if(tempRoot.GetNowMixerGroup() == mixerGroup[(int)EAudioMixerGroup.BGM])
                {
                    tempRoot.isKeeping = true;
                    isKeeper = true;
                }
            }
        }

        if(playObjIndex == -1)
        {            
            CreatePullObj();
            playObjIndex = audioObjList.Count -1;                
        }

        // 여기서 들어온 셋팅설정
        audioObjList[playObjIndex].gameObject.SetActive(true);
        AudioPool audioRoot = audioObjList[playObjIndex].GetComponent<AudioPool>();
        audioRoot.PlayAudio(isLoop_, bgmClips[(int)playBGM_], mixerGroup[(int)EAudioMixerGroup.BGM]);
        if (isKeeper == true) 
        {
            audioRoot.isKeepers = true;
        } 



    }       // PlayBGM(Enum)

    public void PlayBGM(bool isLoop_, AudioClip clip_)
    {        
        int playObjIndex = -1;
        AudioPool tempRoot = null;  // 조건 검사에 사용될 Root

        // 플레이할 오브젝트지정
        for (int i = 0; i < this.audioObjList.Count; i++)
        {
            if (audioObjList[i].gameObject.activeSelf == false)
            {
                playObjIndex = i;                
                break;
            }
            else
            {
                // BGM은 한개만 존재해야함
                tempRoot = audioObjList[i].gameObject.GetComponent<AudioPool>();
                if (tempRoot.GetNowMixerGroup() == mixerGroup[(int)EAudioMixerGroup.BGM])
                {
                    tempRoot.isKeeping = true;
                }

            }
        }
        if (playObjIndex == -1)
        {
            CreatePullObj();
            playObjIndex = audioObjList.Count - 1;
        }

        // 여기서 들어온 셋팅설정
        audioObjList[playObjIndex].gameObject.SetActive(true);
        AudioPool audioRoot = audioObjList[playObjIndex].GetComponent<AudioPool>();
        audioRoot.PlayAudio(isLoop_, clip_, mixerGroup[(int)EAudioMixerGroup.BGM]);
    }       // PlayBGM(AudioClip)

    public void PlayStopBGM(ESoundBGM stopBGM)
    {
        AudioPool poolRoot = null;
        for(int i = 0; i < audioObjList.Count; i++)
        {
            if (audioObjList[i].gameObject.activeSelf == true)
            {
                poolRoot = audioObjList[i].gameObject.GetComponent<AudioPool>();
                if(poolRoot.AudioSource.clip == bgmClips[(int)stopBGM])
                {
                    poolRoot.AudioPlayStop();
                    return;
                }
            }
            else { /*PASS*/ }
        }
    }
    public void PlayStopBGM(ESoundBGM stopBGM1_, ESoundBGM stopBGM2_)
    {
        AudioPool poolRoot = null;
        for (int i = 0; i < audioObjList.Count; i++)
        {
            if (audioObjList[i].gameObject.activeSelf == true)
            {
                poolRoot = audioObjList[i].gameObject.GetComponent<AudioPool>();
                if (poolRoot.AudioSource.clip == bgmClips[(int)stopBGM1_] || poolRoot.AudioSource.clip == bgmClips[(int)stopBGM2_])
                {
                    poolRoot.AudioPlayStop();
                    return;
                }
            }
            else { /*PASS*/ }
        }
    }

    public void PlaySFM()
    {

    }

    public void KeepSoundPlay()
    {       // 킵된 오디오들의 Pause를 풀어주며 킵된오디오 리스트에 제거해주는 함수
        for(int i = 0; i < keepingAudioList.Count; i++)
        {
            keepingAudioList[i].KeepAudioPlay();
        }
        keepingAudioList.Clear();
    }       // KeepSoundPlay()


}       // ClassEnd

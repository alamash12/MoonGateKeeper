using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider[] BGMSliders;
    public Slider[] SFXSliders;

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            else { return instance; }
        }
    }
    AudioSource[] _audioSources = new AudioSource[(int)UI_Define.Sounds.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    float _bgmvolume = 1.0f;
    float _sfxvolume = 1.0f;

    public float BGMVolume { get { return _bgmvolume; } set { PlayerPrefs.SetFloat("BGMVolume", value >= 1 ? 1 : value); } }
    public float SFXVolume { get { return _sfxvolume; } set { PlayerPrefs.SetFloat("SFXVolume", value >= 1 ? 1 : value); } }

    private void Awake()
    {
        if(Instance == null)
        {
            instance = this;
        }
        else if(Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        Init();
    }

  
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            UnityEngine.Object.DontDestroyOnLoad(root);

            for (UI_Define.Sounds s = UI_Define.Sounds.BGM; s < UI_Define.Sounds.MaxCount; s++)
            {
                GameObject go = new GameObject { name = $"{s}" };
                _audioSources[(int)s] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 0.7f);
            _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
            _audioSources[(int)UI_Define.Sounds.BGM].loop = true;
        }
        else
        {
            for (UI_Define.Sounds s = UI_Define.Sounds.BGM; s < UI_Define.Sounds.MaxCount; s++)
            {
                GameObject go = root.transform.Find($"@{s}").gameObject;
                _audioSources[(int)s] = go.GetComponent<AudioSource>();
            }

            _audioSources[(int)UI_Define.Sounds.BGM].loop = true;
        }

        _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 1f); // 게임 시작시 플레이어프리펩의 값을 불러옴
        _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetVolume(UI_Define.Sounds.BGM, _bgmvolume); // 불러온 값으로 오디오소스 볼륨 변경
        SetVolume(UI_Define.Sounds.SFX, _sfxvolume);
        VolumeInit();

        BGMSliders[0].onValueChanged.AddListener(delegate { VolumeChange(UI_Define.Sounds.BGM, BGMSliders[0]); });
        BGMSliders[1].onValueChanged.AddListener(delegate { VolumeChange(UI_Define.Sounds.BGM, BGMSliders[1]); });
        SFXSliders[0].onValueChanged.AddListener(delegate { VolumeChange(UI_Define.Sounds.SFX, SFXSliders[0]); });
        SFXSliders[1].onValueChanged.AddListener(delegate { VolumeChange(UI_Define.Sounds.SFX, SFXSliders[1]); });
    }
    void VolumeInit() // 초기 슬라이더 값 할당
    {
        for(int i = 0; i < BGMSliders.Length;i++)
        {
            BGMSliders[i].value = BGMVolume;
            SFXSliders[i].value = SFXVolume;
        }
    }
    private void VolumeChange(UI_Define.Sounds sounds, Slider slider) // 슬라이더의 값이 변경될때마다 호출
    {
        float volume = slider.value;
        if(sounds == UI_Define.Sounds.BGM)
        { 
            BGMVolume = volume;
            SetVolume(UI_Define.Sounds.BGM, volume);
            BGMSliders[0].value = slider.value;
            BGMSliders[1].value = slider.value;
        }
        else
        {
            SFXVolume = volume;
            SetVolume(UI_Define.Sounds.SFX, volume);
            SFXSliders[0].value = slider.value;
            SFXSliders[1].value = slider.value;
        }
    }
    void SliderInit() // 슬라이더에 playerPref 연동하고 값 적용
    {
        for (int i = 0; i < 2; i++)
        {
            BGMSliders[i].value = BGMVolume;
            SFXSliders[i].value = SFXVolume;
        }
    }

    /// <summary>
    /// SFX용 PlayOneShot으로 구현 
    /// </summary>
    /// <param name="SFXSound"> Define.SFX Enum 에서 가져오기를 바람 </param>
    /// <param name="volume"></param>

    public void Play(UI_Define.SFX SFXSound)
    {
        string path = $"{SFXSound}";
        AudioClip audioClip = GetOrAddAudioClip(path, UI_Define.Sounds.SFX);
        Play(audioClip, UI_Define.Sounds.SFX);
    }
    /// <summary>
    /// BGM용 Play로 구현
    /// </summary>
    /// <param name="BGMSound">Define.BGM Enum 에서 가져오기를 바람 </param>
    /// <param name="volume"></param>
    public void Play(UI_Define.BGM BGMSound)
    {
        string path = $"{BGMSound}";
        AudioClip audioClip = GetOrAddAudioClip(path, UI_Define.Sounds.BGM);
        Play(audioClip, UI_Define.Sounds.BGM);
    }

    void Play(AudioClip audioClip, UI_Define.Sounds type = UI_Define.Sounds.SFX)
    {
        if (audioClip == null)
            return;

        if (type == UI_Define.Sounds.BGM)
        {
            AudioSource audioSource = _audioSources[(int)UI_Define.Sounds.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)UI_Define.Sounds.SFX];
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, UI_Define.Sounds type = UI_Define.Sounds.SFX)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";
        AudioClip audioClip = null;

        if (type == UI_Define.Sounds.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");
        return audioClip;
    }

    public void SetVolume(UI_Define.Sounds type, float volume)
    {
        _audioSources[(int)type].volume = volume;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
    public void ButtonClick()
    {
        Play(UI_Define.SFX.ButtonClick);
    }
}

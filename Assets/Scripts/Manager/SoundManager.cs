using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource[] _audioSources = new AudioSource[(int)UI_Define.Sounds.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    float _bgmvolume = 1.0f;
    float _sfxvolume = 1.0f;

    public float BGMVolume { get { return _bgmvolume; } set { PlayerPrefs.SetFloat("BGMVolume", value >= 1 ? 1 : value); } }
    public float SFXVolume { get { return _sfxvolume; } set { PlayerPrefs.SetFloat("SFXVolume", value >= 1 ? 1 : value); } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
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

        _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

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

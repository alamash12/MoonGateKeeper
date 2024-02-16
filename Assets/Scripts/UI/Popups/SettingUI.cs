using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SettingUI : UI_Base
{
    public static SettingUI Instance;
    enum Texts
    {

    }

    enum Buttons
    {
        CloseButton
    }

    enum Sliders
    {
        BGMSlider,
        SFXSlider,
    }
    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        BindSliderEvt();
        BindEvent(Get<Button>((int)Buttons.CloseButton).gameObject, Close);

        Get<Slider>((int)Sliders.BGMSlider).value = 1f - (SoundManager.instance.BGMVolume);
        Get<Slider>((int)Sliders.SFXSlider).value = 1f - (SoundManager.instance.SFXVolume);
        gameObject.SetActive(false);

    }

    void Close(PointerEventData evt)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    void BindSliderEvt()
    {
        Get<Slider>((int)Sliders.BGMSlider).onValueChanged.AddListener(delegate { VolumeChange(UI_Define.Sounds.BGM); });
        Get<Slider>((int)Sliders.SFXSlider).onValueChanged.AddListener(delegate { VolumeChange(UI_Define.Sounds.SFX); });
    }
    void VolumeChange(UI_Define.Sounds Sound)
    {
        float volume;
        if (Sound == UI_Define.Sounds.BGM)
        {
            volume = 1 - (Get<Slider>((int)Sliders.BGMSlider).value);
            SoundManager.instance.BGMVolume = volume;
        }
        else
        {
            volume = 1 - (Get<Slider>((int)Sliders.SFXSlider).value);
            SoundManager.instance.SFXVolume = volume;
        }
        SoundManager.instance.SetVolume(Sound, volume);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
}

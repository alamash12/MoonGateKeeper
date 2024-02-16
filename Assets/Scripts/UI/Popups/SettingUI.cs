using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SettingUI : UI_Base
{
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

    private void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        BindSliderEvt();
        BindEvent(Get<Button>((int)Buttons.CloseButton).gameObject, Close);

    }

    void Close(PointerEventData evt)
    {
        Destroy(gameObject);
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
            volume = Get<Slider>((int)Sliders.BGMSlider).value;
            SoundManager.instance.BGMVolume = volume;
        }
        else
        {
            volume = Get<Slider>((int)Sliders.SFXSlider).value;
            SoundManager.instance.SFXVolume = volume;
        }
        SoundManager.instance.SetVolume(Sound, volume);
    }
}

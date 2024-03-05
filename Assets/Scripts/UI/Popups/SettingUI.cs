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

        BindEvent(Get<Button>((int)Buttons.CloseButton).gameObject, Close);
    }

    void Close(PointerEventData evt)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }
}

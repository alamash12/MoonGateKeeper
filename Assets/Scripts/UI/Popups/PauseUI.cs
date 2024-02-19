using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : UI_Base
{
    public static PauseUI Instance;
    public GameObject startScreen;
    enum Texts
    {

    }

    enum Buttons
    {
        CloseButton,
        //ReturnToMenu,
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
        //BindEvent(Get<Button>((int)Buttons.ReturnToMenu).gameObject, ReturnMenu);

        gameObject.SetActive(false);

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
    public void ReturnMenu(PointerEventData evt)
    {
        startScreen.SetActive(true);
        gameObject.SetActive(false);
        PoolManager.Instance.ReturnAll();
    }
}

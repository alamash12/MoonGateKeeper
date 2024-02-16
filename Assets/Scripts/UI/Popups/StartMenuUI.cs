using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenuUI : UI_Base
{
    enum Texts
    {
        Title1,
        Title2,
    }

    enum Buttons
    {
        StartButton,
        ConfigButton,
        ExitButton,
    }

    private void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        BindEvent(Get<Button>((int)Buttons.StartButton).gameObject, GameStart);
        BindEvent(Get<Button>((int)Buttons.ConfigButton).gameObject, Config);
        BindEvent(Get<Button>((int)Buttons.ExitButton).gameObject, GameExit);
    }

    void GameStart(PointerEventData evt)
    {
        Destroy(gameObject);
        GameManager.instance.StartStage();
    }

    void Config(PointerEventData evt)
    {
        UIManager.LoadUI(UI_Define.UI_Type.SettingUI);
    }
    void GameExit(PointerEventData evt)
    {
        Application.Quit();
    }
}

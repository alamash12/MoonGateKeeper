using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Define
{
    public enum Sounds
    {
        BGM,
        SFX,
        MaxCount
    }
    public enum BGM
    {
        MainBGM,
        MaxCount
    }
    public enum SFX
    {
        MaxCount
    }

    public enum UI_Type
    {
        StartScreen,
        SettingUI,

    }

    /// <summary>
    /// UI Event 종류 지정
    /// </summary>
    public enum UIEvent
    {
        Click,
        Drag,
        DragEnd,
    }
}

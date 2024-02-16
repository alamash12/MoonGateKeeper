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
        StageBGM,
        MaxCount
    }
    public enum SFX
    {
        ButtonClick,
        Upgrade_select,
        rabbit_cthulu_hit,
        Normal_turret,
        Tank_turret,
        Ironed_turret,
        Freeze_turret,
        Electric_turret,
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public GameObject SettingUI;
    public GameObject AugmentUI;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenSetting()
    {
        SettingUI.SetActive(true);
    }

    public void OpenAugment()
    {
        AugmentUI.SetActive(true);
    }
}

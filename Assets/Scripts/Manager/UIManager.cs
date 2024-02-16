using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static UIManager _instance = null;

    public static UIManager instance { get { Init(); return _instance; } }
    GameObject _root;
    static void Init()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<UIManager>();

            if (_instance == null)
            {
                GameObject uim = new GameObject("UIManager");
                _instance = uim.AddComponent<UIManager>();
            }

            DontDestroyOnLoad(_instance.gameObject);

        }
    }
    public static GameObject Root
    {
        get
        {
            if (instance._root == null)
            {

                GameObject root = GameObject.Find("UI_Root");
                if (root == null)
                {
                    root = new GameObject { name = "UI_Root" };
                }

                _instance._root = root;
                return _instance._root;
            }
            else
            {
                return _instance._root;
            }
        }
    }
    static public GameObject LoadUI(UI_Define.UI_Type uI_Type)
    {
        GameObject ui;
        try
        {
            ui = Instantiate(Resources.Load<GameObject>($"Prefabs/UI/{uI_Type.ToString()}"));
            ui.transform.parent = Root.transform;
            return ui;
        }
        catch
        {
            Debug.LogError($"UI Load 문제 발생 Prefabs/UIs/{uI_Type.ToString()} 확인 바람");
            return null;
        }
    }
}

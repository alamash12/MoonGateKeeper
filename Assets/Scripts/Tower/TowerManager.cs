using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    private void Awake()
    {
        instance = this; 
    }

    public static TowerClass[] Towers = new TowerClass[5];

}

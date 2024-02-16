using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TierByColor", menuName = "ScriptableObjects/TierbyColor", order = 51)]
public class MonsterTierByColor : ScriptableObject
{
    [SerializeField]
    private Color [] monsterColor = new Color[10];
    public Color [] MonsterColor { get { return monsterColor; } }
}


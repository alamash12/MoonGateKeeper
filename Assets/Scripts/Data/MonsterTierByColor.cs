using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TierByColor", menuName = "ScriptableObjects/TierbyColor", order = 51)]
public class MonsterTierByColor : ScriptableObject
{
    [SerializeField]
    private int monsterTier;
    public int MosnterTier { get { return monsterTier; } }

    [SerializeField]
    private Color monsterColor;
    public Color MonsterColor { get { return monsterColor; } }
}


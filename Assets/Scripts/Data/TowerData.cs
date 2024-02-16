using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Base = 0,
    Slow,
    Power,
    KnockBack,
    AreaAttack,
    MaxCount
}

[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData", order = 51)]
public class TowerData : ScriptableObject
{

    [SerializeField]
    private TowerType towerType;
    public TowerType TowerType { get { return towerType; } }

    [SerializeField]
    private int towerLevel;
    public int TowerLevel { get { return towerLevel; } }

    [SerializeField]
    private float towerEfficiency;
    public float TowerEfficiency { get { return towerEfficiency; } }

    [SerializeField]
    private float attackTerm;
    public float AttackTerm { get { return attackTerm; } }


}

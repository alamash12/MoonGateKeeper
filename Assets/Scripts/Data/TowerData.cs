using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Base = 0,
    Slow,
    Power,
    KnockBack,
    MaxCount
}

[CreateAssetMenu(fileName = "New TowerData", menuName = "Tower Data", order = 51)]
public class TowerData : ScriptableObject
{

    [SerializeField]
    private TowerType towerType;
    public TowerType TowerType { get { return towerType; } }

    [SerializeField]
    private int towerLevel;
    public int TowerLevel { get { return towerLevel; } }

    [SerializeField]
    private int upgradeCost1;
    public int UpgradeCost1 { get { return upgradeCost1; } }

    [SerializeField]
    private int upgradeCost2;
    public int UpgradeCost2 { get { return upgradeCost2; } }

    [SerializeField]
    private float towerEfficiency0;
    public float TowerEfficiency0 { get { return towerEfficiency0; } }
    [SerializeField]
    private float towerEfficiency1;
    public float TowerEfficiency1 { get { return towerEfficiency1; } }
    [SerializeField]
    private float towerEfficiency2;
    public float TowerEfficiency2 { get { return towerEfficiency2; } }

    [SerializeField]
    private float attackTerm;
    public float AttackTerm { get { return attackTerm; } }


}

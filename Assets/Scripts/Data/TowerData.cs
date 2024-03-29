using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    Base = 0,
    Seize,
    Slow,
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
    private float towerEfficiency;
    public float TowerEfficiency { get { return towerEfficiency; } }

    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }


}

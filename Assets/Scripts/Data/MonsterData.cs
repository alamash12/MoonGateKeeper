using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterData", order = 51)]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private string monsterName;
    public string MonsterName { get { return monsterName; } }
    [SerializeField]
    private float attackDamage;
    public float AttackDamege { get { return attackDamage; } }
    [SerializeField]
    private float attackSpeed;
    public float AttackSpeed { get { return attackSpeed; } }
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }
    [SerializeField]
    private int killReward;
    public int KillReward { get { return killReward; } }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterData", order = 51)]
public class MonsterData : ScriptableObject
{
    public enum monsterName
    {
        Rabbit = 0,
        Chtulu,
        MaxCount
    }

    [SerializeField]
    private monsterName mName;
    public monsterName MName { get { return mName; } }
    [SerializeField]
    private float maxHealth;
    public float MaxHealth { get { return maxHealth; } }
    [SerializeField]
    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

}

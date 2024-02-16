using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define; 

public class BaseTower : MonoBehaviour
{
    public static BaseTower Instance;
    private void Awake()
    {
        Instance = this;
    }
    public TowerData TowerData;
    public float TowerEffeciency;
    public float AttackTerm;
    public MonsterClass nearMonster;
    public void Start()
    {
        TowerEffeciency = TowerData.TowerEfficiency;
        AttackTerm = TowerData.AttackTerm;
        StartCoroutine(bulletSpawn());
    }

    IEnumerator bulletSpawn()
    {
        while (true)
        {
            GameObject NearestMonGO = PoolManager.GetMonWithLargestX();
            if (NearestMonGO != null)
            {
                nearMonster = NearestMonGO.GetComponent<MonsterClass>();
                Bullet bullet = PoolManager.GetObject((int)PoolGameObjectType.bullet).GetComponent<Bullet>();
                bullet.BulletFire(transform, nearMonster, TowerData.TowerType, TowerData.TowerEfficiency);
            }
            yield return new WaitForSeconds(1f / (TowerData.AttackTerm * GameManager.instance.TowerAttackSpeed));
        }
    }


}

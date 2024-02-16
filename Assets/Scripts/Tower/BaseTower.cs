using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define; 

public class BaseTower : MonoBehaviour
{
    public TowerData TowerData;
    private TowerType towerType;
    public GameObject bulletPrefab;
    public MonsterClass nearMonster; // 이따가 연결할거임
    public void Start()
    {
        towerType = TowerType.Base;
        StartCoroutine(bulletSpawn());
    }

    IEnumerator bulletSpawn()
    {
        while(true)
        {
            GameObject NearestMonGO = PoolManager.GetMonWithLargestX();
            if (NearestMonGO != null)
            {
                nearMonster = NearestMonGO.GetComponent<MonsterClass>();
                Bullet bullet = PoolManager.GetObject((int)PoolGameObjectType.bullet).GetComponent<Bullet>();
                bullet.BulletFire(transform, nearMonster, towerType, TowerData.TowerEfficiency);
            }
            yield return new WaitForSeconds(1f/(TowerData.AttackTerm * GameManager.instance.TowerAttackSpeed));
        }
    }

   
}

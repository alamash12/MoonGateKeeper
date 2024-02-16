using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SlowTower : MonoBehaviour
{
    public static SlowTower instance;
    private void Awake()
    {
        instance = this;
    }
    public TowerData TowerData;
    public float TowerEffeciency;
    public float AttackTerm;
    public MonsterClass nearMonster; // 이따가 연결할거임
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
                bullet.BulletFire(transform, nearMonster, TowerData.TowerType, TowerEffeciency);
                transform.parent.GetComponent<Animator>().SetTrigger(TowerData.TowerType.ToString());
            }
            yield return new WaitForSeconds(1f / (AttackTerm * GameManager.instance.TowerAttackSpeed));
        }
    }

}

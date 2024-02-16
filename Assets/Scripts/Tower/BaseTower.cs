using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public TowerData TowerData;
    private TowerType towerType;
    private float FirstAttackTerm;
    public GameObject bulletPrefab;
    public MonsterClass nearMonster; // 이따가 연결할거임
    public void Start()
    {
        FirstAttackTerm = 3f;
        towerType = TowerType.Base;

        StartCoroutine(bulletSpawn());
    }

    private void Update()
    {
        
    }

    IEnumerator bulletSpawn()
    {
        yield return new WaitForSeconds(FirstAttackTerm);
        while(true)
        {
            GameObject obj = PoolManager.GetObject((int)towerType);
            Bullet bullet = obj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.BulletFire(transform, nearMonster, towerType, TowerData.TowerEfficiency);
            }
            yield return new WaitForSeconds(TowerData.AttackTerm);
        }
    }

    public Transform CheckTransform()
    {
        return nearMonster.transform;
    }
}

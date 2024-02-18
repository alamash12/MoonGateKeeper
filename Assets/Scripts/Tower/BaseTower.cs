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
        AttackTerm = TowerData.AttackSpeed;
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
                SoundManager.instance.Play(UI_Define.SFX.Normal_turret);            
            }           
            yield return new WaitForSeconds(1f / (AttackTerm * GameManager.instance.TowerAttackSpeed));
        }
    }


}

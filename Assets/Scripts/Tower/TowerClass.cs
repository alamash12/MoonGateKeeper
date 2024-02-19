using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class TowerClass : MonoBehaviour
{
    public static TowerClass Instance;
    private void Awake()
    {
        Instance = this;
    }
    public TowerData TowerData;
    public float TowerEffeciency;
    public float AttackSpeed;
    public MonsterClass nearMonster; // 이따가 연결할거임
    public Animator animator;

    void Start()
    {
        TowerEffeciency = TowerData.TowerEfficiency;
        AttackSpeed = TowerData.AttackSpeed;
        StartCoroutine(bulletSpawn());
    }

    IEnumerator bulletSpawn()
    {
        while(true)
        {
            if(TowerData.TowerType == TowerType.AreaAttack)
            {
                if (PoolManager.Instance.livingObjectList.Count > 0)
                {
                    foreach (var Enemy in PoolManager.Instance.livingObjectList.ToList())
                    {
                        Enemy.GetComponent<MonsterClass>().getDamaged(TowerEffeciency);
                    }

                    transform.parent.GetComponent<Animator>().SetTrigger(TowerData.TowerType.ToString());
                    animator.SetTrigger("Shoot");
                    SoundManager.Instance.Play(UI_Define.SFX.Electric_turret);
                }
            }
            else
            {
                GameObject NearestMonGO = PoolManager.GetMonWithLargestX();
                if (NearestMonGO != null)
                {
                    nearMonster = NearestMonGO.GetComponent<MonsterClass>();
                    Bullet bullet = PoolManager.GetObject((int)PoolGameObjectType.bullet).GetComponent<Bullet>();
                    bullet.BulletFire(transform, nearMonster, TowerData.TowerType, TowerEffeciency);
                    transform.parent.GetComponent<Animator>().SetTrigger(TowerData.TowerType.ToString());
                    SoundManager.Instance.Play(UI_Define.SFX.Ironed_turret);
                }
            }            
            yield return new WaitForSeconds(1f / (AttackSpeed * GameManager.instance.TowerAttackSpeed));
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;


public class TowerClass : MonoBehaviour
{
    // 타워는 딱 5개만 필요하다. 그러면 그 타워 가지고 있는 배열 딱 하나만 있으면 된다.
    public static TowerClass[] Towers;

    public TowerData TowerData;
    public float TowerEffeciency;
    public float AttackSpeed;
    public MonsterClass nearMonster;
    public Animator animator;


    private void Awake()
    {
        // 게임 씬에서 타워 오브젝트를 생성하고 배열에 추가합니다. 한번만 돈다! 
        if (Towers == null)
        {
            Towers = new TowerClass[(int)TowerType.MaxCount];
        }

        // 현재 타워 오브젝트를 배열에 등록합니다.
        int towerIndex = (int)TowerData.TowerType;
        if (towerIndex >= 0 && towerIndex < Towers.Length)
        {
            Towers[towerIndex] = this;
        } 
    }

    public void OnEnable()
    {
        if (Towers[(int)TowerData.TowerType] == null)
        {
            Towers[(int)TowerData.TowerType] = this;
        }
    }

    void Start()
    {
        TowerEffeciency = TowerData.TowerEfficiency;
        AttackSpeed = TowerData.AttackSpeed;
        StartCoroutine(bulletSpawn());
    }

    public static void TowerEfficiencyUP(TowerType index, float gradient)
    {
        if (Towers[(int)index] != null)
        {
            Towers[(int)index].TowerEffeciency += gradient;
        }
    }

    public static void TowerSpeedUP(TowerType index, float gradient)
    {
        if (Towers[(int)index] != null)
        {
            Towers[(int)index].AttackSpeed += gradient;
        }
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

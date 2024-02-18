using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class WideTower : MonoBehaviour
{
    public static WideTower Instance;
    private void Awake()
    {
        Instance = this;
    }
    public TowerData TowerData;
    public float TowerEffeciency;
    public float AttackTerm;
    public MonsterClass nearMonster; // 이따가 연결할거임
    public Animator animator;
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
            if (PoolManager.Instance.livingObjectList.Count > 0)
            {
                foreach (var Enemy in PoolManager.Instance.livingObjectList.ToList())
                {
                    Enemy.GetComponent<MonsterClass>().getDamaged(TowerEffeciency);
                }

                transform.parent.GetComponent<Animator>().SetTrigger(TowerData.TowerType.ToString());
                animator.SetTrigger("Shoot");
                SoundManager.instance.Play(UI_Define.SFX.Electric_turret);
            }

            yield return new WaitForSeconds(1f / (AttackTerm * GameManager.instance.TowerAttackSpeed));
        }
    }

}

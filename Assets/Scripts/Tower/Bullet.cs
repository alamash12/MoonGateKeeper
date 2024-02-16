using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public TowerData towerData;
    public float bulletSpeed;
    private InteractionType interactionType;
    private float efficiency;
    private int returnPool; // 풀에 반환하기 위해서 타워타입을 받아와서 그에 맞춰서 반환
    public enum InteractionType
    {
        Damage,
        Slow,
        knockback,
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void BulletFire(Transform spawnerPos, MonsterClass monster, TowerType towerType, float efficiency)
    {
        if(monster == null)
        {
            return;
        }
        Vector3 direction = (spawnerPos.position - monster.transform.position).normalized;
        gameObject.transform.position = spawnerPos.position;
        rb.velocity = direction * bulletSpeed;
        returnPool = (int)towerType;
        switch (towerType)
        {
            case TowerType.Slow:
                interactionType = InteractionType.Slow;
                break;
            case TowerType.KnockBack:
                interactionType = InteractionType.knockback;
                break;
            default:
                interactionType = InteractionType.Damage;
                break;
        }
        this.efficiency = efficiency;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "monster")
        {
            InteractionWithMonster(collision.gameObject.GetComponent<MonsterClass>(), efficiency);
            //오브젝트 풀에 탄환 반환
        }
    }

    private void InteractionWithMonster(MonsterClass monsterClass, float efficiency)
    {
        switch (interactionType)
        {
            case InteractionType.Damage:
                // 데미지
                break;
            case InteractionType.Slow:
                // 슬로우
                break;
            case InteractionType.knockback:
                // 넉백
                break;
        }
    }

    private void OnBecameInvisible() // 화면 밖으로 나갔을 때 호출되는 함수
    {
        PoolManager.ReturnObject(gameObject, returnPool);
    }
}

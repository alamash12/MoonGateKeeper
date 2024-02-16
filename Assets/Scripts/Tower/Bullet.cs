using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer SR;
    private float bulletSpeed = 10;
    private InteractionType interactionType;
    private float efficiency;
    public Sprite[] BulletSprites;
    public enum InteractionType
    {
        Damage,
        Slow,
        knockback,
    }
  
    public void BulletFire(Transform spawnerPos, MonsterClass monster, TowerType towerType, float efficiency)
    {
        gameObject.transform.position = spawnerPos.position;
        Vector3 direction = -(spawnerPos.position - monster.transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
        SR.sprite = BulletSprites[(int)towerType];
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
            PoolManager.ReturnObject(gameObject, ((int)PoolGameObjectType.bullet));
        }
        if (collision.gameObject.tag == "Out")
        {
            PoolManager.ReturnObject(gameObject, ((int)PoolGameObjectType.bullet));
        }
    }

    private void InteractionWithMonster(MonsterClass monsterClass, float efficiency)
    {
        switch (interactionType)
        {
            case InteractionType.Damage:
                monsterClass.getDamaged(efficiency);
                break;
            case InteractionType.Slow:
                monsterClass.getSlowed(1 - efficiency);
                break;
            case InteractionType.knockback:
                monsterClass.getKnockBacked(efficiency);
                break;
        }
    }

    
    
}

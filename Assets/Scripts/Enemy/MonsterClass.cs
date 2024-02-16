using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterClass : MonoBehaviour
{
    [SerializeField]
    private MonsterData monsterData;
    public MonsterData MonsterData {  get { return monsterData; } }

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private float monsterHealth;

    public int monsterTier;
    public bool isSlowed;
    public float monsterSpeed;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        monsterSpeed = monsterData.MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * monsterSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Tower")
        {
            GameManager.instance.setHP(GameManager.instance.entireHP--);
            ReturnToPool();
        }
    }

  
    public void getDamaged(float Damage)
    {
        monsterHealth -= Damage;
        if(monsterHealth < 0)
        {
            ReturnToPool();
            GameManager.instance.killCount++;
            GameManager.instance.CheckStageEnd();
        }
    }

 
    public void getSlowed(float Magnitude)
    {
        if(!isSlowed)
        {
            isSlowed = true;
            monsterSpeed *= Magnitude;
        }
    }

    void ReturnToPool()
    {
        if (monsterData.MName == MonsterData.monsterName.Rabbit)
        {
            PoolManager.ReturnObject(gameObject, (int)PoolGameObjectType.Rabbit);
        }
        else
        {
            PoolManager.ReturnObject(gameObject, (int)PoolGameObjectType.Chtulu);
        }
    }


    public void getKnockBacked(float Range)
    {
        StartCoroutine(KnockBackCoroutine(Range));
    }

    IEnumerator KnockBackCoroutine(float range)
    {
        // 현재 위치와 타겟 위치 계산
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position - Vector3.right * range;

        float elapsedTime = 0f;
        float duration = 1f; // 1초 동안 밀기

        while (elapsedTime < duration)
        {
            // 현재 시간에 따른 보간값 계산
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // 마지막에 위치 보정
        transform.position = targetPosition;
    }

    public void setGrade(int tier, Color color)
    {
        if(monsterData.MName == MonsterData.monsterName.Rabbit)
        {
            spriteRenderer.color = color;
            monsterHealth = 5 + tier;
        }
        else if(monsterData.MName == MonsterData.monsterName.Chtulu)
        {
            spriteRenderer.color = color;
            monsterHealth = 10 + 2 * tier;
        }

        if(GameManager.instance.level > 20)
        {
            monsterHealth += GameManager.instance.level - 20;
        }
    }
}


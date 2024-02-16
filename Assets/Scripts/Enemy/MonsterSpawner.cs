using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterSpawner : MonoBehaviour
{
    public static MonsterSpawner Instance;
    private void Awake()
    {
        Instance = this; 
    }
    List<GameObject> Spawns = new List<GameObject>();

    public MonsterTierByColor tierByColor;

    public int rabbitSpawnCount;
    public int chtuluSpawnCount;



    [SerializeField]
    private GameObject RabbitPrefab;
    [SerializeField]
    private GameObject ChtuluPrefab;

    [SerializeField]
    private float RabbitSpawnRate;          // 스폰 주기
    [SerializeField]
    private float ChtuluSpawnRate;          // 스폰 주기
    [SerializeField]
    private Transform RabbitSpawnPoint;
    [SerializeField]
    private Transform ChtuluSpawnPoint;

    public int rabbitGrade;
    public int chtuluGrade;
   
    IEnumerator SpawnRabbit(int rabbitCount)
    {
        for(int i = 0; i < rabbitCount; i++)
        {
            GameObject rab = PoolManager.GetObject((int)PoolGameObjectType.Rabbit);
            rab.transform.position = RabbitSpawnPoint.position;
            Color nowRabColor = tierByColor.MonsterColor[rabbitGrade - 1];
            rab.GetComponent<MonsterClass>().setGrade(rabbitGrade, nowRabColor);
            yield return new WaitForSeconds(1f / RabbitSpawnRate); // 스폰 간격 설정
        }
    }

    IEnumerator SpawnChtulu(int chtuluCount)
    {
        for (int i = 0; i < chtuluCount; i++)
        {
            GameObject Cht = PoolManager.GetObject((int)PoolGameObjectType.Chtulu);
            Cht.transform.position = ChtuluSpawnPoint.position;
            Color nowChtColor = tierByColor.MonsterColor[chtuluGrade - 1];
            Cht.GetComponent<MonsterClass>().setGrade(chtuluGrade, nowChtColor);
            yield return new WaitForSeconds(1f / ChtuluSpawnRate); // 스폰 간격 설정
        }
    }

    public void SpawnWave(int rabbitCount, int rabbitTier, int chtuluCount, int chtuluTier)
    {
        rabbitGrade = rabbitTier;
        chtuluGrade = chtuluTier;
        StartCoroutine(SpawnRabbit(rabbitCount));
        StartCoroutine(SpawnChtulu(chtuluCount));
    }
}

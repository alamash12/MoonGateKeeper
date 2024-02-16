using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public TowerData TowerData;
    private float FirstAttackTerm;
    public GameObject bulletPrefab;
    public GameObject nearMonster; // 이따가 연결할거임
    public void Start()
    {
        FirstAttackTerm = 3f;


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
            Instantiate(bulletPrefab);
            
            yield return new WaitForSeconds(TowerData.AttackTerm);
        }
    }

    public Transform CheckTransform()
    {
        return nearMonster.transform;
    }
}

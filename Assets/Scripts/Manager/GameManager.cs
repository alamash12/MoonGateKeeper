using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    #region 싱글톤
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;               
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            if (instance != this) 
                Destroy(this.gameObject);
        }
    }
    #endregion

    public int entireHP = 5;
    public TMP_Text lifeText;
    public int level = 0; 
    int rabCount;
    int rabTier;
    int ChtCount;
    int ChtTier;
    //죽은 몬스터 + 닿아서 없어진 몬스터
    public int killCount;
    public int NowWaveMonsterCount;
    public int NowTowerCount = 1;
    public GameObject[] TowerGOs;
    [HideInInspector] public float MonsterMoveSpeed = 1f;
    [HideInInspector] public float TowerAttackSpeed = 1f;
    bool tempMonsterSlow;
    bool tempTowerSpeedUp;



    private void Start()
    {
        UpdateLifeText();
        Time.timeScale = 5f;
    }
    public void StartStage()
    {
        level++;
        killCount = 0;
        Debug.Log($"스테이지 {level} 시작");
        if (level <= 5)
        {
            rabCount = 5; rabTier = level;
            ChtCount = 0; ChtTier = 1;
        }
        else if (level <= 10)
        {
            rabCount = 10; rabTier = level;
            ChtCount = 0; ChtTier = 1;
        }
        else if (level <= 15)
        {
            rabCount = 10; rabTier = 10;
            ChtCount = 0; ChtTier = level - 10;
        }
        else if (level <= 20)
        {
            rabCount = 10; rabTier = 10;
            ChtCount = 10; ChtTier = level - 10;
        }
        else
        {
            rabCount = level - 10; rabTier = 10;
            ChtCount = level - 10; ChtTier = 10;
        }
        NowWaveMonsterCount = rabCount + ChtCount;
        MonsterSpawner.Instance.SpawnWave(rabCount, rabTier, ChtCount, ChtTier);
    }

   
    public void UpKillCount()
    {
        killCount++;
        CheckStageEnd();
    }
    public void CheckStageEnd()
    {
        if(killCount == NowWaveMonsterCount) { EndStage(); }
    }
    void EndStage()
    {
        //Temp몬스터 슬로우, 포탑 강화 해체
        if (tempMonsterSlow)
        {
            tempMonsterSlow = false;
            MonsterMoveSpeed *= 2f;
        }

        if (tempTowerSpeedUp)
        {
            tempTowerSpeedUp = true;
            TowerAttackSpeed /= 1.2f;
        }
        //증강체 띄우기
        CanvasManager.Instance.OpenAugment();
    }

    public void setHP(int hp)
    {
        entireHP = hp;
        UpdateLifeText();
        checkHP();
    }

    public void checkHP()
    {
        if (entireHP <= 0)
        {
            gameOver();
        }
    }

    void UpdateLifeText()
    {
        lifeText.text = $"X {entireHP}";
    }

    //몬스터 죽으면 이거 불러서 킬카운트 증가.
    //킬카운트가 이번 레벨의 몬스터 카운트와 같을 시 다음 웨이브로 넘어간다.
    public void checkKillCount()
    {
        
    }

    public void gameOver()
    {
        Debug.Log("게임 오버");
    }
    
    

    public void TempMonSlow()
    {
        tempMonsterSlow = true;
        MonsterMoveSpeed *= 0.5f;
    }

    public void TempTowerSpeedUP()
    {
        tempTowerSpeedUp = true;
        TowerAttackSpeed *= 1.2f;
    }

    public void OpenTower()
    {
        NowTowerCount++;
        TowerGOs[NowTowerCount-1].gameObject.SetActive(true);
    }
}

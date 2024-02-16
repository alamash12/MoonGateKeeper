using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int entireHP = 5000;
    public TMP_Text lifeText;
    public int level = 10; 
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
    private int bestStage;
    public TMP_Text bestStageText;

    public static SoundManager Sound { get { return instance._soundManager; } }
    SoundManager _soundManager = new SoundManager();

    private void Start()    
    {
        UpdateLifeText();
        Time.timeScale = 4f;
        bestStage = PlayerPrefs.GetInt("BestStage", 0);
        SoundManager.instance.Play(UI_Define.BGM.MainBGM);
    }

    public void StartStage()
    {
        Time.timeScale = 1f;

        level++;
        killCount = 0;
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
            ChtCount = 5; ChtTier = level - 10;
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

        PoolManager.Instance.livingObjectList.Clear();

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

    public void gameOver()
    {
        Time.timeScale = 0f;
        PoolManager.Instance.ReturnAll();

        if (level > bestStage)
        {
            // 현재 스테이지가 최고 기록보다 높을 경우 최고 기록 갱신
            bestStage = level;
            PlayerPrefs.SetInt("BestStage", bestStage);
        }

        bestStageText.text = $"최고 기록 : STAGE {bestStage}";

        level = 0;
        setHP(5);
        for (int i = 1; i < 5; i++)
        {
            TowerGOs[i].SetActive(false);
        }
        MonsterMoveSpeed = 1; TowerAttackSpeed = 1;
        NowTowerCount = 1;
        SeizeTower.instance = null;
        SlowTower.instance = null;
        KnockTower.Instance = null;
        WideTower.Instance = null;
        MonsterSpawner.Instance.StopAllCoroutines();
        CanvasManager.Instance.OpenGameover();
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

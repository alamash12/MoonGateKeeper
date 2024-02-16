using System.Collections;
using System.Collections.Generic;
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

    public static int entireHP = 5;
    public static int level = 1; //웨이브숫자
    int multiplier = 20;
    public int killCount = 0;
    [HideInInspector] public int NowTowerCount = 3;
    [HideInInspector] public float MonsterMoveSpeed = 1f;
    [HideInInspector] public float TowerAttackSpeed = 1f;
    bool tempMonsterSlow;
    bool tempTowerSpeedUp;

    private void Update()
    {
        checkHP();
        checkKillCount();
    }

    public void setHP(int hp)
    {
        entireHP = hp;
    }

    public void checkHP()
    {
        if (entireHP <= 0)
        {
            gameOver();
        }
    }

    //몬스터 죽으면 이거 불러서 킬카운트 증가.
    //킬카운트가 이번 레벨의 몬스터 카운트와 같을 시 다음 웨이브로 넘어간다.
    public void checkKillCount()
    {
        if (killCount >= level * multiplier)
        {
            level++;
            //증강 띄우기.
        }
    }

    public void gameOver()
    {
        
    }
    
    void StartStage()
    {

    }

    void EndStage()
    {
        //Temp몬스터 슬로우, 포탑 강화 해체
        if(tempMonsterSlow)
        {
            tempMonsterSlow = false;
            MonsterMoveSpeed *= 2f;
        }

        if(tempTowerSpeedUp)
        {
            tempTowerSpeedUp = true;
            TowerAttackSpeed /= 1.2f;
        }
        //증강체 띄우기
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
}

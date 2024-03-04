using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AugmentUI : MonoBehaviour
{
    public Button[] buttons;
    public TMP_Text[] btnInfoTexts;
    public Sprite[] Icons;
    
    public static AugmentUI Instance;
    private void Awake()
    {
        Instance = this;
    }

    
    public enum AugmentType
    { 
        AddTower,
        UpgradeBase,
        UpgradeSeize,
        UpgradeSlow,
        UpgradeKnock,
        UpgradeWide,
        UpgradeAll,
        MonSlow,
        TowerSpeedUp,
        TempMonSlow,
        TempTowerSpeedUp,
        LifeUp,
        Reroll,
        MaxCount
    }

    public string[] AugmentString;


    private void OnEnable()
    {
        ShowAugment();
    }

    /// <summary>
    /// 출현 가능한 증강 3개 뽑아와서 그 증강에 대응되는 변화 호출. addlistner로 해당 버튼에 그 증강 기능 구현.
    /// </summary>
    void ShowAugment()
    {
        if (GameManager.instance.entireHP == 0) return; // 마지막 몬스터가 몸박해서 라이프 0되는 경우의 예외처리
        
        int[] AugmentArray = GetThreeValidNumbers(CheckValidNumber());
        for (int i = 0; i < 3; i++)
        {
            int AugmentIndex = AugmentArray[i];
            buttons[i].GetComponent<Image>().sprite = GetProperIcon(AugmentIndex);
            btnInfoTexts[i].text = AugmentString[AugmentIndex];
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => Augment(AugmentIndex));
        }
    }

    Sprite GetProperIcon(int index)
    {
        if (index == 0) return Icons[0];
        if(index<=5) return Icons[1];
        return Icons[index - 4];
    }

    /// <summary>
    /// 증강체 목록중 가능한 목록 체크
    /// </summary>
    List<int> CheckValidNumber()
    {
        var temp = new List<int>();
        if(GameManager.instance.NowTowerCount<5)
        {
            temp.Add((int)AugmentType.AddTower);
        }
        //현재 열려있는 타워 들은 강화 가능
        for(int i =1;i<=GameManager.instance.NowTowerCount;i++ )
        {
            temp.Add(i);
        }
        for (int i = 6; i < (int)AugmentType.MaxCount; i++)
        {
            temp.Add(i);
        }

        return temp;
    }

    /// <summary>
    /// 증강체 목록중 3가지를 반환
    /// </summary>
    /// <param name="Validnumbers"></param>
    /// <returns></returns>
    int[] GetThreeValidNumbers(List<int> validNumbers)
    {
        int[] temp = new int[3];

        // 중복을 허용하지 않고 랜덤하게 3개의 유효한 숫자를 선택
        List<int> tempList = new List<int>(validNumbers);
        for (int i = 0; i < 3; i++)
        {
            int index = UnityEngine.Random.Range(0, tempList.Count);
            temp[i] = tempList[index];
            tempList.RemoveAt(index);
        }

        return temp;
    }

    void Augment(int type)
    {
        AugmentType augmentType = (AugmentType)type;
        switch (augmentType)
        {
            case AugmentType.AddTower:
                GameManager.instance.OpenTower();
                break;
            case AugmentType.UpgradeBase:
                //TowerClass.Towers[(int)TowerType.Base].TowerEffeciency += 2.5f;
                TowerClass.TowerEfficiencyUP(TowerType.Base, 2.5f);
                break;
            case AugmentType.UpgradeSeize:
                //if (SeizeTower.instance != null)
                //{
                //    TowerClass.Towers[(int)TowerType.Seize].TowerEffeciency += 6f;
                //}
                TowerClass.TowerEfficiencyUP(TowerType.Seize, 6f);
                break;
            case AugmentType.UpgradeSlow:
                //if (SlowTower.instance != null)
                //{
                //    TowerClass.Towers[(int)TowerType.Slow].TowerEffeciency += 0.1f;
                //}
                TowerClass.TowerEfficiencyUP(TowerType.Seize, 0.1f);
                break;
            case AugmentType.UpgradeKnock:
                //if (KnockTower.Instance != null)
                //{
                //    TowerClass.Towers[(int)TowerType.KnockBack].TowerEffeciency += 0.5f;
                //}
                TowerClass.TowerEfficiencyUP(TowerType.Seize, 0.5f);
                break;
            case AugmentType.UpgradeWide:
                //if (WideTower.Instance != null)
                //{
                //    TowerClass.Towers[(int)TowerType.AreaAttack].TowerEffeciency += 2.5f;
                //}
                TowerClass.TowerEfficiencyUP(TowerType.Seize, 2.5f);
                break;
            case AugmentType.UpgradeAll:
                //if (TowerClass.Towers[(int)TowerType.Base] != null)
                //{
                //    TowerClass.Towers[(int)TowerType.Base].TowerEffeciency += 0.5f;
                //}
                //if (TowerClass.Towers[(int)TowerType.Seize] != null)
                //{
                //    TowerClass.Towers[(int)TowerType.Seize].TowerEffeciency += 1.2f;
                //}
                //if (TowerClass.Towers[(int)TowerType.Slow] != null)
                //{
                //    TowerClass.Towers[(int)TowerType.Slow].TowerEffeciency += 0.04f;
                //}
                //if (TowerClass.Towers[(int)TowerType.KnockBack] != null)
                //{
                //    TowerClass.Towers[(int)TowerType.KnockBack].TowerEffeciency += 0.2f;
                //}
                //if (TowerClass.Towers[(int)TowerType.AreaAttack] != null)
                //{
                //    TowerClass.Towers[(int)TowerType.AreaAttack].TowerEffeciency += 0.5f;
                //}
                TowerClass.TowerEfficiencyUP(TowerType.Base, 0.5f);
                TowerClass.TowerEfficiencyUP(TowerType.Seize, 1.2f);
                TowerClass.TowerEfficiencyUP(TowerType.Slow, 0.04f);
                TowerClass.TowerEfficiencyUP(TowerType.KnockBack, 0.2f);
                TowerClass.TowerEfficiencyUP(TowerType.AreaAttack, 0.5f);
                break;
            case AugmentType.MonSlow:
                GameManager.instance.MonsterMoveSpeed -= 0.05f;
                break;
            case AugmentType.TowerSpeedUp:
                GameManager.instance.TowerAttackSpeed += 0.2f;
                break;
            case AugmentType.TempMonSlow:
                GameManager.instance.TempMonSlow();
                break;
            case AugmentType.TempTowerSpeedUp:
                GameManager.instance.TempTowerSpeedUP();
                break;
            case AugmentType.LifeUp:
                GameManager.instance.setHP(GameManager.instance.entireHP + 1);
                break;
        }
        SoundManager.Instance.Play(UI_Define.SFX.Upgrade_select);
        if(augmentType == AugmentType.Reroll)
        {
            ShowAugment();
            return;
        }

        GameManager.instance.StartStage();
        gameObject.SetActive(false);
    }


}

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

    void ShowAugment()
    {
        int[] AugmentArray = GetThreeValidNumbers(CheckValidNumber());
        for (int i = 0; i < 3; i++)
        {
            int AugmentIndex = AugmentArray[i];
            buttons[i].GetComponent<Image>().sprite = GetProperIcon(AugmentIndex);
            btnInfoTexts[i].text = AugmentString[AugmentIndex];
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
                BaseTower.Instance.TowerEffeciency += 2.5f;
                break;
            case AugmentType.UpgradeSeize:
                SeizeTower.instance.TowerEffeciency += 6f;
                break;
            case AugmentType.UpgradeSlow:
                SlowTower.instance.TowerEffeciency += 0.1f;
                break;
            case AugmentType.UpgradeKnock:
                KnockTower.Instance.TowerEffeciency += 0.5f;
                break;
            case AugmentType.UpgradeWide:
                WideTower.Instance.TowerEffeciency += 2.5f;
                break;
            case AugmentType.UpgradeAll:
                BaseTower.Instance.TowerEffeciency += 0.5f;
                SeizeTower.instance.TowerEffeciency += 1.2f;
                SlowTower.instance.TowerEffeciency += 0.04f;
                KnockTower.Instance.TowerEffeciency += 0.2f;
                WideTower.Instance.TowerEffeciency += 0.5f;
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

        if(augmentType == AugmentType.Reroll)
        {
            ShowAugment();
            return;
        }

        GameManager.instance.StartStage();
        gameObject.SetActive(false);
    }


}

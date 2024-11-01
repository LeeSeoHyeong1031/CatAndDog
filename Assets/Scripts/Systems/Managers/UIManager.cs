using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    //text
    public TextMeshProUGUI playerBaseHealthText; //플레이어 기지 체력 Text
    public TextMeshProUGUI enemyBaseHealthText; //적 기지 체력 Text
    public TextMeshProUGUI cointText; //코인 Text
    public TextMeshProUGUI levelText; //현재 레벨 Text
    public TextMeshProUGUI costText; //레벨업에 필요한 비용 Text
    //image
    public Image UltimateBackground; //궁극기 배경 이미지

    //MaxLevel() 메서드에서 사용할 변수들
    public GameObject costBackgroundObj;
    public Image levelUpBackgroundImage;

    private void Start()
    {
        Init();
    }
    //플레이어 기지 체력 Text 업뎃
    public void UpdatePlayerBaseHeathText()
    {
        PlayerBase pb = GameManager.Instance.playerBase;
        playerBaseHealthText.text = $"{pb.health}/{pb.maxHealth}";
    }

    //적 기지 체력 Text 업뎃
    public void UpdateEnemyBaseHeathText()
    {
        EnemyBase eb = GameManager.Instance.enemyBase;
        enemyBaseHealthText.text = $"{eb.health}/{eb.maxHealth}";
    }

    //코인 보유량 Text 업뎃
    public void UpdateCoinText(ref int curCoin, ref int maxCoin)
    {
        cointText.text = $"{curCoin} / {maxCoin}";
    }

    //레벨 Text 업뎃
    public void UpdateLevelText()
    {
        levelText.text = $"LV {GameManager.Instance.level + 1}";
    }

    //레벨업 비용 Text 업뎃
    public void UpdateCostText()
    {
        costText.text = GameManager.Instance.costByLevelUp[GameManager.Instance.level].ToString();
    }

    //최대 레벨일 때 처리하기 위한 메서드
    public void MaxLevel()
    {
        costBackgroundObj.SetActive(false);
        levelUpBackgroundImage.color = Color.gray;
        levelText.text = "Lv MAX";
    }

    public void UpdateUltimateColor(ref float ultimateInterval, ref Color origin)
    {
        UltimateBackground.color = Time.time >= ultimateInterval ? Color.white : origin;
    }



    //UI 초기화
    public void Init()
    {
        UpdatePlayerBaseHeathText();
        UpdateEnemyBaseHeathText();
        UpdateLevelText();
        UpdateCostText();
    }
}

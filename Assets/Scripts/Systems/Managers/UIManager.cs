using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    //text
    public TextMeshProUGUI playerBaseHealthText; //플레이어 기지 체력 Text
    public TextMeshProUGUI enemyBaseHealthText; //적 기지 체력 Text
    public TextMeshProUGUI coinText; //코인 Text
    public TextMeshProUGUI levelText; //현재 레벨 Text
    public TextMeshProUGUI costText; //레벨업에 필요한 비용 Text

    //image
    public Image UltimateBackground; //궁극기 배경 이미지

    //MaxLevel() 메서드에서 사용할 변수들
    public GameObject costBackgroundObj;
    public Image levelUpBackgroundImage;

    //Transform
    public Transform ultimateParticleObj;

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
        coinText.text = $"{curCoin} / {maxCoin}";
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

    //Cost 
    //고쳐야 함.
    public void UpdateLevelUpUI(ref bool res)
    {
        //현재 소지한 코인량이 레벨업에 필요한 비용보다 작을 경우
        if (res)
        {
            costText.color = Color.black;
            StartCoroutine(LevelUpBgImageCoroutine(res));
        }
        else
        {
            costText.color = Color.red; //비용Text 빨간색
            levelUpBackgroundImage.color = Color.gray;
        }
    }

    public IEnumerator LevelUpBgImageCoroutine(bool res)
    {
        while (res)
        {
            Color color = Color.yellow;
            color.a = 0.7f;
            levelUpBackgroundImage.color = color;
            yield return new WaitForSeconds(0.5f);
            color.a = 1f;
            levelUpBackgroundImage.color = color;
            yield return new WaitForSeconds(0.5f);
        }
    }

    //최대 레벨일 때 처리하기 위한 메서드
    public void MaxLevel()
    {
        costBackgroundObj.SetActive(false);
        levelUpBackgroundImage.color = Color.gray;
        levelText.text = "Lv MAX";
    }

    //궁극기 활성화
    public void UltimateActive(ref float lastUltimateUse)
    {
        if (Time.time >= lastUltimateUse)
        {
            UltimateBackground.color = Color.white;
            ultimateParticleObj.gameObject.SetActive(true);
        }
    }

    //궁극기 비활성화
    public void UltimateDeActive()
    {
        UltimateBackground.color = Color.gray;
        ultimateParticleObj.gameObject.SetActive(false);
    }

    //UI 초기화
    public void Init()
    {
        UpdatePlayerBaseHeathText(); //플레이어 기지 체력 Text
        UpdateEnemyBaseHeathText();  //적 기지 체력 Text
        UpdateLevelText(); //플레이어 레벨 Text
        UpdateCostText(); //플레이어 레벨업 비용 Text
    }
}

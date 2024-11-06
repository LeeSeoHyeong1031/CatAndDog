using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class UIManager : SingletonManager<UIManager>
{
    [SerializeField] internal Elements elements;
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

    //승리, 패배 GameObject
    public GameObject victoryUI;
    public GameObject defeatUI;
    public GameObject endPanel;

    private bool canUseUlti;

    private void Start()
    {
        StartCoroutine(UpdateLevelUpBgColor());
        StartCoroutine(UltimateBgColor());
    }

    public void FindUIElements()
    {
        // UI 요소들 찾기
        playerBaseHealthText = elements.playerBaseHealthText;
        enemyBaseHealthText = elements.enemyBaseHealthText;
        coinText = elements.coinText;
        levelText = elements.levelText;
        costText = elements.costText;
        UltimateBackground = elements.UltimateBackground;
        costBackgroundObj = elements.costBackgroundObj;
        levelUpBackgroundImage = elements.levelUpBackgroundImage;
        ultimateParticleObj = elements.ultimateParticleObj;
        victoryUI = elements.victoryUI;
        defeatUI = elements.defeatUI;
        endPanel = elements.endPanel;
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

    //레벨업 배경 업그레이드
    public IEnumerator UpdateLevelUpBgColor()
    {
        while (GameManager.Instance.level == GameManager.Instance.maxCoinByLevel.Length - 1)
        {

            if (GameManager.Instance.curCoin >= GameManager.Instance.costByLevelUp[GameManager.Instance.level])
            {
                levelUpBackgroundImage.color = Color.yellow;
                yield return new WaitForSeconds(0.5f);
                Color color = levelUpBackgroundImage.color;
                color.a = 0.7f;
                levelUpBackgroundImage.color = color;
                if (GameManager.Instance.curCoin < GameManager.Instance.costByLevelUp[GameManager.Instance.level]) levelUpBackgroundImage.color = Color.gray;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                levelUpBackgroundImage.color = Color.gray;
            }
            yield return null;
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
            canUseUlti = true;
            ultimateParticleObj.gameObject.SetActive(true);
        }
    }

    public IEnumerator UltimateBgColor()
    {
        while (true)
        {
            if (!canUseUlti) UltimateDeActive();
            else
            {
                UltimateBackground.color = new Color32(250, 100, 255, 255);
                yield return new WaitForSeconds(0.5f);
                UltimateBackground.color = new Color32(250, 100, 255, 170);
                if (!canUseUlti) UltimateDeActive();
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }
    }

    //궁극기 비활성화
    public void UltimateDeActive()
    {
        canUseUlti = false;
        UltimateBackground.color = Color.gray;
        ultimateParticleObj.gameObject.SetActive(false);
    }

    public void Victory(bool value)
    {
        endPanel.SetActive(value);
        victoryUI.SetActive(value);
    }

    public void Defeat(bool value)
    {
        endPanel.SetActive(value);
        defeatUI.SetActive(value);
    }

    //UI 초기화
    public void Init()
    {
        UpdatePlayerBaseHeathText(); //플레이어 기지 체력 Text
        UpdateEnemyBaseHeathText();  //적 기지 체력 Text
        UpdateLevelText(); //플레이어 레벨 Text
        UpdateCostText(); //플레이어 레벨업 비용 Text
        Victory(false);
        Defeat(false);
    }
}

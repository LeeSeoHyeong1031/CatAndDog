using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonManager<UIManager>
{
    //text
    public TextMeshProUGUI playerBaseHealthText; //�÷��̾� ���� ü�� Text
    public TextMeshProUGUI enemyBaseHealthText; //�� ���� ü�� Text
    public TextMeshProUGUI cointText; //���� Text
    public TextMeshProUGUI levelText; //���� ���� Text
    public TextMeshProUGUI costText; //�������� �ʿ��� ��� Text
    //image
    public Image UltimateBackground; //�ñر� ��� �̹���

    //MaxLevel() �޼��忡�� ����� ������
    public GameObject costBackgroundObj;
    public Image levelUpBackgroundImage;

    private void Start()
    {
        Init();
    }
    //�÷��̾� ���� ü�� Text ����
    public void UpdatePlayerBaseHeathText()
    {
        PlayerBase pb = GameManager.Instance.playerBase;
        playerBaseHealthText.text = $"{pb.health}/{pb.maxHealth}";
    }

    //�� ���� ü�� Text ����
    public void UpdateEnemyBaseHeathText()
    {
        EnemyBase eb = GameManager.Instance.enemyBase;
        enemyBaseHealthText.text = $"{eb.health}/{eb.maxHealth}";
    }

    //���� ������ Text ����
    public void UpdateCoinText(ref int curCoin, ref int maxCoin)
    {
        cointText.text = $"{curCoin} / {maxCoin}";
    }

    //���� Text ����
    public void UpdateLevelText()
    {
        levelText.text = $"LV {GameManager.Instance.level + 1}";
    }

    //������ ��� Text ����
    public void UpdateCostText()
    {
        costText.text = GameManager.Instance.costByLevelUp[GameManager.Instance.level].ToString();
    }

    //�ִ� ������ �� ó���ϱ� ���� �޼���
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



    //UI �ʱ�ȭ
    public void Init()
    {
        UpdatePlayerBaseHeathText();
        UpdateEnemyBaseHeathText();
        UpdateLevelText();
        UpdateCostText();
    }
}

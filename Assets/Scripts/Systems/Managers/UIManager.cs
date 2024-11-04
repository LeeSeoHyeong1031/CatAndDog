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
    public TextMeshProUGUI playerBaseHealthText; //�÷��̾� ���� ü�� Text
    public TextMeshProUGUI enemyBaseHealthText; //�� ���� ü�� Text
    public TextMeshProUGUI coinText; //���� Text
    public TextMeshProUGUI levelText; //���� ���� Text
    public TextMeshProUGUI costText; //�������� �ʿ��� ��� Text

    //image
    public Image UltimateBackground; //�ñر� ��� �̹���

    //MaxLevel() �޼��忡�� ����� ������
    public GameObject costBackgroundObj;
    public Image levelUpBackgroundImage;

    //Transform
    public Transform ultimateParticleObj;

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
        coinText.text = $"{curCoin} / {maxCoin}";
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

    //Cost 
    //���ľ� ��.
    public void UpdateLevelUpUI(ref bool res)
    {
        //���� ������ ���η��� �������� �ʿ��� ��뺸�� ���� ���
        if (res)
        {
            costText.color = Color.black;
            StartCoroutine(LevelUpBgImageCoroutine(res));
        }
        else
        {
            costText.color = Color.red; //���Text ������
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

    //�ִ� ������ �� ó���ϱ� ���� �޼���
    public void MaxLevel()
    {
        costBackgroundObj.SetActive(false);
        levelUpBackgroundImage.color = Color.gray;
        levelText.text = "Lv MAX";
    }

    //�ñر� Ȱ��ȭ
    public void UltimateActive(ref float lastUltimateUse)
    {
        if (Time.time >= lastUltimateUse)
        {
            UltimateBackground.color = Color.white;
            ultimateParticleObj.gameObject.SetActive(true);
        }
    }

    //�ñر� ��Ȱ��ȭ
    public void UltimateDeActive()
    {
        UltimateBackground.color = Color.gray;
        ultimateParticleObj.gameObject.SetActive(false);
    }

    //UI �ʱ�ȭ
    public void Init()
    {
        UpdatePlayerBaseHeathText(); //�÷��̾� ���� ü�� Text
        UpdateEnemyBaseHeathText();  //�� ���� ü�� Text
        UpdateLevelText(); //�÷��̾� ���� Text
        UpdateCostText(); //�÷��̾� ������ ��� Text
    }
}

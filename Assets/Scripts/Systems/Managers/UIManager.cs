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

    //�¸�, �й� GameObject
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
        // UI ��ҵ� ã��
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

    //������ ��� ���׷��̵�
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

    //�ñر� ��Ȱ��ȭ
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

    //UI �ʱ�ȭ
    public void Init()
    {
        UpdatePlayerBaseHeathText(); //�÷��̾� ���� ü�� Text
        UpdateEnemyBaseHeathText();  //�� ���� ü�� Text
        UpdateLevelText(); //�÷��̾� ���� Text
        UpdateCostText(); //�÷��̾� ������ ��� Text
        Victory(false);
        Defeat(false);
    }
}

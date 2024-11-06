using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Elements : MonoBehaviour
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

    //�¸�, �й� GameObject
    public GameObject victoryUI;
    public GameObject defeatUI;
    public GameObject endPanel;

    private void Awake()
    {
        UIManager.Instance.elements = this;
        UIManager.Instance.FindUIElements();
    }

    private void Start()
    {
        UIManager.Instance.Init();
    }
}

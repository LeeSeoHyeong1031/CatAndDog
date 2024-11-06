using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Elements : MonoBehaviour
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

    //승리, 패배 GameObject
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

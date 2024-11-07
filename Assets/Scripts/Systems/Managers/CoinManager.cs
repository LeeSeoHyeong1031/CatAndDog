using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //���ΰ��� ������
    public int curCoin = 0; //���� ����
    public int[] maxCoinByLevel = { 100, 150, 200, 250, 300, 350, 400 }; //���� �� �ִ� �ִ� ����
    private int[] coinRaiseByLevel = { 6, 10, 14, 18, 22, 26, 30 }; //�ʴ� ������ ���η�
    private float lastCoinRaiseTime = 0; //������ ������ ���� �ð�
    public int[] costByLevelUp = { 40, 80, 120, 160, 200, 240 }; //������ �� ���.
    public int level = 0;
    private float displayCoin = 0f;

    private void Awake()
    {
        GameManager.Instance.setCoinManager(this);
    }

    private void Start()
    {
        StartCoroutine(UIManager.Instance.UpdateLevelUpBgColor());
        StartCoroutine(DisplayRaiseCoin());
    }

    private void Update()
    {
        UpdateCoin();
        UpdateLevelUpCostColor();
    }

    //���� ������Ʈ �޼���.
    public void UpdateCoin()
    {
        //������ �ʴ� ������ ����
        if (Time.time >= lastCoinRaiseTime)
        {
            lastCoinRaiseTime = Time.time + 1f; //������ ������ ���� �ð��� 1�� �� ������. <- 1�ʰ� ���� ������ ������Ʈ ����.
            curCoin += coinRaiseByLevel[level]; //���������� ���η� ����.
            curCoin = Mathf.Clamp(curCoin, 0, maxCoinByLevel[level]); //curCoin�� ��� �����ִµ� ���� �������� ���� �� �ִ� �ִ������� �Ѿ��
                                                                      //�˾Ƽ� ��.
        }
    }

    public IEnumerator DisplayRaiseCoin()
    {
        while (true)
        {
            float startTime = Time.time;
            float endTime = Time.time + 1f;
            float startCoin = displayCoin;
            float endCoin = curCoin;

            while (Time.time < endTime)
            {
                // Lerp�� �� ��° ���ڸ� 0���� 1���� �ε巴�� ������Ŵ
                displayCoin = Mathf.Lerp(startCoin, endCoin, (Time.time - startTime) / (endTime - startTime));
                UIManager.Instance.UpdateCoinText(displayCoin.ToString("n0"), ref maxCoinByLevel[level]);
                yield return null;
            }
            displayCoin = endCoin; // 1�� �� ��Ȯ�� curCoin ������ ����
        }
    }

    //Cost 
    public void UpdateLevelUpCostColor()
    {
        //������ �ִ�ġ �� ��� return
        if (level == maxCoinByLevel.Length - 1) return;
        UIManager.Instance.costText.color = curCoin >= costByLevelUp[level] ? Color.black : Color.red;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //코인관련 변수들
    public int curCoin = 0; //현재 코인
    public int[] maxCoinByLevel = { 100, 150, 200, 250, 300, 350, 400 }; //가질 수 있는 최대 코인
    private int[] coinRaiseByLevel = { 6, 10, 14, 18, 22, 26, 30 }; //초당 오르는 코인량
    private float lastCoinRaiseTime = 0; //마지막 코인이 오른 시간
    public int[] costByLevelUp = { 40, 80, 120, 160, 200, 240 }; //레벨업 별 비용.
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

    //코인 업데이트 메서드.
    public void UpdateCoin()
    {
        //코인이 초당 오르는 로직
        if (Time.time >= lastCoinRaiseTime)
        {
            lastCoinRaiseTime = Time.time + 1f; //마지막 코인이 오른 시간에 1초 더 더해줌. <- 1초가 지날 때마다 업데이트 해줌.
            curCoin += coinRaiseByLevel[level]; //레벨에따른 코인량 증가.
            curCoin = Mathf.Clamp(curCoin, 0, maxCoinByLevel[level]); //curCoin을 계속 더해주는데 현재 레벨에서 가질 수 있는 최대코인을 넘어서면
                                                                      //알아서 컷.
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
                // Lerp의 세 번째 인자를 0에서 1까지 부드럽게 증가시킴
                displayCoin = Mathf.Lerp(startCoin, endCoin, (Time.time - startTime) / (endTime - startTime));
                UIManager.Instance.UpdateCoinText(displayCoin.ToString("n0"), ref maxCoinByLevel[level]);
                yield return null;
            }
            displayCoin = endCoin; // 1초 후 정확히 curCoin 값으로 설정
        }
    }

    //Cost 
    public void UpdateLevelUpCostColor()
    {
        //레벨이 최대치 일 경우 return
        if (level == maxCoinByLevel.Length - 1) return;
        UIManager.Instance.costText.color = curCoin >= costByLevelUp[level] ? Color.black : Color.red;
    }
}

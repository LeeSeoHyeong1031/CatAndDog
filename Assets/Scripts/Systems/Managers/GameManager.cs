using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : SingletonManager<GameManager>
{
    public List<Player> curPlayerUnits = new(); //현재 게임에 스폰되어 있는 플레이어 유닛List
    public List<Enemy> curEnemyUnits = new(); //현재 게임에 스폰되어 있는 적 유닛List

    public PlayerBase playerBase;
    public EnemyBase enemyBase;

    public PlayerBaseSpawner playerBaseSpawner;

    //코인관련 변수들
    public int curCoin = 0; //현재 코인
    public int[] maxCoinByLevel = { 100, 150, 200, 250, 300, 350, 400 }; //가질 수 있는 최대 코인
    private int[] coinRaiseByLevel = { 6, 10, 14, 18, 22, 26, 30 }; //초당 오르는 코인량
    private float lastCoinRaiseTime = 0; //마지막 코인이 오른 시간
    public int[] costByLevelUp = { 40, 80, 120, 160, 200, 240 }; //레벨업 별 비용.
    public int level = 0;

    private void Update()
    {
        UpdateCoin(); //1초마다 업데이트.
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
            UIManager.Instance.UpdateCoinText(ref curCoin, ref maxCoinByLevel[level]); //코인Text UI업데이트
        }
    }

    //Cost 
    public void UpdateLevelUpCostColor()
    {
        //레벨이 최대치 일 경우 return
        if (level == maxCoinByLevel.Length - 1) return;
        UIManager.Instance.costText.color = curCoin >= costByLevelUp[level] ? Color.black : Color.red;
    }

    //레벨업이 가능한지.
    public void CanLevelUp()
    {
        //레벨별 최대 코인의 길이보다 레벨이 크거나 같다면 레벨이 초과했으니 return.
        if (level >= maxCoinByLevel.Length - 1)
        {
            return; //레벨업 할 수 없음.  (현재 레벨 최대치 6, maxCoinByLevel 최대치 6)
        }

        //레벨업 버튼을 눌렀는데 현재 가진 코인이 레벨업 하기 위한 자원보다 작은 경우.
        if (curCoin < costByLevelUp[level])
        {
            return;
        }
        //현재 가진 코인이 레벨업 하기 위한 자원보다 클 경우 레벨업.
        else
        {
            //레벨업 하니까 현재 코인을 레벨업 비용 만큼 차감 해야함.
            curCoin -= costByLevelUp[level];
            LevelUp();
        }
    }

    //레벨업 메서드.
    private void LevelUp()
    {
        level++;
        //레벨UI update, 레벨업에 필요한 비용UI update.

        //Cost는 다른 배열의 길이보다 하나 작기 때문에 에러남. 그래서 최대 레벨에 도달 했을 때
        //UpdateCostText를 호출하지 않고 MaxLevel을 통해 처리.
        if (level == maxCoinByLevel.Length - 1)
        {
            UIManager.Instance.MaxLevel();
            return;
        }
        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateCostText();
    }

    //게임 진행
    public void Resume()
    {
        Time.timeScale = 1;
    }

    //일시 정지
    public void Stop()
    {
        Time.timeScale = 0;
    }

    //타이틀 씬 불러오기
    public void LoadTitleScene()
    {
        MySceneManager.Instance.LoadTitleScene();
    }

    //게임 매니저 관련 필드 초기화
    public void localInit()
    {
        level = 0;
        curPlayerUnits.Clear();
        curEnemyUnits.Clear();
        curCoin = 0;
    }

    //게임 시작 전 모든 초기화
    public void AllInit()
    {
        localInit();
    }

    //GameManager에 있는 curPlayerUnits에 Add할 떄 리스트안에서 y좌표 별로 정렬해서 넣어놔야 함.
    //높은 y좌표 값부터 내림차순 정렬. 그리고 그 정렬된 List에 있는 요소 Layer값을 순차적으로 +1씩 부여.
    public void DescendingSortList<T>(List<T> list) where T : MonoBehaviour
    {
        List<T> u_List = new List<T>();

        while (u_List.Count < list.Count)
        {
            float maxY = float.MinValue;
            T bigUnit = null;

            foreach (T unit in list)
            {
                // 이미 추가된 플레이어는 건너뛰기
                if (u_List.Contains(unit)) continue;

                // y값이 가장 큰 플레이어 찾기
                if (unit.transform.position.y > maxY)
                {
                    maxY = unit.transform.position.y;
                    bigUnit = unit;
                }
            }

            if (bigUnit != null)
            {
                u_List.Add(bigUnit);
            }
        }

        // 정렬된 리스트를 curPlayerUnits에 업데이트
        for (int i = 0; i < u_List.Count; i++)
        {
            list[i] = u_List[i];
            list[i].GetComponent<SortingGroup>().sortingOrder = i; // 순차적으로 레이어 값 부여
        }
    }
}
